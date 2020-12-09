
using Abp.Domain.Repositories;
using ForeSpark.Home.Dto;
using ForeSpark.Installations;
using ForeSpark.Request;
using ForeSpark.Request.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ForeSpark.Home
{
    public class HomeAppService : ForeSparkAppServiceBase, IHomeAppService
    {
        private readonly IRepository<Request.Request> _requestRepository;
        private readonly IRepository<Cities.Cities> _citiesRepository;
        private readonly IRepository<Installations.Installations> _installationsRepository;
         public HomeAppService(IRepository<Request.Request> requestRepository, 
             IRepository<Installations.Installations> installationsRepository,
             IRepository<Cities.Cities> citiesRepository)
        {
            _requestRepository = requestRepository;
            _installationsRepository = installationsRepository;
            _citiesRepository = citiesRepository;
        }
        public async Task<HomeDto> GetHomeMetaData()
        {
            HomeDto homeDto = new HomeDto();
            homeDto.RequestsHome.Pending = (await _requestRepository.CountAsync(x => x.StatusId == (int)RequestStatusEnum.PENDING));
            homeDto.RequestsHome.Approved = (await _requestRepository.CountAsync(x => x.StatusId == (int)RequestStatusEnum.APPROVED));
            homeDto.RequestsHome.Declined = (await _requestRepository.CountAsync(x => x.StatusId == (int)RequestStatusEnum.DECLINED));
            homeDto.RequestsHome.Processed = (await _requestRepository.CountAsync(x => x.StatusId == (int)RequestStatusEnum.PROCESSED));

            homeDto.InstallationsHome.InstallationsActive = (await _installationsRepository.CountAsync(x => x.Status == (int)InstallationStatusEnum.ACTIVE));
            homeDto.InstallationsHome.InstallationsInactive = (await _installationsRepository.CountAsync(x => x.Status == (int)InstallationStatusEnum.INACTIVE));
            homeDto.InstallationsHome.InstallationsMalfunction = (await _installationsRepository.CountAsync(x => x.Status == (int)InstallationStatusEnum.MALFUNCTION));
            homeDto.InstallationsHome.InstallationsTotal = await _installationsRepository.CountAsync();

            var insights = _requestRepository.GetAllIncluding(x => x.City).ToList()
                    .GroupBy(x => x.CityId) 
                    .Select(x => new RequestsInsightHome
                    {
                        RequestsCount = x.Count(),
                        City = ObjectMapper.Map<CitiesDto>(_citiesRepository.Get(x.First().CityId))
                    })
                    .ToList();
                homeDto.InsightHome = insights;


            return homeDto;

        }
    }
}
