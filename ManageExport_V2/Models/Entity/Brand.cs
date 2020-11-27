using System;
using System.Collections.Generic;
using System.Text;

namespace ManageExport_V2.Models.Entity
{
    public class Brand : BaseEntity<int>
    {
        public string ShortName { get; set; }
        public string FullName { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
