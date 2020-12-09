using Abp.Application.Services;
using ForeSpark.Home.Dto;
using System.Threading.Tasks;

namespace ForeSpark.Home
{
    public interface IHomeAppService : IApplicationService
    {
        Task<HomeDto> GetHomeMetaData();
    }
}
