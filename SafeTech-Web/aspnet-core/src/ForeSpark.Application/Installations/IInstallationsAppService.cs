using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ForeSpark.Installations.Dto;
using ForeSpark.Request.Dto;
using System.Threading.Tasks;

namespace ForeSpark.Installations
{
    public interface IInstallationsAppService : IAsyncCrudAppService<InstallationsDto, int, PagedInstallationsResultRequestDto, CreateInstallationsDto, InstallationsDto>
    {
        Task<ListResultDto<CitiesDto>> GetAllCitiesAsync();
    }
}
