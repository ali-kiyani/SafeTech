using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ForeSpark.Request
{
    public class RequestImages : Entity<int>
    {
        public Request Request { get; set; }
        [ForeignKey("Request")]
        public int RequestId { get; set; }
        public string Image { get; set; }
    }
}
