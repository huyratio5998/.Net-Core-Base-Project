using ManageExport_V2.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageExport_V2.Models
{
    public class ExportProductPageViewModel
    {
        public int SAID { get; set; }
        public string SubAgentName { get; set; }
        public Product Product { get; set; }
        public int ExportNumber{ get; set; }
        public double ExportPrice{ get; set; }              
    }
}
