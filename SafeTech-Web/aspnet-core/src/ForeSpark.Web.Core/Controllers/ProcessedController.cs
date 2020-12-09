using Abp.UI;
using ForeSpark.Configuration;
using ForeSpark.Processed;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ForeSpark.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ProcessedController : ForeSparkControllerBase
    {
        private readonly IProcessedAppService _processedAppService;
        private readonly IConfigurationRoot _appConfiguration;
        private string ImagesFolderPath = null;

        public ProcessedController(IProcessedAppService processedAppService, IWebHostEnvironment hostEnvironment)
        {
            _processedAppService = processedAppService;
            _appConfiguration = hostEnvironment.GetAppConfiguration();
            ImagesFolderPath = Path.Combine(_appConfiguration["Paths:PhysicalPath"], _appConfiguration["Paths:ProcessedPhysicalPath"]);
            Directory.CreateDirectory(ImagesFolderPath);
        }

        [HttpGet, Route("{requestId}/{processedImageName}")]
        public IActionResult GetImage(int requestId, string processedImageName)
        {
            var path = Path.Combine(ImagesFolderPath, requestId.ToString());
            var imageFile = Directory.EnumerateFiles(path, $"{processedImageName}.*", SearchOption.TopDirectoryOnly).FirstOrDefault();
            if (imageFile == null)
                throw new UserFriendlyException(L("ImageNotFound"));

            var image = System.IO.File.OpenRead(imageFile);
            new FileExtensionContentTypeProvider().TryGetContentType(imageFile, out var contentType);
            return File(image, contentType);
        }
    }
}
