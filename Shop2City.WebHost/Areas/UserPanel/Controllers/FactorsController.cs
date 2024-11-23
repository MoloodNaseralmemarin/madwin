using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop2City.Core.Services.Factors;
using Shop2City.Core.Services.Users;
using Shop2City.DataLayer.Entities;
using Shop2City.DataLayer.Entities.Users;


namespace Shop2City.WebHost.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class FactorsController: Controller
    {
        private readonly IFactorService _factorService;
        private readonly IUserService _userService;
        public FactorsController(IFactorService factorService, IUserService userService)
        {
            _userService= userService;
            _factorService= factorService;
        }

        public IActionResult ShowFactor(int id, bool finaly = false, string type = "")
        {
            ViewData["TypePost"] = _factorService.TypePosts();
            var order = _factorService.GetFactorForUserPanel(User.Identity.Name, id);
            if (order == null)
            {
                return NotFound();
            }

            ViewBag.typeDiscount = type;
            ViewBag.finaly = finaly;
            return View(order);
        }




        [Authorize]
        public async Task<IActionResult> ShowFactorForUser(int factorId)
        {
            int userId = await _userService.GetUserIdByFactorId(factorId);
            var cellPhone = await _userService.GetCellPhoneByUserId(userId);
            var factor = _factorService.GetFactorByUserId(userId);

            var listFactorDetails = _factorService.GetAllFactorDetailByFactorId(factorId);
            //وضعیت پرداخت مشخص میکنم که فاکتور نهایی بشه
            factor.IsFinaly = true;
            #region در این مرحله جدول سفارش آپدیت میکنم
             _factorService.UpdateFactor(factor);
            #endregion
            //در این مرحله ی پیامک به خریدار ارسال می شود.
            #region Send SMS For User For Factor 
            try
            {
                var otpsms = new Ghasedak.Core.Api("ce805d8405091990a26d5964e2e393667da9422ec5a472edfed717fa3b0aecfa");
                var res = otpsms.VerifyAsync(1, "PanahPlastSendFactor",
                new string[] { cellPhone },//شماره خریدار
                factorId.ToString(), "پناه پلاست");

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
            var smsorder = new Sms
            {
                CellPhone = cellPhone,
                Message = "در سامانه ثبت شده است." + factorId + "فاکتور شما با کد پیگیری.\r\nفروشگاه آنلاین پناه پلاست \r\n لغو 11"

            };
           await _userService.AddSMSAsync(smsorder);
            #endregion
            #endregion
            //در این مرحله به ازای هر جزئیات فاکتور یک پیامک به کارفرما ارسال می شود.
            #region ارسالsms به کارفرما
            try
            {
                var i = 1;
                foreach (var item in listFactorDetails)
                {
                    var tempelete = item.FactorId.ToString() + "-" + listFactorDetails.Count + "-" + i + "نوع" + item.Product.Title + "تعداد" + item.Quantity.ToString();
                    var otpSms = new Ghasedak.Core.Api("ce805d8405091990a26d5964e2e393667da9422ec5a472edfed717fa3b0aecfa");
                    var res = otpSms.VerifyAsync(1, "PanahPlastGetFactor",
                    new string[] { "09180580270", "09182185223" },//09180580270
                    item.FactorId + "-" + listFactorDetails.Count + "-" + i,
                    item.Product.Title,
                    item.Quantity.ToString(),
                    "پناه پلاست");
                    i++;
                    #region AddSMs

                    var sms = new Sms
                    {
                        //به وقتش درست کن
                        CellPhone = "09376421351",//09180580270
                        Message = "فاکتور" + item.FactorId + "-" + listFactorDetails.Count + "-" + i + " " + "نوع" + item.Product.Title + " " + "تعداد :" + item.Quantity + " " + "با تشکر پناه پلاست لغو 11",

                    };
                    await _userService.AddSMSAsync(sms);
                    #endregion
                }
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
            return View(factor);
        }

        public IActionResult UseDiscount(int factorId, string code)
        {
            var type = _factorService.UseDiscount(factorId, code);
            return Redirect("/UserPanel/Factors/ShowFactor/" + factorId + "?type=" + type);
        }
    }
}

