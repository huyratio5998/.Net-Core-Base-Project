using ManageExport_V2.Models;
using ManageExport_V2.Models.Entity;
using ManageExport_V2.Services.Interfaces;
using System;

namespace ManageExport_V2.Services
{
    public class ExportProductServices : IExportProductServices
    {
        private readonly ExportContext _context;
        public ExportProductServices(ExportContext context)
        {
            _context = context;
        }

        public ExportProductViewModel ExportProduct()
        {
            return new ExportProductViewModel();
        }
       
    }
}
