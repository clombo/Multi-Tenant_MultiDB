using MTA.API.Services;
using MTA.Domain.Interfaces;

namespace MTA.API.Middleware;

public class TenantHeaderResolver
{
    private readonly RequestDelegate _next;

    public TenantHeaderResolver(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ICurrentTenant currentTenant)
    {
        context.Request.Headers.TryGetValue("tenant", out var tenantFromHeader); // Tenant Id from incoming request header
        if (string.IsNullOrEmpty(tenantFromHeader) == false)
        {
            await currentTenant.SetTenant(tenantFromHeader);
        }
        await _next(context);
    }
}