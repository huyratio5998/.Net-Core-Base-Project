using System;
using System.Collections.Generic;
using System.Text;

namespace ManageExport.Models.Entity
{
    public class ExportListDetail : BaseEntity<int>
    {                
        public DateTime ExportDate { get; set; }

        public int ExportDocumentBillId { get; set; }
        public ExportDocumentBill ExportDocumentBill{ get; set; }
        public int ProductId { get; set; }
        public Product Products { get; set; }

    }
}
