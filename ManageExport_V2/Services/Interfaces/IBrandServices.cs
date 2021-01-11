using ManageExport_V2.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ManageExport_V2.Models.Entity;
using System.Linq;

namespace ManageExport_V2.Services.Interfaces
{
    public interface IBrandServices
    {
        Task<IQueryable<Brand>> GetBrand();
        Task<Brand> GetBrandById(int Id);
    }
}
