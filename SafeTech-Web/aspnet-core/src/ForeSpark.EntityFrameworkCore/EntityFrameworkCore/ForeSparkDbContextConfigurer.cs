using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace ForeSpark.EntityFrameworkCore
{
    public static class ForeSparkDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<ForeSparkDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<ForeSparkDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
