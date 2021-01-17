using ManageExport_V2.Models;
using ManageExport_V2.Models.Entity;
using ManageExport_V2.Repositories.Interfaces;
using ManageExport_V2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageExport_V2.Services
{
    public class ExportProductServices : IExportProductServices
    {
        private IUnitOfWork _unitOfWork;
        public ExportProductServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IQueryable<ExportProductBill>> ExportProduct(string[] includes = null)
        {
           return  _unitOfWork.ExportProductBillRepositorys.GetAll(includes);            
        }
        public async Task<bool> AddExportProduct(ExportProductViewModel exportProductViewModel)
        {
            try
            {
                ExportProductBill exportProductBill = new ExportProductBill();
                // add exportProductBill
                exportProductBill.TotalMoney = exportProductViewModel.TotalMoney;
                exportProductBill.ExportDate = DateTime.UtcNow;
                exportProductBill.ExportManagerId = exportProductViewModel.ExportManager.Id;
                exportProductBill.UserId = exportProductViewModel.SubsidiaryAgent.Id;
                int NewestID = _unitOfWork.ExportProductBillRepositorys.getNewId().Equals(null) ? 0 : (int)_unitOfWork.ExportProductBillRepositorys.getNewId();
                exportProductBill.Code = $"EPB_{NewestID + 1}";                
                exportProductBill.CreatedDate = DateTime.UtcNow;
                exportProductBill.ModifiedDate = DateTime.UtcNow;
                _unitOfWork.ExportProductBillRepositorys.Add(exportProductBill);
                await _unitOfWork.Commit();
                int ProductBillId = exportProductBill.Id;
                foreach (var item in exportProductViewModel.ExportProducts)
                {
                    // add exportProductBillDetail
                    ExportListDetail product = new ExportListDetail();
                    product.ProductId = item.Id;
                    product.ExportNumber = item.ExportNumber;
                    product.ExportPrice = item.ExportPrice;
                    product.ExportProductBillId = ProductBillId;
                    product.CreatedDate = DateTime.UtcNow;
                    product.ModifiedDate = DateTime.UtcNow;
                    product.ExportDate = DateTime.UtcNow;                                        
                    _unitOfWork.ExportListDetailRepositorys.Add(product);
                    await _unitOfWork.Commit();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;                
            }
        }

        public Task<IQueryable<ExportListDetail>> GetExportProductDetail(int ProductBillId, string[] includes = null)
        {
            return _unitOfWork.ExportListDetailRepositorys.GetMulti(x=>x.ExportProductBillId==ProductBillId,includes);
        }
    }
}
