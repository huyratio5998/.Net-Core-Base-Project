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

        public Task<IQueryable<Product>> GetProducts()
        {
            return _unitOfWork.Products.GetMulti(x => x.IsActive);
        }

        public async Task CreateProduct(Product user)
        {
            try
            {
                _unitOfWork.Products.Add(user);
                await _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task UpdateProduct(Product user)
        {
            try
            {
                _unitOfWork.Products.Add(user);
                await _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task DeleteProductById(int id)
        {
            try
            {
                _unitOfWork.Products.Delete(id);
                await _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task DeleteVirtual(int id)
        {
            var entity = await _unitOfWork.Products.GetSingleById(id);
            entity.IsActive = false;
            entity.ModifiedDate = DateTime.UtcNow;
            await _unitOfWork.Products.Update(entity);
            await _unitOfWork.Commit();
        }       
    }
}
