using System;
using System.Collections.Generic;
using System.Text;

namespace ManageExport.Models.Entity
{
    public class Stock : BaseEntity<int>
    {
        public string Name { get; set; }
        
        public IEnumerable<Product> Products { get; set; }
    }
}
