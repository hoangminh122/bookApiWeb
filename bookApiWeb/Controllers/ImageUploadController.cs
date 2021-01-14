using bookApiWeb.Models.Files;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace bookApiWeb.Controllers
{
    [Route("api/[controller]")]
    [Controller]
    public class ImageUploadController :ControllerBase
    {
        public static IHostingEnvironment _enviroment;
        public ImageUploadController(IHostingEnvironment enviroment)
        {
            _enviroment = enviroment;
        }
        [HttpPost]
        //[FromForm(Name = "UploadFile1")]
        public async Task<string> PostFile(FileUploadAPI objFile)
        {
            if(objFile.files.Length > 0)
            {
                if(!Directory.Exists(_enviroment.ContentRootPath +"\\Upload\\"))
                {
                    Directory.CreateDirectory(_enviroment.ContentRootPath + "\\Upload\\");
                }
                using (FileStream fileStream = System.IO.File.Create(_enviroment.ContentRootPath + "\\Upload\\"+objFile.files.FileName))
                {
                    objFile.files.CopyTo(fileStream);
                    fileStream.Flush();
                    return objFile.files.FileName;
                }
            }
            else
            {
                return "Failed";
            }
        }
    }
}
