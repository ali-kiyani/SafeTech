using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ForeSpark.EntityFrameworkCore;
using ForeSpark.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace ForeSpark.Web.Tests
{
    [DependsOn(
        typeof(ForeSparkWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class ForeSparkWebTestModule : AbpModule
    {
        public ForeSparkWebTestModule(ForeSparkEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ForeSparkWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(ForeSparkWebMvcModule).Assembly);
        }
    }
}