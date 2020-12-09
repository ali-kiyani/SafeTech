using System;
using System.Collections.Generic;
using System.Text;

namespace ForeSpark.Home.Dto
{
    public class RequestsHomeDto
    {
        public int Pending { get; set; }
        public int Approved { get; set; }
        public int Declined { get; set; }
        public int Processed { get; set; }
    }
}
