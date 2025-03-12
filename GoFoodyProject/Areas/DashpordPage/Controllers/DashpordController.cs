using BL;
using Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GoFoodyProject.Areas.DashpordPage.Controllers
{
    [Area("DashpordPage")]
    public class DashpordController : Controller
    {
        ConfigContext _context;
        IResturant _resturant;
        public DashpordController(ConfigContext context, IResturant resturant) 
        {
            _context = context;
            _resturant = resturant;
        }
        public IActionResult AdminTemplate()
        {
            return View();
        }
        //#============================================== اضافه مطعم جديد ++=========================================

        // GET: AddRestaurant
        public IActionResult AddRestaurant()
        {
            var model = new AddRestaurantViewModel();
            return View(model);
        }

        // POST: AddRestaurant
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRestaurant(AddRestaurantViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // رفع الصورة إذا كانت موجودة
            string? imagePath = null;
            if (model.RestaurantImageFile != null)
            {
                imagePath = await UploadImageAsync(model.RestaurantImageFile, "RestaurantImage");
            }
            else
            {
                imagePath = "default.jpg"; // تعيين صورة افتراضية
            }

            // إنشاء كائن `Restaurant` من البيانات المدخلة
            var restaurant = new Restaurant
            {
                RestaurantName = model.RestaurantName,
                RestaurantAddress = model.RestaurantAddress,
                OwnerName = model.OwnerName,
                PhoneNumber = model.PhoneNumber,
                RestaurantImage = imagePath
            };

            // حفظ المطعم في قاعدة البيانات
            _context.TbRestaurants.Add(restaurant);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Restaurant added successfully!";
            return RedirectToAction("AddMenuItem");
        }

        //#============================================== تعديل علي  مطعم  ++=========================================

        [HttpGet]
        public IActionResult EditRestaurant(int id)
        {
            var restaurant = _context.TbRestaurants.Find(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            var model = new AddRestaurantViewModel
            {
                RestaurantId = restaurant.RestaurantId,
                RestaurantName = restaurant.RestaurantName,
                RestaurantAddress = restaurant.RestaurantAddress,
                OwnerName = restaurant.OwnerName,
                PhoneNumber = restaurant.PhoneNumber,
                RestaurantImage = restaurant.RestaurantImage // تحميل الصورة القديمة
            };

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> EditRestaurant(AddRestaurantViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var restaurant = _context.TbRestaurants.Find(model.RestaurantId);
            if (restaurant == null)
            {
                return NotFound();
            }

            // تحديث البيانات الأساسية
            restaurant.RestaurantName = model.RestaurantName;
            restaurant.RestaurantAddress = model.RestaurantAddress;
            restaurant.OwnerName = model.OwnerName;
            restaurant.PhoneNumber = model.PhoneNumber;

            // تحديث الصورة إذا تم رفع صورة جديدة
            if (model.RestaurantImageFile != null)
            {
                string uploadedFileName = await UploadImageAsync(model.RestaurantImageFile, "RestaurantImage");
                restaurant.RestaurantImage = uploadedFileName;
            }

            _context.SaveChanges();

            return RedirectToAction("RestaurantList");
        }



        //#============================================== مسح مطعم  ++=========================================


        [HttpPost]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var restaurant = await _context.TbRestaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            _context.TbRestaurants.Remove(restaurant);
            await _context.SaveChangesAsync();

            // تحميل قائمة المطاعم بعد الحذف
            var remainingRestaurants = await _context.TbRestaurants.ToListAsync();

            return View("RestaurantList", remainingRestaurants);
        }


        // عرض صفحة إضافة MenuItem

        //#==================================================== عرض المطاعم الموجوده عندي اول ال انا ضفتها كلها ======================================

        public async Task<IActionResult> RestaurantList()
        {
            List<Restaurant> restaurants = _resturant.GetAll(); // تصحيح نوع البيانات
            return View(restaurants);
        }

        //#================================================================== اضافه menu دخل مطعم معين ======================================
        [HttpGet]
        public async Task<IActionResult> AddMenuItem()
        {
            // جلب جميع المطاعم لعرضها في القائمة المنسدلة
            var restaurants = await _context.TbRestaurants.ToListAsync();

            // إنشاء ViewModel وإرسال البيانات للـ View
            var viewModel = new MenuItemViewModel
            {
                Restaurants = restaurants,
                MenuItem = new MenuItem() // ✅ حل المشكلة بتهيئة MenuItem
            };

            return View(viewModel);
        }



        [HttpPost]
        public async Task<IActionResult> AddMenuItem(MenuItemViewModel viewModel, IFormFile MenuItemImageFile)
        {
            if (string.IsNullOrEmpty(viewModel.MenuItem.FoodName))
            {
                ModelState.AddModelError("MenuItem.FoodName", "Food Name is required.");

                // إعادة إرسال قائمة المطاعم للـ View
                viewModel.Restaurants = await _context.TbRestaurants.ToListAsync();
                return View(viewModel);
            }

            // استدعاء الميثود العامة لرفع الصورة
            viewModel.MenuItem.ProductImage = await UploadImageAsync(MenuItemImageFile, "RestaurantImage") ?? "default.jpg";

            // حفظ العنصر في قاعدة البيانات
            _context.TbMenuItems.Add(viewModel.MenuItem);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Menu item added successfully!";
            return RedirectToAction("AddMenuItem");
        }

        //#================================================================== عرض قائمه ال menu داخل مطعم معين======================================
        public async Task<IActionResult> ViewMenu(int restaurantId)
        {
            var menuItems = await _context.TbMenuItems
                                          .Where(m => m.RestaurantId == restaurantId)
                                          .ToListAsync();

            var model = new MenuItemViewModel
            {
                Items = menuItems, // قائمة عناصر المينيو الخاصة بالمطعم
                SelectedRestaurantId = restaurantId // المطعم المحدد
            };

            return View(model);
        }



        //#================================================================== تعديل ال menu ======================================
        [HttpGet]
        public IActionResult EditMenuItem(int id)
        {
            var menuItem = _context.TbMenuItems.Find(id);
            if (menuItem == null)
            {
                return NotFound();
            }

            var model = new MenuItemViewModel
            {
                MenuItem = menuItem,
                Restaurants = _context.TbRestaurants.ToList() // تحميل قائمة المطاعم هنا أيضًا
            };

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> EditMenuItem(MenuItemViewModel model, IFormFile? MenuItemImageFile)
        {
            if (!ModelState.IsValid)
            {
                model.Restaurants = _context.TbRestaurants.ToList();
                return View(model);
            }

            var menuItem = _context.TbMenuItems.Find(model.MenuItem.MenuItemId);
            if (menuItem == null)
            {
                return NotFound();
            }

            // ✅ استدعاء الميثود لرفع الصورة
            if (MenuItemImageFile != null && MenuItemImageFile.Length > 0)
            {
                menuItem.ProductImage = await UploadImageAsync(MenuItemImageFile, "RestaurantImage");
            }

            // ✅ تحديث باقي البيانات
            menuItem.FoodName = model.MenuItem.FoodName;
            menuItem.Price = model.MenuItem.Price;
            menuItem.SalePrice = model.MenuItem.SalePrice;
            menuItem.Sale = model.MenuItem.Sale;
            menuItem.Qty = model.MenuItem.Qty;
            menuItem.Total = model.MenuItem.Total;
            menuItem.Description = model.MenuItem.Description;
            menuItem.RestaurantId = model.SelectedRestaurantId;

            await _context.SaveChangesAsync();
            return RedirectToAction("ViewMenu", new { restaurantId = model.SelectedRestaurantId });
        }

        //#================================================================== حذف itmes  ال menu ======================================


        [HttpPost]
        public IActionResult DeleteMenuItem(int id)
        {
            var menuItem = _context.TbMenuItems.Find(id);
            if (menuItem == null)
            {
                return NotFound();
            }

            int restaurantId = menuItem.RestaurantId; // الحصول على ID المطعم
            _context.TbMenuItems.Remove(menuItem);
            _context.SaveChanges();

            // تحميل قائمة الطعام الخاصة بالمطعم بعد الحذف
            var remainingItems = _context.TbMenuItems
                .Where(m => m.RestaurantId == restaurantId)
                .ToList();

            // تجهيز البيانات في نموذج `MenuItemViewModel`
            var model = new MenuItemViewModel
            {
                Items = remainingItems, // قائمة الطعام بعد الحذف
                SelectedRestaurantId = restaurantId
            };

            return View("ViewMenu", model); // تمرير الـ Model إلى الـ View بشكل صحيح
        }




        //#================================================================== helper method to upload images ======================================


        private async Task<string> UploadImageAsync(IFormFile imageFile, string folderName)
        {
            string fileName = "default.jpg";

            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/UploadsImage/{folderName}");

                // إنشاء اسم فريد للصورة
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                // رفع الصورة إلى السيرفر
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
            }

            return fileName;
        }




    }
}
