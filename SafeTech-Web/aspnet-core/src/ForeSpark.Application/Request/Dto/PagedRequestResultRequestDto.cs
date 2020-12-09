using Abp.Application.Services.Dto;

namespace ForeSpark.Request.Dto
{
    public class PagedRequestResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Status { get; set; }
        public int? CityId { get; set; }
    }
}
