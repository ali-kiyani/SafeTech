using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using ForeSpark.Installations.Dto;
using ForeSpark.Request.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForeSpark.Installations
{
    public class InstallationsAppService : AsyncCrudAppService<Installations, InstallationsDto, int, PagedInstallationsResultRequestDto, CreateInstallationsDto, InstallationsDto>, IInstallationsAppService
    {
        private readonly IRepository<Installations> _installationsRepository;
        private readonly IRepository<Cities.Cities> _citiesRepository;

        public InstallationsAppService(IRepository<Installations> installationsRepository,
            IRepository<Cities.Cities> citiesRepository) : base(installationsRepository)
        {
            _installationsRepository = installationsRepository;
            _citiesRepository = citiesRepository;
        }

        public override async Task<InstallationsDto> CreateAsync(CreateInstallationsDto input)
        {
            //return base.CreateAsync(input);
            CheckCreatePermission();
            Installations installation = new Installations()
            {
                Make = input.Make,
                Serial = input.Serial,
                CityId = input.CityId,
                Address = input.Address,
                Lat = input.Lat,
                Lng = input.Lng,
                Status = input.Status
            };
            var installationId = await _installationsRepository.InsertAndGetIdAsync(installation);
            var installationDto = ObjectMapper.Map<InstallationsDto>(_installationsRepository.GetAllIncluding(x => x.City).Where(x => x.Id == installationId).FirstOrDefault());
            return installationDto;

        }

        public async Task<ListResultDto<CitiesDto>> GetAllCitiesAsync()
        {
            return new ListResultDto<CitiesDto>(ObjectMapper.Map<List<CitiesDto>>(await _citiesRepository.GetAllListAsync()));
        }

        public async Task<PagedResultDto<InstallationsDto>> GetPagedInstallations(PagedInstallationsResultRequestDto input)
        {
            CheckGetAllPermission();
            var allInstallations = _installationsRepository.GetAllIncluding(x => x.City)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Address.Contains(input.Keyword) || x.Make.Contains(input.Keyword) || x.Serial.Contains(input.Keyword))
                .WhereIf(input.CityId.HasValue && input.CityId.Value != 0, x => x.CityId == input.CityId);
            var pagedRequests = allInstallations.Skip(input.SkipCount).Take(input.MaxResultCount).OrderByDescending(x => x.Id).ToList();
            return await Task.FromResult(new PagedResultDto<InstallationsDto>(allInstallations.Count(), ObjectMapper.Map<List<InstallationsDto>>(pagedRequests)));
        }

        public ListResultDto<InstallationsDto> GetAllInstallations()
        {
            CheckGetAllPermission();
            return new ListResultDto<InstallationsDto>(ObjectMapper.Map<List<InstallationsDto>>(_installationsRepository.GetAllIncluding(x => x.City).ToList()));
        }

        public ListResultDto<InstallationsDto> GetActiveInstallations()
        {
            CheckGetAllPermission();
            return new ListResultDto<InstallationsDto>(ObjectMapper.Map<List<InstallationsDto>>(_installationsRepository.GetAllIncluding(x => x.City).Where(x => x.Status == (int)InstallationStatusEnum.ACTIVE)).ToList());
        }

        public ListResultDto<InstallationsDto> GetInactiveInstallations()
        {
            CheckGetAllPermission();
            return new ListResultDto<InstallationsDto>(ObjectMapper.Map<List<InstallationsDto>>(_installationsRepository.GetAllIncluding(x => x.City).Where(x => x.Status == (int)InstallationStatusEnum.INACTIVE)).ToList());
        }

        public ListResultDto<InstallationsDto> GetInstallationsForCity(int cityId)
        {
            CheckGetAllPermission();
            if (cityId == 0)
            {
                return new ListResultDto<InstallationsDto>(ObjectMapper.Map<List<InstallationsDto>>(_installationsRepository.GetAllIncluding(x => x.City)).ToList());
            }
            return new ListResultDto<InstallationsDto>(ObjectMapper.Map<List<InstallationsDto>>(_installationsRepository.GetAllIncluding(x => x.City).Where(x => x.CityId == cityId)).ToList());
        }

        public async Task ChangeStatus(int installationId, int status)
        {
            var installation = await _installationsRepository.GetAsync(installationId);
            installation.Status = status;
            await _installationsRepository.UpdateAsync(installation);
        }
    }
}
