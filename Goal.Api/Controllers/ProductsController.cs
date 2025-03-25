using Goal.Application.Activities.Product;
using Goal.Application.Interfaces.Product;
using Goal.Application.Mappings;
using Microsoft.AspNetCore.Mvc;
using Goal.Shared.Models.Response.Product;
using MediatR;

namespace Goal.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Consumes("application/json")]
[Produces("application/json")]
public class ProductsController(IProductService productService, IMediator mediator, IApplicationMapper mapper) : ControllerBase
{
    /// <summary>
    /// Retrieves a list of products (Version 1)
    /// | Vrati vsechny produkty
    /// </summary>
    /// <returns>List of products</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllProductsV1()
    {
        var products = await productService.GetAllProductsAsync();
        return products is not null && !products.Any() ? NotFound() : Ok(products);
    }

    /// <summary>
    /// Retrieves a list of products with pagination (Version 2)
    /// | Vrati vsechny produkty s podporou strankovani
    /// </summary>
    /// <param name="pageNumber">Page number</param>
    /// <param name="pageSize">Number of items per page</param>
    /// <returns>Paged list of products</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [MapToApiVersion("2.0")]
    public async Task<ActionResult<ProductPageResponse>> GetAllProductsV2([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        if (pageNumber <= 0 || pageSize <= 0) return BadRequest("Page number and page size must be greater than 0.");

        var productsPage = await productService.GetAllProductsPagedAsync(pageNumber, pageSize);
        if (!productsPage.Items.Any()) return NotFound();

        return Ok(mapper.Map(productsPage));
    }

    /// <summary>
    /// Retrieves a product by its ID
    /// | Vrati produkt podle zvoleneho ID
    /// </summary>
    /// <param name="id">The ID of the product</param>
    /// <returns>The product details</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ProductResponse>> GetProductById(int id)
    {
        var product = await productService.GetProductByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    /// <summary>
    /// Updates the description of an existing product
    /// | Zmena obsahu popisu zvoleneho produktu
    /// </summary>
    /// <param name="id">The ID of the product</param>
    /// <param name="newDescription"></param>
    /// <returns>No content on success</returns>
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ProductResponse>> UpdateProductV1(int id, [FromBody] string newDescription)
    {
        var product = await productService.UpdateProductDescriptionAsync(id, newDescription);
        if (product == null) return NotFound();
        return Ok(product);
    }

    /// <summary>
    /// Updates the description of an existing product using Mediator pattern (Version 2)
    /// | Zmena obsahu popisu zvoleneho produktu s vyuzitim Mediator patternu
    /// </summary>
    /// <param name="id">The ID of the product</param>
    /// <param name="newDescription"></param>
    /// <returns>The updated product</returns>
    [HttpPatch("{id:int}/v2")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [MapToApiVersion("2.0")]
    public async Task<ActionResult<ProductResponse>> UpdateProductV2(int id, [FromBody] string newDescription)
    {
        var result = await mediator.Send(new UpdateProductDescriptionActivity(id, newDescription));

        if (result == null) return NotFound();
        return Ok(result);
    }
}