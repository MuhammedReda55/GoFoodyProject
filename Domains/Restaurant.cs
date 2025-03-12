using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string OwnerName { get; set; }
        public string? RestaurantImage { get; set; }
        public string? OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }

        // ✅ العلاقة مع قائمة الطعام
        public virtual ICollection<MenuItem> MenuItems { get; set; }

        public Restaurant() 
        {
            MenuItems = new List<MenuItem>();
        }
    }
}
