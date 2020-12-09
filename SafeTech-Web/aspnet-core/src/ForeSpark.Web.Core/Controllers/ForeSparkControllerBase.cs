using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace ForeSpark.Controllers
{
    public abstract class ForeSparkControllerBase: AbpController
    {
        protected ForeSparkControllerBase()
        {
            LocalizationSourceName = ForeSparkConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
