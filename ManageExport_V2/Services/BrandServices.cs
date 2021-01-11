using ManageExport_V2.Repositories.Interfaces;
using ManageExport_V2.Services.Interfaces;
using System;
using System.Threading.Tasks;
using ManageExport_V2.Models.Entity;
using System.Collections.Generic;
using System.Linq;

namespace ManageExport_V2.Services
{
    public class BrandServices : IBrandServices
    {
        private IUnitOfWork _unitOfWork;
        public BrandServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IQueryable<Brand>> GetBrand()
        {
            try
            {
                var brands = await _unitOfWork.BrandRepository.GetAll();
                return brands;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Brand> GetBrandById(int Id)
        {
            return await _unitOfWork.BrandRepository.GetSingleById(Id);
        }
    }
}
