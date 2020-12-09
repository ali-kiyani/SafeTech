using System.Threading.Tasks;
using Abp.Application.Services;
using ForeSpark.Sessions.Dto;

namespace ForeSpark.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
