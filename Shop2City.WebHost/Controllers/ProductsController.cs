using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop2City.Core.DTOs.Products;
using Shop2City.Core.DTOs.ShopCart;
using Shop2City.Core.DTOs.ShoppingCart;
using Shop2City.Core.Helpers;
using Shop2City.Core.Services.Factors;
using Shop2City.Core.Services.Orders;
using Shop2City.Core.Services.Products;
using Shop2City.Core.Services.ShoppingCart;
using Shop2City.Core.Services.Users;
using Shop2City.DataLayer.Context;
using Shop2City.DataLayer.Entities.Products;
using Shop2City.DataLayer.Entities.ShoppingCartItems;
using System.Drawing.Drawing2D;

namespace Shop2City.WebHost.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IFactorService _orderService;
        private readonly IUserService _userService;
        private readonly Shop2CityContext _context;


        public ProductsController(IProductService productService, IFactorService orderService, IUserService userService, Shop2CityContext context)
        {
            _productService = productService;
            _orderService = orderService;
            _userService = userService;
            _context = context;
        }
        public IActionResult Index(int pageId = 1, string filterProductTitleFa = ""
           ,List<int> selectedGroups = null)
        {
            ViewBag.selectedGroups = selectedGroups;
            ViewBag.FilterProductTitleFa = filterProductTitleFa;
            ViewBag.Groups = _productService.GetAllGroup();
            ViewBag.list = _productService.ShowMainProductGroups();
            ViewBag.pageId = pageId;
            ViewData["Referer"] = Request.Headers["Path"].ToString();
            return View(_productService.GetProduct(pageId, filterProductTitleFa, selectedGroups));
        }
        [Authorize]
        public IActionResult BuyProduct()
        
        {
            var user = _userService.GetUserByUserName(User.Identity.Name);
            var userId = _userService.GetUserIdByUserName(User.Identity.Name);
            _orderService.DeleteFactorDetail(userId);
            _orderService.DeleteFactor(userId);

            var cart = HttpContext.Session.GetJson<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();
            foreach (var item in cart)
            {

                _orderService.AddFactor(User.Identity.Name, item.ProductId,item.Count);
            }
            var order = _context.Factors
                .FirstOrDefault(o => o.userId == userId && !o.IsFinaly);
            return Redirect("/UserPanel/Factors/ShowFactor/"+order.FactorId);
        }
    }
}