namespace MTA.Domain.Interfaces;

public interface ICurrentTenant
{
    string? TenantId { get; set; }
    string? TenantConnectionString { get; set; } 
    public Task<bool> SetTenant(string tenant);
}