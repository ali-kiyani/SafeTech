using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForeSpark.Request.Dto
{
    [AutoMap(typeof(RequestStatus))]
    public class RequestStatusDto : EntityDto<int>
    {
        public string Status { get; set; }
    }
}
