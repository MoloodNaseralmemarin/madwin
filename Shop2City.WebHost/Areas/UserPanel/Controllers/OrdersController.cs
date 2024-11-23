
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop2City.Core.Services.Factors;
using Shop2City.Core.Services.Orders;
using Shop2City.Core.Services.Products;
using Shop2City.Core.Services.Users;
using Shop2City.DataLayer.Context;
using Shop2City.DataLayer.Entities;
using Shop2City.DataLayer.Entities.Users;

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

        public async Task<IActionResult> ShowOrder(int id, bool finaly = false, string type = "")
        {
            int userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);
            ViewData["TypePost"] = _orderService.TypePosts();
            var order = await _orderService.GetOrderForUserPanel(userId, id);

            if (order == null)
            {
                return NotFound();
            }

            ViewBag.typeDiscount = type;
            ViewBag.finaly = finaly;
            return View(order);
        }

        [Authorize]
        public async Task<IActionResult> ShowOrderForUser(int orderId)
        {
            int userId = await _userService.GetUserIdByOrderId(orderId);
            var cellPhone = await _userService.GetCellPhoneByUserId(userId);
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
                new string[] { cellPhone },
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
                CellPhone = cellPhone,
                Message = "در سامانه ثبت شده است." + order + "سفارش شما با کد پیگیری.\r\nفروشگاه آنلاین وین ماد \r\nلغو "

            };
            await _userService.AddSMSAsync(smsorder);
            #endregion

            #region ارسالsms به خانم ...
            try
            {
                var otpsms = new Ghasedak.Core.Api("ce805d8405091990a26d5964e2e393667da9422ec5a472edfed717fa3b0aecfa");
                var res = otpsms.VerifyAsync(1, "PanahPlastGetOrder",
                new string[] { "09182185223" },
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
                CellPhone = cellPhone,
                Message = "فاکتور" + order.Id + "نوع" + order.OrderCategory.Title + "/" + order.OrderSubCategory.Title + "ارتفاع" + order.Height + "عرض" + order.Width + "تعداد" + order.Count + "\r\nفروشگاه آنلاین وین ماد \r\nلغو",

            };
            await _userService.AddSMSAsync(smsmrsorder);
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
            await _userService.AddSMSAsync(sms);
            #endregion
            #endregion
            return View(order);
        }

        public async Task<IActionResult> UseDiscount(int orderId, string code)
        {
            var type = await _orderService.UseDiscount(orderId, code);
            return Redirect("/UserPanel/Orders/ShowOrder/" + orderId + "?type=" + type);
        }
    }
}

