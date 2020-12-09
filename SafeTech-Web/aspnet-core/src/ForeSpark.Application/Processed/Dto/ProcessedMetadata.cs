using ForeSpark.Request.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForeSpark.Processed.Dto
{
    public class ProcessedMetadata
    {
        public RequestDto Request { get; set; }
        public List<ProcessedDetailsDto> ProcessedDetails { get; set; }
    }
}
