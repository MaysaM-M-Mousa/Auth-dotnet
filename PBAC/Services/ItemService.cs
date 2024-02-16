using Microsoft.EntityFrameworkCore;
using PBAC.DB;
using PBAC.Models.Db;
using PBAC.Services.Contracts;

namespace PBAC.Services;

public class ItemService : IItemService
{
    private readonly PbacContext _context;

    public ItemService(PbacContext context)
    {
        _context = context;
    }

    public async Task Create(Item item)
    {
        item.Id = Guid.NewGuid();
        _context.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid itemId)
    {
        _context.Remove(new Item() { Id = itemId });
        await _context.SaveChangesAsync();
    }

    public async Task<Item> Get(Guid itemId)
    {
        return await _context
            .Items
            .FirstOrDefaultAsync(p => p.Id == itemId);
    }

    public async Task Update(Guid itemId, Item item)
    {
        item.Id = itemId;
        _context.Update(item);
        await _context.SaveChangesAsync();
    }
}
