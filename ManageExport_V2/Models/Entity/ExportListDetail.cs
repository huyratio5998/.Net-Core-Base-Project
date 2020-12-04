using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ManageExport_V2.Models.Entity
{
    public class ExportListDetail : BaseEntity<int>
    {                
        public DateTime ExportDate { get; set; }        
        public int ExportProductBillId { get; set; }
        public ExportProductBill ExportDocumentBill { get; set; }
        public int ProductId { get; set; }
        public Product Products { get; set; }

    }
}
