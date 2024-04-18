using Refit;
using RefitClientTemplate.GeneratedModels;

namespace RefitClientGeneratedModels.Clients;

public interface ITenantApi
{
    [Post("/api/services/app/Tenant/Create")]
    Task<TenantDto> CreateAsync([Body] CreateTenantDto input);

    [Delete("/api/services/app/Tenant/Delete")]
    Task DeleteAsync([Query] int id);

    [Get("/api/services/app/Tenant/Get")]
    Task<TenantDto> GetAsync([Query] int id);

    [Get("/api/services/app/Tenant/GetAll")]
    Task<TenantDtoPagedResultDto> GetAllAsync([Query] string keyword, [Query] bool isActive, [Query] int skipCount, [Query] int maxResultCount);

    [Put("/api/services/app/Tenant/Update")]
    Task<TenantDto> UpdateAsync([Body] TenantDto input);
}
