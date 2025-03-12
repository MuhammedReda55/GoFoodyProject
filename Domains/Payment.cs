using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }  // CreditCard, CashOnDelivery
        public DateTime PaymentDate { get; set; }

        // ✅ العلاقة مع الطلب
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
