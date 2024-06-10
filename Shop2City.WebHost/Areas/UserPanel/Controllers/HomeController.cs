using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop2City.Const;
using Shop2City.Core.DTOs.Account;
using Shop2City.Core.DTOs.Orders;
using Shop2City.Core.Services;
using Shop2City.Core.Services.Calculations;
using Shop2City.Core.Services.Orders;
using Shop2City.Core.Services.Products;
using Shop2City.Core.Services.UserPanel;
using Shop2City.Core.Services.Users;
using Shop2City.Core.Services.Wages;
using Shop2City.DataLayer.Entities.Orders;

namespace Shop2City.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserPanelService _userPanelService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly ICalculationService _calculationRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IGenericRepository<OrderDetailModel> _orderDetailRepository;
        private readonly IGenericRepository<CalculationDetailModel> _calculationDetailRepository;
        private readonly IWageService _wageService;

        public HomeController(IUserService userService, IUserPanelService userPanelService,IProductService productService, IOrderService orderService, ICalculationService calculationRepository, IOrderRepository orderRepository, IGenericRepository<OrderDetailModel> orderDetailRepository, IGenericRepository<CalculationDetailModel> calculationDetailRepository, IWageService wageService)
        {
            _userService = userService;
            _userPanelService = userPanelService;
            _productService = productService;
            _orderService = orderService;
            _calculationRepository = calculationRepository;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _calculationDetailRepository = calculationDetailRepository;
            _wageService = wageService;
        }
        public IActionResult Index()
        {
            var category = _productService.GetCategoryForManageProduct(1);
            ViewData["Categories"] = new SelectList(category, "Value", "Text");

            var subCategory = _productService.GetSubCategoryForManageProduct(int.Parse(category.First().Value));
            ViewData["SubCategories"] = new SelectList(subCategory, "Value", "Text");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(OrderViewModel orderView)
        {
            int userId = _userService.GetUserIdByUserName(User.Identity.Name);
            decimal resultPrice = 0;

            int oldHeight = orderView.Height;
            int oldWidth = orderView.Width;
            #region به گفته آقای نادری 03-21
            if(orderView.Height<200)
            {
                orderView.Height = 200;

            }
            if (orderView.Width < 80)
            {
                orderView.Width = 80;
            }
            #endregion

            //برای عرض و ارتفاع جدید
            //برای گرد کردن عدد به سمت بالا
            int aa = orderView.Height %  10;
            if (aa != 0)
            {
                int b = 10 - aa;
                orderView.Height = orderView.Height + b;
            }
            else
            {
                orderView.Height = orderView.Height;
            }
            //=================
            //برای گرد کردن عدد به سمت بالا
            int cc = orderView.Width % 10;
            if (cc != 0)
            {
                int d = 10 - cc;
                orderView.Width = orderView.Width + d;
            }
            else
            {
                orderView.Width = orderView.Width;
            }

            var detailRemove = await _orderRepository.GetCalculationDetails();
            foreach (var item in detailRemove)
            {
                _calculationDetailRepository.RemoveEntity(item);
            }

            _orderService.DeleteOrderDetail(userId);
            _orderService.DeleteOrder(userId);
            decimal resultTotalCost = 0;
            decimal resultCost = 0;
;
            // فهمیدم فرمولاش چیه
            var calculation = await _calculationRepository.CalculationByCategory(orderView.CategoryId,orderView.SubCategoryId);
            int test = 0;
            if (calculation != null)
            {
                foreach (var item in calculation)
                {
                    resultCost = 0;
                    #region پرده طلقی ایرانی
                    if (item.CalculationId == 1)
                    {
                        decimal a1 = 0;

                        decimal cost = await _calculationRepository.GetPriceById(1);
                        //resultCost = (((orderView.Height + 10) * orderView.Width) * cost) / 10000;
                        decimal a = oldHeight + 10;
                        decimal b = a * orderView.Width;
                        decimal c = b * cost;
                        decimal d = c / 10000;
                        a1 = d;
                        resultCost = d;

                    }
                    #endregion
                    #region پرده طلقی خارجی
                    else if (item.CalculationId == 2)
                    {
                        decimal cost = await _calculationRepository.GetPriceById(2);
                        resultCost = (((oldHeight + 10) * orderView.Width) * cost) / 10000;
                    }
                    #endregion
                    #region پرده توری یک لایه
                    else if (item.CalculationId == 3)
                    {
                        decimal cost = await _calculationRepository.GetPriceById(3);
                        resultCost = (((oldHeight + 10) * orderView.Width) * cost) / 10000;
                    }
                    #endregion
                    #region پرده توری دو لایه
                    else if (item.CalculationId == 4)
                    {
                        test = item.CalculationId;

                        decimal result = 0;
                        decimal cost = await _calculationRepository.GetPriceById(4);
                        // resultCost =((((orderView.Height + 10) * 40) / 10000) * cost);

                        decimal a = oldHeight + 10;
                        decimal b = a * 40;
                        decimal c = b / 10000;
                        decimal d = c * cost;
                        //جمع قیمت پرده توری + نصف گان
                        decimal e = (await _calculationRepository.GetGanPrice(oldHeight) / 2);
                        decimal pp = 0;
                        pp = d + e;

                        resultCost += pp;
                        decimal cost1 = await _calculationRepository.GetPriceById(3);
                        resultCost += (((orderView.Height + 10) * orderView.Width) * cost1) / 10000;
                        resultCost += await _calculationRepository.GetZipper5Price(orderView.Width);
                        resultCost += await _calculationRepository.GetZipper2Price(oldHeight);
                        resultCost += await _calculationRepository.GetChodonPrice(orderView.Width);
                        resultCost += await _calculationRepository.GetGanPrice(oldHeight);
                        resultCost += await _calculationRepository.GetMagnetPrice(oldHeight);
                        resultCost += await _calculationRepository.GetGlue4Price(orderView.Width);
                        resultCost += await _calculationRepository.GetGlue2Price(oldHeight);
                        resultCost += await _calculationRepository.GetWagePrice(12);
                        resultCost += await _calculationRepository.GetWagePrice(13);
                    }
                    #endregion
                    #region زیپ چسب 5 سانت
                    else if (item.CalculationId == 5)
                    {
                        resultCost += await _calculationRepository.GetZipper5Price(orderView.Width);
                    }
                    #endregion
                    #region زیپ چسب 2.5 سانت
                    else if (item.CalculationId == 6)
                    {
                        resultCost += await _calculationRepository.GetZipper2Price(oldHeight);
                    }
                    #endregion
                    #region جودون
                    else if (item.CalculationId == 7)
                    {
                        resultCost += await _calculationRepository.GetChodonPrice(orderView.Width);
                    }
                    #endregion
                    #region گان
                    else if (item.CalculationId == 8)
                    {
                        resultCost += await _calculationRepository.GetGanPrice(oldHeight);
                    }
                    #endregion
                    #region آهنربا
                    else if (item.CalculationId == 9)
                    {
                        resultCost += await _calculationRepository.GetMagnetPrice(oldHeight);
                    }
                    #endregion
                    #region چسب 2 طرفه 4 سانت
                    else if (item.CalculationId == 10)
                    {
                        resultCost += await _calculationRepository.GetGlue4Price(orderView.Width);
                    }
                    #endregion
                    #region چسب 2 طرفه 2 سانت
                    else if (item.CalculationId == 11)
                    {
                        resultCost += await _calculationRepository.GetGlue2Price(oldHeight);
                    }
                    #endregion
                    #region اجرت دوخت
                    else if (item.CalculationId == 12)
                    {
                        resultCost += await _calculationRepository.GetWagePrice(12);
                    }
                    #endregion
                    #region اجرت بسته بندی
                    else if (item.CalculationId == 13)
                    {
                        resultCost += await _calculationRepository.GetPackagingPrice(13);
                    }
                    #endregion

                    var calculationDetail = new CalculationDetailModel
                    {
                        OrderId = 0,
                        ProductSelectedCalculationId = item.Id,
                        Cost = resultCost,
                        TotalCost = resultCost * orderView.Count,
                    };
                    await _orderRepository.AddCalculationDetail(calculationDetail);

                    resultTotalCost += resultCost;
                }

                resultPrice = resultTotalCost;
                #region پیدا کردن آخرین  مبلغ کارمزد
                var value = _wageService.WageVlaue();
                #endregion
                #region مبلغ کل + مبلغ کارمزد
                decimal resultWage = 0;
                if (test==4)
                {
                     resultWage = ((resultTotalCost * 100) / 100);
                }
                else
                {
                     resultWage = ((resultTotalCost * value) / 100);
                }
                #endregion
                #region سه تا رقم آخر مبلغ صفر میکنه
                resultTotalCost += Math.Floor(resultWage / 1000) * 1000;
                #endregion

                var order = new OrderModel
                {
                    UserId = userId,
                    CategoryId = orderView.CategoryId,
                    SubCategoryId=orderView.SubCategoryId,
                    Height = oldHeight,
                    Width = oldWidth,
                    Price= resultPrice,
                    Count = orderView.Count,
                    //سه تا رقم آخر مبلغ صفر  میکنه
                    Cost = Math.Floor(resultTotalCost / 1000) * 1000,
                    //سه تا رقم آخر مبلغ صفر  میکنه
                    TotalCost = Math.Floor(orderView.Count * resultTotalCost / 1000) * 1000,
                };
                await _orderRepository.AddProductToOrder(order);

                var orderdetail = new OrderDetailModel
                {
                    OrderId = order.Id,
                    //سه تا رقم آخر مبلغ صفر  میکنه
                    Cost = Math.Floor(resultTotalCost / 1000) * 1000,
                    //سه تا رقم آخر مبلغ صفر  میکنه
                    TotalCost = Math.Floor(orderView.Count * resultTotalCost / 1000) * 1000,
                };
                await _orderRepository.AddOrderDetail(orderdetail);

                ViewBag.id=order.Id;
                var details = await _orderRepository.GetCalculationDetails();

                details.ForEach(
                    c => c.OrderId = order.Id);
                foreach (var item in details)
                {
                    _calculationDetailRepository.UpdateEntity(item);
                }
                await _calculationDetailRepository.SaveChanges();
            }
            #region سه تا رقم آخر صفر میشه
            ViewBag.resultTotalCost = Math.Floor(resultTotalCost / 1000) * 1000;
            #endregion
            var orderId = ViewBag.id;
            var category = _productService.GetCategoryForManageProduct(1);
            ViewData["Categories"] = new SelectList(category, "Value", "Text");

            var subCategory = _productService.GetSubCategoryForManageProduct(int.Parse(category.First().Value));
            ViewData["SubCategories"] = new SelectList(subCategory, "Value", "Text");
            return Redirect("/UserPanel/Orders/ShowOrder/" + orderId);
        }
    }
}