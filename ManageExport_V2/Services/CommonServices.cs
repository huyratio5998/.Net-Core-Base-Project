using ManageExport_V2.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ManageExport_V2.Services
{
    public class CommonServices : ICommonServices
    {
        private readonly IWebHostEnvironment _hostEnvironment;        
        public CommonServices(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }
        public async Task<string> CreateImage(IFormFile imageFile,string imageName,string saveFolder)
        {
            try
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (imageFile != null)
                {
                    imageName = imageFile.FileName;
                    //create save folder if not exist                                        
                    if (!Directory.Exists(wwwRootPath+saveFolder))
                    {
                        Directory.CreateDirectory(wwwRootPath+saveFolder);
                    }                    
                    string path = Path.Combine(wwwRootPath + saveFolder, imageName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }
                }                
                return imageName;
            }
            catch(Exception e)
            {
                throw e;
            }
           
        }
        public async Task<string> EditImage(IFormFile imageFile,string imageName, string saveFolder)
        {

            try
            {
                if (imageFile != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    var imagePath = Path.Combine(wwwRootPath+saveFolder, imageFile.FileName);
                    if (!System.IO.File.Exists(imagePath))
                    {      
                        // create image when path not exist
                        imageName = imageFile.FileName;
                        if (!Directory.Exists(wwwRootPath + saveFolder))
                        {
                            Directory.CreateDirectory(wwwRootPath + saveFolder);
                        }
                        string path = Path.Combine(wwwRootPath + saveFolder, imageName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(fileStream);
                        }
                    }
                    else
                    {
                        imageName = imageFile.FileName;
                    }
                }
                return imageName;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
