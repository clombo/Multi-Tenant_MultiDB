namespace MTA.Domain.Entities;

public interface IMustHaveTenant
{
    public string TenantId { get; set; }
}