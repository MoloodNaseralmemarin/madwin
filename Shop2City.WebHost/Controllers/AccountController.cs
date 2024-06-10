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
using Shop2City.Core.DTOs.ShoppingCart;
using Shop2City.Core.Helpers;

namespace Shop2City.WebHost.Controllers
{
    public class AccountController(IUserService userService) : Controller
    {


        #region Register

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }
           

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel registerViewModel)
        { 
            if (!ModelState.IsValid)
                return View(registerViewModel);

            #region IsExist...
            if (userService.IsExistCellPhone(registerViewModel.cellPhone))
            {
                ModelState.AddModelError("cellPhone", ErrorMessage.InvalidCellPhone);
                //TempData["Message"] = " شما قبلاً با " + registerViewModel.cellPhone + "ثبت نام کرده اید. ";
                return View(registerViewModel);
            }
            #endregion

            #region Register User
            var user = new User
            {
                CellPhone = registerViewModel.cellPhone,
                TelPhone = "00000000",
                FirstName = registerViewModel.firstName,
                LastName = registerViewModel.lastName,
                Password = PasswordHelper.EncodePasswordMd5(registerViewModel.password),
                CreateDate = DateTime.Now,
                UserName = registerViewModel.cellPhone,
                Address =registerViewModel.address,
            };
            #endregion

            userService.AddUser(user);
            #region Send Welcome SMS
            try
            {
                var otpsms = new Ghasedak.Core.Api("ce805d8405091990a26d5964e2e393667da9422ec5a472edfed717fa3b0aecfa");
                var res = otpsms.VerifyAsync(1, "PanahPlast",
                    [registerViewModel.cellPhone],
                registerViewModel.firstName+ " "+registerViewModel.lastName,"پناه پلاست");

            }
            catch (Ghasedak.Core.Exceptions.ApiException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Ghasedak.Core.Exceptions.ConnectionException ex)
            {
                Console.WriteLine(ex.Message);
            }

            #region AddSMs
            var sms = new Sms
            {
                CellPhone = registerViewModel.cellPhone,
                Message = registerViewModel.firstName + " " + registerViewModel.lastName + "گرامی به فروشگاه آنلاین پلاست خوش آمدید.\r\nفروشگاه آنلاین پناه پلاست \r\nلغو "

            };
            userService.AddSMS(sms);
            #endregion
            #endregion
            //ViewBag.IsSuccess
            ViewBag.IsSuccess = "" + registerViewModel.firstName+ " " +registerViewModel.lastName +" "+ "عزیز! با تشکر عضویت شما با موفقیت انجام شد.جهت ادامه خرید خود با نام کاربری و کلمه عبور خود وارد شوید.";
            return View();
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
        public ActionResult Login(LoginViewModel login, string returnUrl)
        {
            if(!ModelState.IsValid)
            {
                return View(login);
            }

            
            var user = userService.LoginUser(login);
            if (user != null)
            {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                        new Claim(ClaimTypes.Name,user.UserName)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = login.rememberMe
                    };
                    HttpContext.SignInAsync(principal, properties);

                    ViewBag.IsSuccess = true;
                    if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    else
                        return View();

            }
            ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده یافت نشد");
            return View(login);
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