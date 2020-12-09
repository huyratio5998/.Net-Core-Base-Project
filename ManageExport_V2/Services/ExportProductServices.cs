using ManageExport_V2.Models;
using ManageExport_V2.Models.Entity;
using ManageExport_V2.Repositories.Interfaces;
using ManageExport_V2.Services.Interfaces;
using System;

namespace ManageExport_V2.Services
{
    public class ExportProductServices : IExportProductServices
    {
        private IUnitOfWork _unitOfWork;
        public ExportProductServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ExportProductViewModel ExportProduct()
        {
            return new ExportProductViewModel();
        }
        public bool AddExportProduct(ExportProductViewModel exportProductViewModel)
        {
            try
            {
                ExportProductBill exportProductBill = new ExportProductBill();                
                foreach (var item in exportProductViewModel.ExportProducts)
                {
                    // add exportProductBillDetail
                    ExportListDetail product = new ExportListDetail();
                    product.ProductId = item.Id;
                    product.ExportProductBillId = exportProductBill.Id;
                    product.CreatedDate = DateTime.UtcNow;
                    product.ModifiedDate = DateTime.UtcNow;
                    product.ExportDate = DateTime.UtcNow;                                        
                    _unitOfWork.ExportListDetailRepositorys.Add(product);                                 
                }
                // add exportProductBill
                exportProductBill.TotalMoney = exportProductViewModel.TotalMoney;
                exportProductBill.ExportDate = DateTime.UtcNow;
                exportProductBill.ExportManagerId = exportProductViewModel.ExportManager.Id;
                exportProductBill.UserId = exportProductViewModel.SubsidiaryAgent.Id;
                exportProductBill.Code = exportProductViewModel.Code;
                exportProductBill.CreatedDate = DateTime.UtcNow;
                exportProductBill.ModifiedDate = DateTime.UtcNow;
                _unitOfWork.ExportProductBillRepositorys.Add(exportProductBill);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception e)
            {
                return false;                
            }
        }
    }
}
