using Abp.UI;
using ForeSpark.Configuration;
using ForeSpark.Request;
using ForeSpark.Request.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NUglify.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ForeSpark.Controllers
{
    [Route("api/[controller]/[action]")]
    public class RequestController : ForeSparkControllerBase
    {
        private readonly IRequestAppService _requestAppService;
        private readonly string requestImagePrefix = "request_";
        private readonly IConfigurationRoot _appConfiguration;
        private string ImagesFolderPath = null;

        public RequestController(IRequestAppService requestAppService, IWebHostEnvironment hostEnvironment)
        {
            _requestAppService = requestAppService;
            _appConfiguration = hostEnvironment.GetAppConfiguration();
            ImagesFolderPath = Path.Combine(_appConfiguration["Paths:PhysicalPath"], _appConfiguration["Paths:RequestsImagesFolder"]);
            Directory.CreateDirectory(ImagesFolderPath);
        }

        [HttpPost]
        public async Task<RequestDto> CreateNewRequestAsync()
        {
            var model = HttpContext.Request.Form["requestForm"];
            var createrequestDto = JsonConvert.DeserializeObject<CreateRequestDto>(model);
            RequestDto request = await _requestAppService.CreateAsync(createrequestDto);
            var requestImages = HttpContext.Request.Form.Files;
            string path = Path.Combine(ImagesFolderPath, request.Id.ToString());
            Directory.CreateDirectory(path); ;
            int i = 1;
            List<string> imagesNameList = new List<string>();
            requestImages.ForEach(async (image) =>
            {
                if (image.Length > 0)
                {
                    var nameSplits = image.FileName.Split(".");
                    var type = nameSplits[nameSplits.Length - 1];
                    var imageName = requestImagePrefix + request.Id + "_" + i + "." + type;
                    var filePath = Path.Combine(path, imageName);
                    i++;
                    imagesNameList.Add(imageName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }
                }
            });
            await _requestAppService.AddRequestImagesForRequest(request.Id, imagesNameList);
            return request;
        }

        [HttpGet, Route("{requestId}/{requestImageName}")]
        public IActionResult GetImage(int requestId, string requestImageName)
        {
            var path = Path.Combine(ImagesFolderPath, requestId.ToString());
            var imageFile = Directory.EnumerateFiles(path, $"{requestImageName}.*", SearchOption.TopDirectoryOnly).FirstOrDefault();
            if (imageFile == null)
                throw new UserFriendlyException(L("ImageNotFound"));

            var image = System.IO.File.OpenRead(imageFile);
            new FileExtensionContentTypeProvider().TryGetContentType(imageFile, out var contentType);
            return File(image, contentType);
        }
    }
}
