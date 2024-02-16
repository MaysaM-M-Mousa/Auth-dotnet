using Microsoft.EntityFrameworkCore;
using PBAC.DB;
using PBAC.Models.Db;
using PBAC.Services.Contracts;

namespace PBAC.Services;

public class ProductService : IProductService
{
    private readonly PbacContext _context;

    public ProductService(PbacContext context)
    {
        _context = context;
    }

    public async Task Create(Product product)
    {
        product.Id = Guid.NewGuid();
        _context.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid productId)
    {
        _context.Remove(new Product() { Id = productId });
        await _context.SaveChangesAsync();
    }

    public async Task<Product> Get(Guid productId)
    {
        return await _context
            .Products
            .FirstOrDefaultAsync(p => p.Id == productId);
    }

    public async Task Update(Guid productId, Product product)
    {
        product.Id = productId;
        _context.Update(product);
        await _context.SaveChangesAsync();
    }
}
