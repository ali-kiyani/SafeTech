using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ForeSpark.Authorization;

namespace ForeSpark
{
    [DependsOn(
        typeof(ForeSparkCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class ForeSparkApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<ForeSparkAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(ForeSparkApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
