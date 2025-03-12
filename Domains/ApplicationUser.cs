using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string? Role { get; set; }  // Customer, RestaurantOwner, DeliveryPerson

        // ✅ العلاقات
        public virtual ICollection<Order> Orders { get; set; }  // لو كان عميل
        
        public virtual ICollection<Order> Deliveries { get; set; }
        public virtual ICollection<Restaurant> Restaurants { get; set; } // لو كان مندوب توصيل

        public ApplicationUser() 
        {
            Orders = new HashSet<Order>();
            Restaurants=new HashSet<Restaurant>();
             Deliveries = new HashSet<Order>();
        }
    }
}
