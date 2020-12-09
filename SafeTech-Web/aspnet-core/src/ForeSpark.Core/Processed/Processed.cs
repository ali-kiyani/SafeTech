using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForeSpark.Processed
{
    public class Processed : Entity<int>
    {
        public Request.Request Request { get; set; }

        [ForeignKey("Request")]
        public int RequestId { get; set; }
        public Installations.Installations Installations { get; set; }

        [ForeignKey("Installations")]
        public int InstallationId { get; set; }
        public DateTime InVisionTime { get; set; }
        public string FileName { get; set; }
    }
}
