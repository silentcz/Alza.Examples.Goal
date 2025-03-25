using Goal.Infrastructure.Mappings;
using Goal.Infrastructure.Persistence;
using Goal.Infrastructure.Repositories.Interfaces.Product;
using Goal.Shared.DTOs.Product;
using Goal.Shared.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace Goal.Infrastructure.Repositories.Services.Product;

public class ProductRepository(GoalDatabaseContext dbContext, IInfrastructureMapper mapper) : IProductRepository
{
    public async Task<IEnumerable<ProductDto>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        /* var products = await dbContext.Products.ToListAsync();
         * old: - nepouziva cancellationToken
         */

        var products = await dbContext.Products.ToListAsync(cancellationToken);
        return products.Select(mapper.Map);
    }

    public async Task<ProductDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        /* var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
         * old: - neoptimalni,
         *      - nepouziva cancellationToken
         */

        var product = await dbContext.Products.FindAsync([id], cancellationToken);
        /* pouziti: cancellationToken
         * FirstOrDefaultAsync vs FindAsync
         *   FirstOrDefaultAsync .. vzdy generuje dotaz do databáze bez ohledu na to, zda je entita sledovana v dbContext
         *                       .. vhdona pro pouziti s predikatem, tj. obecnejsi,
         *   FindAsync           .. nejprve hleda entitu v lokalni cache (dbContext tracking), kdyz ji nenajde, dotazuje databázi
         *                       .. je specialne navrzen pro vyhledavani podle PK
         */

        return product is not null ? mapper.Map(product) : null;
    }

    public async Task<PagedResult<ProductDto>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        if (pageNumber < 1 || pageSize < 1)
        {
            return new PagedResult<ProductDto> { Items = [] };
        }

        var totalItems = await dbContext.Products.CountAsync(cancellationToken: cancellationToken);

        /* var products = await dbContext.Products
               .OrderBy(p => p.Id)
               .Skip((pageNumber - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();
         * old: - nepouziva cancellationToken
         */

        var products = await dbContext.Products
            .AsNoTracking()
            .OrderBy(p => p.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        /* pouziti: cancellationToken
         * pridan:  AsNoTracking .. data pouze cte
         */

        var productDtos = products.Select(mapper.Map);

        return new PagedResult<ProductDto>
        {
            Items = productDtos,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = totalItems
        };
    }

    public async Task<ProductDto?> UpdateAsync(int id, string newDescription, CancellationToken cancellationToken = default)
    {
        /* var existingProduct = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
         * old: - neoptimalni,
         *      - nepouziva cancellationToken
         *      - SaveChangesAsync spadne kvuli SqlServerRetryingExecutionStrategy
         */

        var existingProduct = await dbContext.Products.FindAsync([id], cancellationToken);
        if (existingProduct is null) return null;

        try
        {
            var strategy = dbContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
                existingProduct.Description = newDescription;
                await dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            });
            /* pouziti: cancellationToken
             * pridan:  transaction
             * fix:     pouziti CreateExecutionStrategy, ktera automaticky opakuje operace pri prechodnych chybach pripojeni
             */

            return mapper.Map(existingProduct);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error updating product", ex);
        }
    }
}