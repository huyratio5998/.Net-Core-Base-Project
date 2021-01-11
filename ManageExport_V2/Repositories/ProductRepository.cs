using ManageExport_V2.Models;
using ManageExport_V2.Models.Entity;
using ManageExport_V2.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageExport_V2.Repositories
{
    public class ProductRepository : BaseRepository<Product>,IProductRepository
    {
        public ProductRepository(ExportContext context): base(context)
        {
           
        }
        public int? getNewId()
        {           
          return _context.Products?.OrderByDescending(x => x.Id)?.FirstOrDefault()?.Id;                       
        }
    }
}
