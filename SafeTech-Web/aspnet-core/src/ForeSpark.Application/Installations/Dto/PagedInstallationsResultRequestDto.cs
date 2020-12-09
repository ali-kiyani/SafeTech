using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForeSpark.Installations.Dto
{
    public class PagedInstallationsResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public int? CityId { get; set; }
    }
}
