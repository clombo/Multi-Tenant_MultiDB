using Microsoft.AspNetCore.Mvc;
using MTA.Domain.Interfaces;
using MTA.Domain.Models;

namespace MTA.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TenantController : ControllerBase
{
    private readonly ITenantService _tenantService;

    public TenantController(ITenantService tenantService)
    {
        _tenantService = tenantService;
    }

    //public ITenantService TenantService { get; }

    // Create a new tenant
    [HttpPost]
    public async Task<IActionResult> Post(CreateTenantRequest request)
    {
        var result = await _tenantService.CreateTenant(request);
        return Ok(result);
    }
}