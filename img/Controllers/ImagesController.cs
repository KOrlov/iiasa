using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using img.Services;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;

namespace img.Controllers
{
    [Route("api/images/")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private IImageService ImageService { get; set; }

        public ImagesController(IImageService imageService)
        {
            ImageService = imageService;
            
        }

        [Route("about")]
        public string About()
        {
            return "Images API";
        }

        [Route("upload")]
        [HttpPost]
        public string Upload()
        {
            try
            {
                byte[] buffer = new byte[Request.ContentLength ?? 0];
                var r = Request.Body.Read(buffer);
                return ImageService.Create(buffer);
            }
            catch
            {
                return "error";
            }

        }


        [Route("exif")]
        [HttpGet]
        public string Exif(string name)
        {
            try
            {
                return ImageService.Exif(name);
            }
            catch
            {
                return "error";
            }

        }
        [Route("enhance")]
        [HttpGet]
        public FileContentResult Enhance(string name)
        {
            try
            {

                return File(ImageService.Enhance(name), $"image/{ImageService.Format()}");
            }
            catch
            {
                return null;
            }

        }
        [Route("original")]
        [HttpGet]
        public FileContentResult Original(string name)
        {
            try
            {
                
                return File(ImageService.Original(name), $"image/{ImageService.Format()}");
            }
            catch
            {
                return null;
            }

        }

        [Route("resize")]
        [HttpGet]
        public FileContentResult Resize(int x, int y, string name)
        {
            try
            {
                return File(ImageService.Resize(x,y,name), $"image/{ImageService.Format()}");
            }
            catch
            {
                return null;
            }

        }


    }
}