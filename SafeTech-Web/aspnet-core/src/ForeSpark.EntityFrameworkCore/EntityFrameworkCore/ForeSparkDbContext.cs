using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using ForeSpark.Authorization.Roles;
using ForeSpark.Authorization.Users;
using ForeSpark.MultiTenancy;
using ForeSpark.Request;

namespace ForeSpark.EntityFrameworkCore
{
    public class ForeSparkDbContext : AbpZeroDbContext<Tenant, Role, User, ForeSparkDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<RequestStatus> RequestStatuses { get; set; }
        public DbSet<Request.Request> Requests { get; set; }
        public DbSet<RequestImages> RequestImages { get; set; }
        public DbSet<Cities.Cities> Cities { get; set; }
        public DbSet<Installations.Installations> Installations { get; set; }
        public DbSet<Processed.Processed> Processed { get; set; }
        public ForeSparkDbContext(DbContextOptions<ForeSparkDbContext> options)
            : base(options)
        {
        }
    }
}
