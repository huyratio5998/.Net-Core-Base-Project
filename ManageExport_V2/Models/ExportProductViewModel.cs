using ManageExport_V2.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageExport_V2.Models
{
    public class ExportProductViewModel
    {
        public string Code { get; set; }
        public double TotalMoney { get; set; }
        public User SubsidiaryAgent{ get; set; }
        public User ExportManager{ get; set; }
        public List<Product> ExportProducts{ get; set; }
        public DateTime ExportDate { get; set; }
        public ExportProductViewModel()
        {
            ExportProducts = new List<Product>();
        }
    }
}
