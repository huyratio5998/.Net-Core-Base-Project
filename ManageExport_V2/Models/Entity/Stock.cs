using System;
using System.Collections.Generic;
using System.Text;

namespace ManageExport_V2.Models.Entity
{
    public class Stock : BaseEntity<int>
    {
        public string Name { get; set; }
        
        public IEnumerable<Product> Products { get; set; }
    }
}
