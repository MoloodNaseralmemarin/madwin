using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Shop2City.Const;
using Shop2City.Core.DTOs.Account;
using Shop2City.Core.Security;
using Shop2City.Core.Services.Users;
using Shop2City.DataLayer.Entities;
using Shop2City.DataLayer.Entities.Users;
using System.Resources;

namespace Shop2City.WebHost.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IUserService userService, ILogger<AccountController> logger)
        {
            _userService = userService;
            _logger = logger;
            
        }
        #region Register

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
                return View(registerViewModel);
            if (_userService.IsExistCellPhone(registerViewModel.cellPhone))
            {
                ModelState.AddModelError("cellPhone", ErrorMessage.InvalidCellPhone);
                return View(registerViewModel);
            }

            #region Add User
            var user = new User
            {
                CellPhone = registerViewModel.cellPhone,
                TelPhone = "00000000",
                FirstName = registerViewModel.firstName,
                LastName = registerViewModel.lastName,
                Password = PasswordHelper.EncodePasswordMd5(registerViewModel.password),
                CreateDate = DateTime.Now,
                UserName = registerViewModel.cellPhone,
                Address = registerViewModel.address,
            };

            // Register user information
            await _userService.AddUserAsync(user);

            // Send a welcome SMS
            var isSmsSent = await SendWelcomeSmsAsync(registerViewModel);

            // اگر ارسال پیامک ناموفق بود، خطا به کاربر نمایش داده می‌شود
            if (!isSmsSent)
            {
                ModelState.AddModelError(string.Empty, "خطا در ارسال پیامک خوشامدگویی.");
                return View(registerViewModel);
            }

            // ذخیره اطلاعات SMS در پایگاه داده
            await SaveSmsAsync(registerViewModel);
            #endregion


            return RedirectToAction("Login");
        }

        // متد برای ارسال پیامک خوشامدگویی
        private async Task<bool> SendWelcomeSmsAsync(RegisterViewModel registerViewModel)
        {
            try
            {
                var otpsms = new Ghasedak.Core.Api("ce805d8405091990a26d5964e2e393667da9422ec5a472edfed717fa3b0aecfa");
                var result = await otpsms.VerifyAsync(1, "PanahPlast", new[] { registerViewModel.cellPhone }, $"{registerViewModel.firstName} {registerViewModel.lastName}", "ماد وین");

                // بررسی نتیجه ارسال پیامک
                if (result != null)  // جایگزین کردن با کد یا ویژگی درست
                {
                    return true;
                }
                else
                {
                    _logger.LogWarning("Error in sending SMS.");
                    return false;
                }
            }
            catch (Ghasedak.Core.Exceptions.ApiException ex)
            {
                // لاگ کردن خطاهای مربوط به API
                _logger.LogError(ex, "API error during sending SMS.");
                return false;
            }
            catch (Ghasedak.Core.Exceptions.ConnectionException ex)
            {
                // لاگ کردن خطاهای مربوط به اتصال
                _logger.LogError(ex, "Connection error during sending SMS.");
                return false;
            }
            catch (Exception ex)
            {
                // لاگ کردن سایر خطاها
                _logger.LogError(ex, "Unexpected error during sending SMS.");
                return false;
            }
        }

        // متد برای ذخیره اطلاعات SMS
        private async Task SaveSmsAsync(RegisterViewModel registerViewModel)
        {
            var sms = new Sms
            {
                CellPhone = registerViewModel.cellPhone,
                Message = $"{registerViewModel.firstName} {registerViewModel.lastName} گرامی، به فروشگاه آنلاین پلاست خوش آمدید.\r\nفروشگاه آنلاین پناه پلاست \r\nلغو "
            };

            await _userService.AddSMSAsync(sms);
        }

        #endregion

        #region Login
        [Route("Login")]
        public IActionResult Login(bool editProfile = false)
        {
            ViewBag.EditProfile = editProfile;
            return View();
        }

        [HttpPost]
        [Route("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState is invalid during login attempt for Email: {Email}", login.userName);
                return View(login);
            }

            try
            {
                var user = _userService.LoginUser(login);
                if (user != null)
                {
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
            };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = login.rememberMe
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);

                    _logger.LogInformation("User {UserId} logged in successfully.", user.Id);
                    TempData["LoginSuccessMessage"] = "ورود شما با موفقیت انجام شد.";

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        _logger.LogInformation("Redirecting user {UserId} to returnUrl: {ReturnUrl}", user.Id, returnUrl);
                        return Redirect(returnUrl);
                    }

                    return View();
                }

                _logger.LogWarning("Invalid login attempt for userName: {userName}", login.userName);
                ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده یافت نشد");
                return View(login);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during login attempt for userName: {userName}", login.userName);
                return View(login);
            }
        }



        #endregion

        #region Logout
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Cart");
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }
        #endregion
    }
}