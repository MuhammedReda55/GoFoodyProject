using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        // ✅ العلاقة مع الطلب
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        // ✅ العلاقة مع قائمة الطعام
        public int MenuItemId { get; set; }
        public virtual MenuItem MenuItem { get; set; }
    }
}
