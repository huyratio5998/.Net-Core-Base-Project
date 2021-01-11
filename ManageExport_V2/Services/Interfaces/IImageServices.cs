using ManageExport_V2.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ManageExport_V2.Models.Entity;
namespace ManageExport_V2.Services.Interfaces
{
    public interface IImageServices
    {
        Task<IEnumerable<Image>> GetImages();

    }
}
