
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ManageExport_V2.Models.Entity
{
    public class ExportProductBill : BaseEntity<int>
    {
        public string Code { get; set; }
        [Display(Name = "Total Money")]
        public double TotalMoney { get; set; }
        [Display(Name = "Export Date")]
        public DateTime ExportDate { get; set; }
        public int ExportManagerId { get; set; }
        //useid is subsidiary agent id
        public int UserId { get; set; }
        public User User { get; set; }
        public IEnumerable<ExportListDetail> ExportListDetails { get; set; }
    }
}
