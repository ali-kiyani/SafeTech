using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForeSpark.Request
{
    public class RequestStatus : Entity<int>
    {
        public RequestStatus(string status)
        {
            this.Status = status;
        }
        public string Status { get; set; }
    }
}
