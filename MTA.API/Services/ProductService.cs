using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MTA.Data.Contexts;
using MTA.Domain.Entities;
using MTA.Domain.Interfaces;
using MTA.Domain.Models;

namespace MTA.API.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    
    public ProductService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ProductEntity>> GetAllProducts()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<ProductEntity> GetProduct(Guid id)
    {
        return await _context.Products.FirstOrDefaultAsync(w => w.Id == id);
    }

    
    public async Task<ProductEntity> CreateProduct(CreateProductRequest product)
    {
        var newProduct = _mapper.Map<ProductEntity>(product);
        await _context.AddAsync(newProduct);
        await _context.SaveChangesAsync();

        return newProduct;
    }

    public async Task<bool> DeleteProduct(Guid id)
    {
        var productToDelete = await _context.Products.FirstOrDefaultAsync(w => w.Id == id);

        if (productToDelete == null)
            return false;

        _context.Remove(productToDelete);
        await _context.SaveChangesAsync();

        return true;
    }
}