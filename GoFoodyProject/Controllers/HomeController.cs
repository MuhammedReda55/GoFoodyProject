using System.Diagnostics;
using BL;
using Domains;
using GoFoodyProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoFoodyProject.Controllers
{
    public class HomeController : Controller
    {
        ConfigContext _context;
        IResturant _resturant;
        public HomeController(ConfigContext context, IResturant resturant)
        {
            _context = context;
            _resturant = resturant;
        }
        

        public async Task<IActionResult> HomePage()
        {
            List<Restaurant> restaurants = _resturant.GetAll(); // تصحيح نوع البيانات
            return View(restaurants);
        }

        public async Task<IActionResult> MenusPage(int restaurantId)
        {
            var menuItems = await _context.TbMenuItems
                                          .Where(m => m.RestaurantId == restaurantId)
                                          .ToListAsync();

            // جلب بيانات المطعم بناءً على ID
            var restaurant = await _context.TbRestaurants
                                           .FirstOrDefaultAsync(r => r.RestaurantId == restaurantId);

            var model = new MenuItemViewModel
            {
                Items = menuItems, // قائمة الطعام
                SelectedRestaurantId = restaurantId, // معرف المطعم
                Resturant = restaurant // بيانات المطعم (بما فيها الصورة)
            };

            return View(model);
        }

    }
}
