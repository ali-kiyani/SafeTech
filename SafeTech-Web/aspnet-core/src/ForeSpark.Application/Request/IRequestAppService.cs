using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ForeSpark.Request.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ForeSpark.Request
{
    public interface IRequestAppService : IAsyncCrudAppService<RequestDto, int, PagedRequestResultRequestDto, CreateRequestDto, RequestDto>
    {
        Task<RequestDetailsDto> GetRequestDetails(int id);
        Task<bool> AddRequestImagesForRequest(int RequestId, List<string> imagesNames);
        Task<bool> ApproveRequest(int requestId);
        Task<bool> DeclineRequest(int requestId);
    }
}
