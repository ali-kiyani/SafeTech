using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForeSpark.EntityFrameworkCore.Seed.RequestStatus
{
    public class RequestStatusBuilder
    {
        private readonly ForeSparkDbContext _context;

        public RequestStatusBuilder(ForeSparkDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateDefaultRequestStatus();
        }

        private void CreateDefaultRequestStatus()
        {
            if (_context.RequestStatuses.Count() == 0)
            {
                _context.RequestStatuses.Add(new Request.RequestStatus("PENDING"));
                _context.RequestStatuses.Add(new Request.RequestStatus("APPROVED"));
                _context.RequestStatuses.Add(new Request.RequestStatus("DECLINED"));
                _context.RequestStatuses.Add(new Request.RequestStatus("PROCESSED"));
                _context.SaveChanges();
            }
        }
    }
}
