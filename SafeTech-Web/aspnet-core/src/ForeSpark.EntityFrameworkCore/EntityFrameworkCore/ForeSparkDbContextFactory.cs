using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ForeSpark.Configuration;
using ForeSpark.Web;

namespace ForeSpark.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class ForeSparkDbContextFactory : IDesignTimeDbContextFactory<ForeSparkDbContext>
    {
        public ForeSparkDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ForeSparkDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            ForeSparkDbContextConfigurer.Configure(builder, configuration.GetConnectionString(ForeSparkConsts.ConnectionStringName));

            return new ForeSparkDbContext(builder.Options);
        }
    }
}
