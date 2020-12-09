using Abp.Application.Services;
using ForeSpark.MultiTenancy.Dto;

namespace ForeSpark.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

