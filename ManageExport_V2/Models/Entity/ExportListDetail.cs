using System;
using System.Collections.Generic;
using System.Text;

namespace ManageExport_V2.Models.Entity
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
