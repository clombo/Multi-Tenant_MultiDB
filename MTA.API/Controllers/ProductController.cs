using Microsoft.AspNetCore.Mvc;
using MTA.API.Services;
using MTA.Domain.Interfaces;
using MTA.Domain.Models;

namespace MTA.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService; 

    public ProductController(IProductService productService)
    {
        _productService = productService; // inject the products service
    }
   
    // Get list of products
    [HttpGet]
    public async Task<IActionResult> Get()
    {   
        var list = await _productService.GetAllProducts(); 
        return Ok(list);
    }
    
    //Get single product
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSingleProduct(Guid id)
    {
        var product = await _productService.GetProduct(id);
        return Ok(product);
    }

    // Create a new product
    [HttpPost]
    public async Task<IActionResult> Post(CreateProductRequest request)
    {
        var result = await _productService.CreateProduct(request);
        return Ok(result);
    }

    // Delete a product by id
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _productService.DeleteProduct(id);
        return Ok(result);
    }
}