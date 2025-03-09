using Goal.Api;
using Goal.Api.Configurations;
using Goal.API.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

// Přidání verzování API
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true; // Přidá informace o dostupných verzích do odpovědi
    options.AssumeDefaultVersionWhenUnspecified = true; // Použije výchozí verzi, pokud není specifikována
    options.DefaultApiVersion = new ApiVersion(1, 0); // Výchozí verze (v1.0)
});

// Přidání podpory API verzování do dokumentace Swagger
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// Přidání kontrolerů a Swaggeru do DI
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Konfigurace Swagger dokumentace pro více verzí
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

// Registrace služeb pomocí ServiceExtensions (a dalších DI konfigurací)
builder.Services.AddServices(builder.Configuration);


var app = builder.Build();

// Middleware pro Swagger
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    foreach (var description in provider.ApiVersionDescriptions)
    {
        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
            description.GroupName.ToUpperInvariant());
    }
});

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();