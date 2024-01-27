using Microsoft.EntityFrameworkCore;
using MTA.Data.Contexts;
using MTA.Domain.Interfaces;

namespace MTA.API.Services;

public class CurrentTenant : ICurrentTenant
{
    public string? TenantId { get; set; }
    public string? TenantConnectionString { get; set; }
    private readonly TenantDbContext _context;

    public CurrentTenant(TenantDbContext context)
    {
        _context = context;
    }

    public async Task<bool> SetTenant(string tenant)
    {
        var tenantInfo = await _context.Tenants.Where(x => x.Id == tenant).FirstOrDefaultAsync(); // check if tenant exists
        if (tenantInfo != null)
        {
            TenantId = tenantInfo.Id;
            TenantConnectionString = tenantInfo.ConnectionString;
            return true;
        }
        else
        {
            throw new Exception("Tenant invalid"); 
        }
    }
}