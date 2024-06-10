using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop2City.Core.Services.Factors;
using Shop2City.Core.Services.Orders;
using Shop2City.Core.Services.Products;
using Shop2City.Core.Services.Users;


namespace Shop2City.WebHost.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly IFactorService _factorService;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        public HomeController(IProductService productService, IFactorService factorService, IUserService userService, IOrderService orderService)
        {
            _productService = productService;
            _factorService = factorService;
            _userService = userService;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("ShowProduct/{id}")]
        public IActionResult ShowProduct(int id)
        {
            var product = _productService.GetProductForShow(id);
            var productGalleries = _productService.GetProductActiveGalleries(id);


            return View(Tuple.Create(product,productGalleries));
        }

        [HttpGet]
        public IActionResult ContactUs()
        {
            return View();
        }

        #region GetCategory

        //گروه اصلی
        public IActionResult GetCategory(int id)
        {
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem() {Text = "گروه اصلی را انتخاب کنید", Value = ""}
            };
            list.AddRange(_productService.GetCategoryForManageProduct(id));
            return Json(new SelectList(list, "Value", "Text"));
        }

        #endregion

        #region GetSubCategory

        public IActionResult GetSubCategory(int id)
        {
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem() {Text = "گروه فرعی را انتخاب کنید", Value = ""}
            };
            list.AddRange(_productService.GetSubCategoryForManageProduct(id));

            return Json(new SelectList(list, "Value", "Text"));
        }

        #endregion



        public int GetPricePostByFactor(int typePostId)
        {
            var pricePost = _factorService.GetPricePostByTypePostId(typePostId);
            var user = _userService.GetUserByUserName(User.Identity.Name);
            var factorId = _factorService.GetFactorIdByUserId(user.Id);
            var factor = _factorService.GetFactorByFactorId(factorId);
            factor.typePostId= typePostId;
            factor.totalPrice = factor.FactorSum + pricePost;
            _factorService.UpdateFactor(factor);
            return pricePost;
        }

        public int GetPricePostByOrder(int typePostId)
        {
            var pricePost = _orderService.GetPricePostByTypePostId(typePostId);
            var user = _userService.GetUserByUserName(User.Identity.Name);
            var orderId = _orderService.GetOrderIdByUserId(user.Id);
            var order = _orderService.GetOrderByOrderId(orderId);
            order.TypePostId = typePostId;
            order.TotalCost = order.TotalCost + pricePost;
            _orderService.UpdateOrder(order);
            return pricePost;
        }
    }
}
