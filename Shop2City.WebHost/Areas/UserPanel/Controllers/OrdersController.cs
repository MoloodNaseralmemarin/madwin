
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop2City.Core.Services.Factors;
using Shop2City.Core.Services.Orders;
using Shop2City.Core.Services.Products;
using Shop2City.Core.Services.Users;
using Shop2City.DataLayer.Context;
using Shop2City.DataLayer.Entities;
using Stimulsoft.System.Windows.Forms;


namespace Shop2City.WebHost.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly Shop2CityContext _context;
        private readonly IProductService _productService;

        public OrdersController(IOrderService orderService, IUserService userService, Shop2CityContext context, IProductService productService)
        {
            _orderService = orderService;
            _userService = userService;
            _context = context;
            _productService = productService;
        }

            public IActionResult ShowOrder(int id, bool finaly = false, string type = "")
            {
            ViewData["TypePost"] = _orderService.TypePosts();
            var order = _orderService.GetOrderForUserPanel(User.Identity.Name, id);

                if (order == null)
                {
                    return NotFound();
                }

                ViewBag.typeDiscount = type;
                ViewBag.finaly = finaly;
                return View(order);
            }
       
        public IActionResult Index()
        {
            //وقتی به این مرحله رسیدم اطلاعات توی جدول order ذخیره شده است ولی تکلیف ی سری از فیلدها مشخص نیست

            #region اطلاعات کاربر برای داشتن شماره موبایل برای ارسال پیامک نیاز دازرم
            var user = _userService.GetUserByUserName(User.Identity.Name);
            #endregion
            // در این مرحله نسب به نام کاربری شناسه کاربر بدست می آوریم.

            var userId = _userService.GetUserIdByUserName(User.Identity.Name);
            //اینجا با داشتن شناسه کاربر اطلاعات فاکتور رو به دست میآوریم
            var order = _orderService.GetOrderByUserId(userId);
            //وضعیت پرداخت مشخص میکنم که فاکتور نهایی بشه
            order.IsFinaly = true;
            #region در این مرحله جدول سفارش آپدیت میکنم
            _orderService.UpdateOrder(order);
            #endregion
            #region SMS
            #region Send SMS For User For Order 
            try
            {
                var otpsms = new Ghasedak.Core.Api("ce805d8405091990a26d5964e2e393667da9422ec5a472edfed717fa3b0aecfa");
                var res = otpsms.VerifyAsync(1, "PanahPlastSendOrder",
                new string[] { user.CellPhone, },
                    order.Id.ToString(), "وین ماد");

            }
            catch (Ghasedak.Core.Exceptions.ApiException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Ghasedak.Core.Exceptions.ConnectionException ex)
            {
                Console.WriteLine(ex.Message);
            }
            #endregion
            #region AddSMs User
            var smsorder = new Sms
            {
                CellPhone = user.CellPhone,
                Message = "در سامانه ثبت شده است." + order + "سفارش شما با کد پیگیری.\r\nفروشگاه آنلاین وین ماد \r\nلغو "

            };
            _userService.AddSMS(smsorder);
            #endregion
           
            #region ارسالsms به خانم ...
            try
            {
                var otpsms = new Ghasedak.Core.Api("ce805d8405091990a26d5964e2e393667da9422ec5a472edfed717fa3b0aecfa");
                var res = otpsms.VerifyAsync(1, "PanahPlastGetOrder",
                new string[] {"09182185223" },
                order.Id.ToString(),
                order.OrderCategory.Title + "/" + order.OrderSubCategory.Title,
                order.Height.ToString(),
                order.Width.ToString(),
                order.Count.ToString(),
                "وین ماد");

            }

            catch (Ghasedak.Core.Exceptions.ApiException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Ghasedak.Core.Exceptions.ConnectionException ex)
            {
                Console.WriteLine(ex.Message);
            }

            #endregion
            #region AddSMs Mrs
            var smsmrsorder = new Sms
            {
                CellPhone = user.CellPhone,
                Message = "فاکتور" + order.Id + "نوع" + order.OrderCategory.Title + "/" + order.OrderSubCategory.Title + "ارتفاع" + order.Height + "عرض" + order.Width + "تعداد" + order.Count + "\r\nفروشگاه آنلاین وین ماد \r\nلغو",

            };
            _userService.AddSMS(smsmrsorder);
            #endregion
            #region ارسال SMS به داداش
            try
            {
                var otpsms = new Ghasedak.Core.Api("ce805d8405091990a26d5964e2e393667da9422ec5a472edfed717fa3b0aecfa");
                var res = otpsms.VerifyAsync(1, "PanahPlastGetOrderAdmin",
                new string[] { "09180580270" },
                order.Id.ToString(),
                order.User.FirstName + " " + order.User.LastName,
                order.OrderCategory.Title + "/" + order.OrderSubCategory.Title,
                order.Height.ToString(),
                order.Width.ToString(),
                order.Count.ToString(),
                "وین ماد");

            }

            catch (Ghasedak.Core.Exceptions.ApiException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Ghasedak.Core.Exceptions.ConnectionException ex)
            {
                Console.WriteLine(ex.Message);
            }
            #endregion
            #region AddSMs

            var sms = new Sms
            {
                CellPhone = "09180580270",
                Message = "فاکتور" + order.Id + "نوع" + order.OrderCategory.Title + "/" + order.OrderSubCategory.Title + "ارتفاع" + order.Height + "عرض" + order.Width + "تعداد" + order.Count + "\r\nفروشگاه آنلاین وین ماد \r\nلغو",

            };
            _userService.AddSMS(sms);
            #endregion
            #endregion
            return View(order);
        }

        [Authorize]
        public IActionResult DeleteOrder(int id)
        {
            _orderService.DeleteOrderItem(id);
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult OrderForUser(bool isfinaly)
        {
            ViewData["TypePost"] = _orderService.TypePosts();
            var showorder = _orderService.GetUserOrders(User.Identity.Name, isfinaly);
            if (showorder == null)
                return View();
            return View(showorder);
        }

        public async Task<IActionResult> UseDiscount(int orderId, string code)
        {
            var type =_orderService.UseDiscount(orderId, code);
            return Redirect("/UserPanel/Orders/ShowOrder/" + orderId + "?type=" + type);
        }
    }
}

