namespace MTA.Domain.Models;

public class CreateProductRequest
{
    public required string Name { get; set; }
    public decimal Price { get; set; }
}