using ManageExport_V2.Models;
using ManageExport_V2.Models.Entity;
using ManageExport_V2.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageExport_V2.Repositories
{
    public class ExportProductBillRepository : BaseRepository<ExportProductBill>, IExportProductBillRepository
    {
        protected ExportProductBillRepository(ExportContext context) : base(context)
        {
        }
        public bool AddExportProduct(ExportProductViewModel exportProductViewModel)
        {
            try
            {
                ExportProductBill exportProductBill = new ExportProductBill();

                foreach (var item in exportProductViewModel.ExportProducts)
                {

                }
                return _unitOfWork..GetSingleById(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
