using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entity
{
    public abstract class BaseEntity<T>
    {
        public T Id { get; set; }

        // check xem id có bằng với giá trị mặc định của T hay ko, true nếu domain entity được xét tự động tăng r
        public bool IsTransient()
        {
            return Id.Equals(default(T));
        }        
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
