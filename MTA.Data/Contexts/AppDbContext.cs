using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MTA.Domain.Entities;
using MTA.Domain.Interfaces;

namespace MTA.Data.Contexts;

public class AppDbContext : DbContext
{
    private readonly ICurrentTenant _currentTenantService;
    private  string CurrentTenantId { get; set; }
    private string CurrentTenantConnectionString { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options, ICurrentTenant currentTenantService) : base(options)
    {
        _currentTenantService = currentTenantService;
        CurrentTenantId = _currentTenantService.TenantId;
        CurrentTenantConnectionString = _currentTenantService.TenantConnectionString;
    }
    
    public DbSet<ProductEntity> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.Entity<ProductEntity>().HasQueryFilter(a => a.TenantId == CurrentTenantId);
    }

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries<IMustHaveTenant>().ToList()) 
        {
            switch (entry.State)
            {
                case EntityState.Added:
                case EntityState.Modified:
                    entry.Entity.TenantId = CurrentTenantId; 
                    break;
            }
        }
        var result = base.SaveChanges();
        return result;
    }
    
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<IMustHaveTenant>().ToList()) 
        {
            switch (entry.State)
            {
                case EntityState.Added:
                case EntityState.Modified:
                    entry.Entity.TenantId = CurrentTenantId; 
                    break;
            }
        }
        
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
    
    // On Configuring -- dynamic connection string, fires on every request
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string tenantConnectionString = CurrentTenantConnectionString;
        if (!string.IsNullOrEmpty(tenantConnectionString)) // use tenant db if one is specified
        {
            _ = optionsBuilder.UseSqlServer(tenantConnectionString);
        }
    }
}