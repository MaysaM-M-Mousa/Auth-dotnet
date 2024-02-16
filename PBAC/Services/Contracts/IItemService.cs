using PBAC.Models.Db;

namespace PBAC.Services.Contracts;

public interface IItemService
{
    public Task Create(Item item);

    public Task Update(Guid itemId, Item item);

    public Task Delete(Guid itemId);

    public Task<Item> Get(Guid itemId);
}
