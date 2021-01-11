using ManageExport_V2.Services.Interfaces;
using System;
using System.Threading.Tasks;
using ManageExport_V2.Models.Entity;
using ManageExport_V2.Repositories.Interfaces;
using System.Collections.Generic;

namespace ManageExport_V2.Services
{
    public class ImageServices : IImageServices
    {
        private IUnitOfWork _unitOfWork;
        public ImageServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Image>> GetImages()
        {
            try
            {
                var images = await _unitOfWork.ImageRepository.GetAll();
                return images;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
