using MTA.Domain.Entities;
using MTA.Domain.Models;

namespace MTA.Domain.Interfaces;

public interface IProductService
{
    Task<List<ProductEntity>> GetAllProducts();
    Task<ProductEntity> GetProduct(Guid id);
    Task<ProductEntity> CreateProduct(CreateProductRequest product);
    Task<bool> DeleteProduct(Guid id);
}