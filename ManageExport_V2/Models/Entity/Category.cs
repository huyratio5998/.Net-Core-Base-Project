using System;
using System.Collections.Generic;
using System.Text;

namespace ManageExport_V2.Models.Entity
{
    public class Category : BaseEntity<int>
    {
        public string Name { get; set; }
        public int? ParentId{ get; set; }
        public IEnumerable<ProductCategory> ProductCategories { get; set; }
    }
}
