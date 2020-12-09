using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ForeSpark.Installations.Dto;
using ForeSpark.Request.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForeSpark.Processed.Dto
{
    [AutoMap(typeof(Processed))]
    public class ProcessedDto : EntityDto<int>
    {
        public RequestDto Request { get; set; }
    }
}
