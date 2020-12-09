using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForeSpark.Request.Dto
{
    [AutoMap(typeof(Cities.Cities))]
    public class CitiesDto : EntityDto<int>
    {
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}
