using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForeSpark.Cities
{
    public class Cities : Entity<int>
    {
        public Cities(string Name, double Lat, double Lng)
        {
            this.Name = Name;
            this.Lat = Lat;
            this.Lng = Lng;
        }

        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}
