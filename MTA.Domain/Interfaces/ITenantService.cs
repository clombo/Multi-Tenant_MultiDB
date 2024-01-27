using MTA.Domain.Entities;
using MTA.Domain.Models;

namespace MTA.Domain.Interfaces;

public interface ITenantService
{
    Task<TenantEntity> CreateTenant(CreateTenantRequest request);
}