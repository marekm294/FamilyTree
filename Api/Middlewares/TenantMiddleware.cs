using Api.Exceptions;
using Domain.DataServices.Abstraction;
using Domain.Services.Abstraction;

namespace Api.Middlewares;

public sealed class TenantMiddleware
{
    private readonly RequestDelegate _requestDelegate;

    // TODO: added memory cache
    public TenantMiddleware(RequestDelegate requestDelegate)
    {
        _requestDelegate = requestDelegate;
    }

    public async Task InvokeAsync(
        HttpContext context,
        ICurrentTenant currentTenant,
        ITenantService tenantService)
    {
        await AssignTenant(context, currentTenant, tenantService);
        await _requestDelegate(context);
    }

    private async Task AssignTenant(
        HttpContext context,
        ICurrentTenant currentTenant,
        ITenantService tenantService)
    {
        // TODO retreive tenant from token
        var tenantIdValue = context.Request.Headers["Tenant"].ToString();

        if (string.IsNullOrEmpty(tenantIdValue) ||
            !Guid.TryParse(tenantIdValue, out var tenantId))
        {
            throw new InvalidTenantIdException();
        }

        var tenant = await tenantService.GetTenantByIdAsync(tenantId);
        if (tenant is null)
        {
            throw new InvalidTenantIdException();
        }

        if (tenant.IsActive is false)
        {
            throw new TenantDeactivatedException();
        }

        currentTenant.TenantId = tenantId;
    }
}
