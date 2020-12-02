using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageExport_V2.Services.Interfaces
{
    public interface ICommonServices
    {
        Task<string> CreateImage(IFormFile imageFile, string imageName, string saveFolder);
        Task<string> EditImage(IFormFile imageFile, string imageName, string saveFolder);
    }
}
