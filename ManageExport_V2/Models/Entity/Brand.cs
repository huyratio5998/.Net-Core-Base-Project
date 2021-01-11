using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ManageExport_V2.Models.Entity
{
    public class Brand : BaseEntity<int>
    {
        [Display(Name = "Short Name")]
        public string ShortName { get; set; }
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
