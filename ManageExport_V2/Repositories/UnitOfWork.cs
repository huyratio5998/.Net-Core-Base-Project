using ManageExport_V2.Models;
using ManageExport_V2.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageExport_V2.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ExportContext _context;

        public UnitOfWork(ExportContext context, IUserRepository users, IProductRepository products)
        {
            _context = context;
            Users = users;
            Products = products;
        }

        public IUserRepository Users { get; }

        public IProductRepository Products { get; }
        public IExportListDetailRepository ExportListDetailRepositorys { get; }
        public IExportProductBillRepository ExportProductBillRepositorys { get; }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
