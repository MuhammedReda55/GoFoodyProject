using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class MenuItemViewModel
    {
        public MenuItem MenuItem { get; set; }
        public Restaurant Resturant { get; set; }
        public List<MenuItem> Items { get; set; } =new List<MenuItem>();
        public List<Restaurant> Restaurants { get; set; } = new List<Restaurant>();

        public int SelectedRestaurantId { get; set; }

    }
}
