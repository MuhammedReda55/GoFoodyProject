using Domains;
using GoFoodyProject.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace GoFoodyProject.Controllers
{
    public class UserController : Controller
    {

     UserManager<ApplicationUser> _userManager;
     SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly EmailService _emailService;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
            IEmailSender emailSender, EmailService emailService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailSender = emailSender;
        _emailService = emailService;
        }

        // ✅ عرض صفحة تسجيل الدخول
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        // ✅ تنفيذ تسجيل الدخول
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginVM.Email);


                if (user != null)
                {
                    if (user.LockoutEnd != null && user.LockoutEnd > DateTime.UtcNow)
                    {
                        ModelState.AddModelError(string.Empty, "Your account is locked.");
                        return View(loginVM);
                    }
                    var result = await _userManager.CheckPasswordAsync(user, loginVM.Password);

                    if (result)
                    {
                        await _signInManager.SignInAsync(user, loginVM.RememberMe);
                        return RedirectToAction("HomePage", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "هناك خطأ في الحساب او في كلمه السر");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "البريد الالكتروني غير موجود");
                }
            }

            return View(loginVM);


            
        }

        // ✅ عرض صفحة التسجيل
        [HttpGet]
        public IActionResult Register(string returnUrl = null)
        {
            return View(new RegisterViewModel { ReturnUrl = returnUrl });
        }

        // ✅ تنفيذ إنشاء حساب جديد
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // ✅ التحقق من تطابق كلمة المرور والتأكيد
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Password and confirmation do not match");
                return View(model);
            }

            // ✅ إنشاء مستخدم جديد من `ApplicationUser` وليس `RegisterViewModel`
            var user = new ApplicationUser
            {
                UserName = model.Email,
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                
            };
          

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                await _userManager.AddToRoleAsync(user, SD.UserRole);
                string loginUrl = Url.Action("Login", "Dashpord", null, protocol: Request.Scheme);

                // ✅ استدعاء EmailService لإرسال البريد
                await _emailService.SendRegistrationEmailAsync(user.Email, user.FullName, loginUrl);


                // ✅ إعادة التوجيه إلى ReturnUrl إذا كان موجودًا، وإلا الذهاب للصفحة الرئيسية
                //return LocalRedirect(model.ReturnUrl ?? "/");
                return RedirectToAction("HomePage", "Home");
            }

            // ✅ إذا حدث خطأ أثناء إنشاء المستخدم، نضيف الأخطاء للـ ModelState لعرضها في الواجهة
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }


        // ✅ تنفيذ تسجيل الخروج
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login","User");
        }

        // ✅ دالة إعادة التوجيه للصفحة الأصلية بعد تسجيل الدخول أو التسجيل
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }

}

