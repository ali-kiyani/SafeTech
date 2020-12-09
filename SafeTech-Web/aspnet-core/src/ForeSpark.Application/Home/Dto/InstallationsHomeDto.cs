using System;
using System.Collections.Generic;
using System.Text;

namespace ForeSpark.Home.Dto
{
    public class InstallationsHomeDto
    {
        public int InstallationsTotal { get; set; }
        public int InstallationsActive { get; set; }
        public int InstallationsInactive { get; set; }
        public int InstallationsMalfunction { get; set; }
    }
}
