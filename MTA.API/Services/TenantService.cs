using Microsoft.EntityFrameworkCore;
using MTA.Data.Contexts;
using MTA.Domain.Entities;
using MTA.Domain.Interfaces;
using MTA.Domain.Models;

namespace MTA.API.Services;

public class TenantService : ITenantService
{
    private readonly TenantDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly IServiceProvider _serviceProvider;

    public TenantService(TenantDbContext context, IConfiguration configuration, IServiceProvider serviceProvider)
    {
        _context = context;
        _configuration = configuration;
        _serviceProvider = serviceProvider;
    }

    public async Task<TenantEntity> CreateTenant(CreateTenantRequest request)
    {
        string newConnectionString = null;
        
        // generate a connection string for new tenant database
        string dbName = "MTAMulti-" + request.Id;
        string defaultConnectionString = _configuration.GetConnectionString("MTA");
        newConnectionString = defaultConnectionString.Replace("MTAMulti", dbName);

        // create a new tenant database and bring current with any pending migrations from ApplicationDbContext
        try
        {
            using IServiceScope scopeTenant = _serviceProvider.CreateScope();
            AppDbContext dbContext =   scopeTenant.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.SetConnectionString(newConnectionString);
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Applying ApplicationDB Migrations for New '{request.Id}' tenant.");
                Console.ResetColor();
                dbContext.Database.Migrate();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }


        TenantEntity tenant = new() // create a new tenant entity
        {
            Id = request.Id,
            Name = request.Name,
            ConnectionString = newConnectionString,
        };

        await _context.AddAsync(tenant);
        await _context.SaveChangesAsync();

        return tenant;
    }
}