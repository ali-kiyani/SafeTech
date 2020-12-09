using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ForeSpark.Processed.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ForeSpark.Processed
{
    public interface IProcessedAppService : IAsyncCrudAppService<ProcessedDto, int, PagedProcessedResultRequestDto, ProcessedDto, ProcessedDto>
    {
        ProcessedMetadata GetProcessedMetadata(int id);
    }
}
