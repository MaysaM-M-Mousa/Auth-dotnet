using PBAC.Models.Db;

namespace PBAC.Services.Contracts;

public interface IProductService
{
    public Task Create(Product product);

    public Task Update(Guid productId, Product product);

    public Task Delete(Guid productId);

    public Task<Product> Get(Guid productId);
}
