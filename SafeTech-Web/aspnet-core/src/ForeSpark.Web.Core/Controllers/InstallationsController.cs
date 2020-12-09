using ForeSpark.Installations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForeSpark.Controllers
{
    [Route("api/[controller]/[action]")]
    public class InstallationsController : ForeSparkControllerBase
    {
        private readonly IInstallationsAppService _installationsAppService;

        public InstallationsController(IInstallationsAppService installationsAppService)
        {
            _installationsAppService = installationsAppService;
        }
    }
}
