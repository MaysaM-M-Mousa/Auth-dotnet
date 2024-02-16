using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PBAC.Authorization;
using PBAC.Services.Contracts;

namespace PBAC.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IItemController : ControllerBase
{
    private readonly IItemService _itemService;

    public IItemController(IItemService itemService)
    {
        _itemService = itemService;
    }

    [HasPermission("item:create")]
    [HttpPost]
    public async Task CreateItem(ItemDto item)
    {
        await _itemService.Create(new() { Name = item.Name, Description = item.Description });
    }

    [HasPermission("item:update")]
    [HttpPut("{itemId}")]
    public async Task UpdateItem(Guid itemId, ItemDto item)
    {
        await _itemService.Update(itemId, new() { Name = item.Name, Description = item.Description });
    }

    [HasPermission("item:delete")]
    [HttpDelete("{itemId}")]
    public async Task DeleteItem(Guid itemId)
    {
        await _itemService.Delete(itemId);
    }

    [HasPermission("item:read")]
    [HttpGet("{itemId}")]
    public async Task<ItemDto> GetItem(Guid itemId)
    {
        var item = await _itemService.Get(itemId) ?? throw new KeyNotFoundException();
        return new()
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description
        };
    }
}


public class ItemDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}