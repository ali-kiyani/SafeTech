using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForeSpark.Home.Dto
{
    public class HomeDto : EntityDto<int>
    {
        public RequestsHomeDto RequestsHome { get; set; }
        public InstallationsHomeDto InstallationsHome { get; set; }
        public List<RequestsInsightHome> InsightHome { get; set; }

        public HomeDto()
        {
            RequestsHome = new RequestsHomeDto();
            InstallationsHome = new InstallationsHomeDto();
            InsightHome = new List<RequestsInsightHome>();
        }
    }
}
