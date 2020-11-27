using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ManageExport_V2.Models.Entity
{
    public class Product : BaseEntity<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public  int Number { get; set; }        
        public DateTime MFG { get; set; }        
        public DateTime EXP { get; set; }
        public string Country { get; set; }        
        public string Description { get; set; }
        public double Price { get; set; }
        public string MainImage { get; set; }        
        public DateTime RecieveDate { get; set; }

        //        
        [ForeignKey("User")]
        public int SupplyId { get; set; }
        public User User { get; set; }
        public int StockId { get; set; }
        public Stock Stock { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public IEnumerable<ExportListDetail> ExportListDetails{ get; set; }
        public IEnumerable<ProductCategory> ProductCategorys { get; set; }
        public IEnumerable<Image> Images { get; set; }



    }
}
