using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }  // Pending, In Progress, Delivered
        public decimal TotalPrice { get; set; }

        // ✅ العلاقة مع العميل
        public string CustomerId { get; set; }
        public virtual ApplicationUser Customer { get; set; }

        // ✅ العلاقة مع مندوب التوصيل (اختياري)
        public string DeliveryPersonId { get; set; }
        public virtual ApplicationUser DeliveryPerson { get; set; }

        // ✅ العلاقة مع تفاصيل الطلب
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public int PaymentId { get; set; }
        public virtual Payment Payment { get; set; }

        public Order() 
        {
            OrderItems = new List<OrderItem>();
        }
    }
}
