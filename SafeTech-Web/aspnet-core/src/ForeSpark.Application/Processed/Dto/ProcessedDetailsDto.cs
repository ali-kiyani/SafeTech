using Abp.AutoMapper;
using ForeSpark.Installations.Dto;
using ForeSpark.Request.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForeSpark.Processed.Dto
{
    [AutoMap(typeof(Processed))]
    public class ProcessedDetailsDto
    {
        public InstallationsDto Installations { get; set; }
        public DateTime InVisionTime { get; set; }
        public string FileName { get; set; }
    }
}
