using Goal.Application.Interfaces.Product;
using Microsoft.AspNetCore.Mvc;
using Goal.Shared.Models.Request.Product;
using Goal.Shared.Models.Response.Product;

namespace Goal.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Consumes("application/json")]
[Produces("application/json")]
public class ProductsController(IProductService productService) : ControllerBase
{
    /// <summary>
    /// Retrieves a list of products (Version 1).
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
    /// Retrieves a list of products with pagination (Version 2).
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
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllProductsV2([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        if (pageNumber <= 0 || pageSize <= 0)
        {
            return BadRequest("Page number and page size must be greater than 0.");
        }

        var productsPage = await productService.GetAllProductsPagedAsync(pageNumber, pageSize);
        if (!productsPage.Items.Any())
        {
            return NotFound();
        }

        return Ok(productsPage.Items);
    }

    /// <summary>
    /// Retrieves a product by its ID (Shared between API versions 1 and 2).
    /// </summary>
    /// <param name="id">The ID of the product</param>
    /// <returns>The product details</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ProductResponse>> GetProductById(int id)
    {
        var product = await productService.GetProductByIdAsync(id);
        return Ok(product);
    }

    /// <summary>
    /// Updates an existing product (Shared between API versions 1 and 2).
    /// </summary>
    /// <param name="id">The ID of the product</param>
    /// <param name="newDescription"></param>
    /// <returns>No content on success</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> UpdateProduct(int id, [FromBody] string newDescription)
    {
        var product = await productService.UpdateProductDescriptionAsync(id, newDescription);
        return Ok(product);
    }
}