using System;
using System.Collections.Generic;
using System.Text;

namespace ForeSpark.Installations.Dto
{
    public class CreateInstallationsDto
    {
        public string Make { get; set; }
        public string Serial { get; set; }
        public int CityId { get; set; }
        public int Status { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Address { get; set; }
    }
}
