using Goal.Api;
using Goal.Api.Configurations;
using Goal.API.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

// Add API versioning
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});

// Add API versioning support to Swagger documentation
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// Add controllers and Swagger to DI
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Config Swagger documentation for multiple versions
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

// Reg. services using ServiceExtensions (and other DI configurations)
builder.Services.AddServices(builder.Configuration);
// Add MediatR
builder.Services.AddMediatR(cfg => {
    // registration handlers from API
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    // registration handlers from activities
    cfg.RegisterServicesFromAssembly(typeof(Goal.Application.Activities.Product.UpdateProductDescriptionActivity).Assembly);
});

var app = builder.Build();

// Middleware for Swagger
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

// https redirect not need it
//app.UseHttpsRedirection();

app.MapControllers();

app.Run();