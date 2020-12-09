using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ForeSpark.Request.Dto;

namespace ForeSpark.Installations.Dto
{
    [AutoMap(typeof(Installations))]
    public class InstallationsDto : EntityDto<int>
    {
        public string Make { get; set; }
        public string Serial { get; set; }
        public CitiesDto City { get; set; }
        public int Status { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Address { get; set; }
    }
}
