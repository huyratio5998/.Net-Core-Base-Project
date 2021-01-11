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
        private ICommonServices _commonServices;

        public ProductServices(IUnitOfWork unitOfWork, ICommonServices commonServices)
        {
            _unitOfWork = unitOfWork;
            _commonServices = commonServices;
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
        public async Task<Product> GetProductById(int id,string[] includes=null)
        {
            try
            {
                var product= await _unitOfWork.Products.GetSingleByCondition(x => x.Id == id, includes);
                product.MFG = product.MFG.ToLocalTime();
                product.EXP = product.EXP.ToLocalTime();
                product.RecieveDate = product.RecieveDate.ToLocalTime();
                product.CreatedDate = product.CreatedDate.ToLocalTime();
                product.ModifiedDate = product.ModifiedDate.ToLocalTime();
                return product;
            }
            catch(Exception e)
            {
                throw e;
            }
        }      
        public Task<IQueryable<Product>> GetProducts(string[] include=null)
        {
            return _unitOfWork.Products.GetMulti(x => x.IsActive,include);                                 
        }

        public async Task CreateProduct(Product product)
        {
            try
            {
                int NewestID = _unitOfWork.Products.getNewId().Equals(null) ? 0 : (int)_unitOfWork.Products.getNewId();
                product.Code = $"P_{NewestID + 1}";
                product.MainImage = await _commonServices.CreateImage(product.ImageFile, product.MainImage, "/images/Products/" + product.Name);
                product.MFG = product.MFG.ToUniversalTime();
                product.EXP = product.EXP.ToUniversalTime();
                product.RecieveDate = product.RecieveDate.ToUniversalTime();
                product.CreatedDate = product.ModifiedDate = DateTime.UtcNow;
                product.IsActive = true;
                _unitOfWork.Products.Add(product);
                await _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task UpdateProduct(Product product)
        {
            try
            {
                product.MainImage = await _commonServices.EditImage(product.ImageFile, product.MainImage, "/images/Products/" + product.Name);
                product.MFG = product.MFG.ToUniversalTime();
                product.EXP = product.EXP.ToUniversalTime();
                product.RecieveDate = product.RecieveDate.ToUniversalTime();
                product.ModifiedDate = DateTime.UtcNow;
                await _unitOfWork.Products.Update(product);
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
