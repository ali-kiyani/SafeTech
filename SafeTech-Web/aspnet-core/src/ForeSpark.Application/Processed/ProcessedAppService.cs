using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using ForeSpark.Processed.Dto;
using ForeSpark.Request.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeSpark.Processed
{
    public class ProcessedAppService : AsyncCrudAppService<Processed, ProcessedDto, int, PagedProcessedResultRequestDto, ProcessedDto, ProcessedDto>, IProcessedAppService
    {
        private readonly IRepository<Processed> _processedRepository;

        public ProcessedAppService(IRepository<Processed> processedRepository) : base(processedRepository)
        {
            _processedRepository = processedRepository;
        }

        public ProcessedMetadata GetProcessedMetadata(int id)
        {
            var processed = _processedRepository.GetAll().Where(x => x.RequestId == id).Include(x => x.Request).Include(x => x.Installations).ThenInclude(x => x.City).ToList();
            ProcessedMetadata details = new ProcessedMetadata();
            details.Request = ObjectMapper.Map<RequestDto>(processed.FirstOrDefault().Request);
            details.ProcessedDetails = ObjectMapper.Map<List<ProcessedDetailsDto>>(processed);
            return details;
        }

        public override Task<PagedResultDto<ProcessedDto>> GetAllAsync(PagedProcessedResultRequestDto input)
        {
            CheckGetAllPermission();
            var allProcessed = _processedRepository.GetAll().Include(x => x.Request).ThenInclude(x => x.City)
                                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.RequestId.ToString().Contains(input.Keyword)
                || x.Request.Name.Contains(input.Keyword) || x.Request.Address.Contains(input.Keyword)
                || x.Request.Address.Contains(input.Keyword) || x.Request.CNIC.Contains(input.Keyword))
                .WhereIf(input.CityId.HasValue && input.CityId.Value != 0, x => x.Request.CityId == input.CityId).ToList().Distinct(new ProcessedComparer());
            var pagedProcessed = allProcessed.Skip(input.SkipCount).Take(input.MaxResultCount).OrderByDescending(x => x.Id).ToList();
            return Task.FromResult(new PagedResultDto<ProcessedDto>(allProcessed.Count(), ObjectMapper.Map<List<ProcessedDto>>(pagedProcessed)));
        }

        protected override IQueryable<Processed> CreateFilteredQuery(PagedProcessedResultRequestDto input)
        {
            return Repository.GetAllIncluding()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.RequestId.ToString().Contains(input.Keyword));
        }

        protected override IQueryable<Processed> ApplySorting(IQueryable<Processed> query, PagedProcessedResultRequestDto input)
        {
            return query.OrderByDescending(order => order.Id);
        }
    }
}
