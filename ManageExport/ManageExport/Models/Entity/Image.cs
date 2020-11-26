using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entity
{
    public class Image : BaseEntity<int>
    {
        public string Url { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
