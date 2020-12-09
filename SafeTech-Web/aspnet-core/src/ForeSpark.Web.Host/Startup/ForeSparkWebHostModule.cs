using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ForeSpark.Configuration;

namespace ForeSpark.Web.Host.Startup
{
    [DependsOn(
       typeof(ForeSparkWebCoreModule))]
    public class ForeSparkWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public ForeSparkWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ForeSparkWebHostModule).GetAssembly());
        }
    }
}
