using ManageExport_V2.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageExport_V2.Services.Interfaces
{
    public interface IProductServices
    {
        Task<IQueryable<Product>> GetProducts();
        Task<IQueryable<Product>> GetProducts(string str);
        Task<Product> GetProductById(int id);
        Task CreateProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProductById(int id);
        Task DeleteVirtual(int id);
    }
}
