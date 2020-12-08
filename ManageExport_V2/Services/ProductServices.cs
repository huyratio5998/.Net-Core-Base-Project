using ManageExport_V2.Models;
using ManageExport_V2.Models.Entity;
using ManageExport_V2.Repositories;
using ManageExport_V2.Repositories.Interfaces;
using ManageExport_V2.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ManageExport_V2.Services
{
    public class ProductServices :IProductServices
    {
        private IUnitOfWork _unitOfWork;
        public ProductServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<IQueryable<Product>> GetProducts(string str)
        {
            try
            {
                return _unitOfWork.Products.GetMulti(x => x.DisplayName.Contains(str) || x.Brand.ShortName.Contains(str));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Task<Product> GetProductById(int id)
        {
            try
            {
                return _unitOfWork.Products.GetSingleById(id);
            }catch(Exception e)
            {
                throw e;
            }
        }
        
    }
}
