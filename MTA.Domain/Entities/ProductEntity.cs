namespace MTA.Domain.Entities;

public class ProductEntity : IMustHaveTenant
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public required string TenantId { get; set; }
}