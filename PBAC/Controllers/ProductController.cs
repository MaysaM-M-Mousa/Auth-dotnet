using Microsoft.AspNetCore.Mvc;
using PBAC.Authorization;
using PBAC.Constants;
using PBAC.Services.Contracts;

namespace PBAC.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HasPermission(Permissions.Products.Create)]
    [HttpPost]
    public async Task CreateProduct(ProductDto product)
    {
        await _productService.Create(new() { Name = product.Name, Quantity = product.Quantity });
    }

    [HasPermission(Permissions.Products.Update)]
    [HttpPut("{productId}")]
    public async Task UpdateProduct(Guid productId, ProductDto product)
    {
        await _productService.Update(productId, new() { Name = product.Name, Quantity = product.Quantity });
    }

    [HasPermission(Permissions.Products.Delete)]
    [HttpDelete("{productId}")]
    public async Task DeleteProduct(Guid productId)
    {
        await _productService.Delete(productId);
    }

    [HasPermission(Permissions.Products.Read)]
    [HttpGet("{productId}")]
    public async Task<ProductDto> GetProduct(Guid productId)
    {
        var product = await _productService.Get(productId);
        return new()
        {
            Id = product.Id,
            Name = product.Name, 
            Quantity = product.Quantity
        };
    }
}

public class ProductDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public int Quantity { get; set; }
}