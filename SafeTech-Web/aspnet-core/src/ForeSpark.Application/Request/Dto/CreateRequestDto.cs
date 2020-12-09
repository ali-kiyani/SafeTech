using System;
using System.ComponentModel.DataAnnotations;

namespace ForeSpark.Request.Dto
{
    public class CreateRequestDto
    {
        [Required]
        public string CNIC { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int CityId { get; set; }
    }
}
