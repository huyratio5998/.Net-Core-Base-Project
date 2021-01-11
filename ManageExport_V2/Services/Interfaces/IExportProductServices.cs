using ManageExport_V2.Models;
using ManageExport_V2.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageExport_V2.Services.Interfaces
{
    public interface IExportProductServices
    {
        Task<IQueryable<ExportProductBill>> ExportProduct(string[] includes = null);
        bool AddExportProduct(ExportProductViewModel exportProductViewModel);
    }
}
