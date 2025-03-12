using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class MenuItem
    {
        public int MenuItemId { get; set; }
        public string FoodName { get; set; }
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
        public int? Sale { get; set; }
        public string? Description { get; set; }
        public string? ProductImage { get; set; }
        public int? Qty { get; set; }
        public decimal? Total { get; set; }

       

        // ✅ العلاقة مع المطعم
        public int RestaurantId { get; set; }
        public virtual Restaurant? Restaurant { get; set; }

       

        public int OrderItemId { get; set; }
        public OrderItem? OrderItem { get; set; }
    }
}
