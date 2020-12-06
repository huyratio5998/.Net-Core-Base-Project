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

        public UnitOfWork(ExportContext context, IUserRepository users)
        {
            _context = context;
            Users = users;
        }

        public IUserRepository Users { get; }
                               
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
