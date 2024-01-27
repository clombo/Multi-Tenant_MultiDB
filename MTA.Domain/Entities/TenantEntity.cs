namespace MTA.Domain.Entities;

public class TenantEntity
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string ConnectionString { get; set; }
}