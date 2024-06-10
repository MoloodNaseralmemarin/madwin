using Microsoft.AspNetCore.Mvc;
using Shop2City.Core.DTOs.ShopCart;
using Shop2City.Core.DTOs.ShoppingCart;
using Shop2City.Core.Helpers;
using Shop2City.Core.Services.Products;
using Shop2City.DataLayer.Context;
using Shop2City.DataLayer.Entities.Products;

namespace Shop2City.WebHost.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductService _productService;
        private readonly Shop2CityContext _context;

        public ShoppingCartController(IProductService productService, Shop2CityContext context)
        {
            _productService = productService;
            _context = context;
        }

        #region اضافه به سبد خرید صفحه اول
        // صدا زدن این متد با جی کوئری ایجکس اتفاق می افتد
        public Tuple<int, int> AddToCartMain(int id)
        {
            var product = _productService.GetProductByProductId(id);
            //ساخت لیستی از ویو مدل
            List<ShopCartitemTest> cart = new List<ShopCartitemTest>();
            //یعنی کاربر اولین خریدش نیست و قبلا محصولی انتخاب کرده
            //یعنی شسن از قبل پره
            if (HttpContext.Session.Get("Cart") != null)
            {
                // اطلاعات نشون بده
                cart = HttpContext.Session.GetJson<List<ShopCartitemTest>>("Cart");
            }
            //اگر محصول وجود داشت یدونه به تعدادش اضافه میشه اگر نبود ی دونه محصول جدید اضافه میشه به سبد خرید
            //یعنی از محصول انتخاب شده قبلا انتخاب کردیم و سشن پره
            if (cart.Any(p => p.productId == id))
            {
                //اول محصول با شناسه وارد شده پیدا میکنیم و یدونه به تعدادش اضافه میکنیم
                //جایگاه محصول توی لیست پیدا میکنیم
                var index = cart.FindIndex(p => p.productId == id);
                //یدونه به تعداد محصول موجود اضافه میکنیم
                //یعنی ی محصول با یک شناسه و به تعداد 2
                cart[index].count += 1;
            }
            else
            {
                //اولین باره که این محصول انتخاب کرده است
                cart.Add(new ShopCartitemTest()
                {
                    productId = id,
                    count = 1,
                    price =product.Price
                });
            }

            //به روز رسانی سشن
            HttpContext.Session.SetJson("Cart", cart);
            //برگردان تعدا اقلام موجود در سشن
            // return cart.Sum(p => p.count);
            return Tuple.Create(cart.Sum(p => p.count), cart.Sum(p => p.price));
        }
        #endregion

        #region  برای نمایش محصولات اضافه به سبد خرید
        // صدا زدن این متد با جی کوئری ایجکس اتفاق می افتد
        public Tuple<int,int> AddToCartShow(int id,int count)
        {
            var product = _productService.GetProductByProductId(id);
            //ساخت لیستی از ویو مدل
            List<ShopCartitemTest> cart = new List<ShopCartitemTest>();
            //یعنی کاربر اولین خریدش نیست و قبلا محصولی انتخاب کرده
            //یعنی شسن از قبل پره
            if (HttpContext.Session.Get("Cart") != null)
            {
                // اطلاعات نشون بده
                cart = HttpContext.Session.GetJson<List<ShopCartitemTest>>("Cart");
            }
            //اگر محصول وجود داشت یدونه به تعدادش اضافه میشه اگر نبود ی دونه محصول جدید اضافه میشه به سبد خرید
            //یعنی از محصول انتخاب شده قبلا انتخاب کردیم و سشن پره
            if (cart.Any(p => p.productId == id))
            {
                //اول محصول با شناسه وارد شده پیدا میکنیم و یدونه به تعدادش اضافه میکنیم
                //جایگاه محصول توی لیست پیدا میکنیم
                var index = cart.FindIndex(p => p.productId == id);
                //یدونه به تعداد محصول موجود اضافه میکنیم
                //یعنی ی محصول با یک شناسه و به تعداد 2
                cart[index].count +=count;
                cart[index].price += count * product.Price;
            }
            else
            {
                //اولین باره که این محصول انتخاب کرده است
                cart.Add(new ShopCartitemTest()
                {
                    productId = id,
                    count = count,
                    price =count * product.Price
                });
            }

            //به روز رسانی سشن
            HttpContext.Session.SetJson("Cart", cart);
            //برگردان تعدا اقلام موجود در سشن
            // return cart.Sum(p => p.count);
            return Tuple.Create(cart.Sum(p => p.count), cart.Sum(p => p.price));
        }
        #endregion
        #region ShowCart
        //اینو متوجه شدم ولی متوجه نشدم که باز چرا براش ی لیست درست کردیم
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetJson<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();

            CartViewModel cartViewModel = new()
            {
                CartItemViewModels = cart,
                GrandTotal = cart.Sum(x => x.Quantity * x.Price)
            };

            return View(cartViewModel);
        }
        #endregion
        #region افزایش به سبد خرید
        public IActionResult Increase(int id)
        {
            var product = _productService.GetProductByProductId(id);

            var cart = HttpContext.Session.GetJson<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();//منظورشو کامل متوجه نشدم

            var cartItem = cart.FirstOrDefault(c => c.ProductId == id);

            if (cartItem == null)
            {
                cart.Add(new CartItemViewModel(product));
            }
            else
            {
                cartItem.Count += 1;
                cartItem.Price += product.Price;


            }

            HttpContext.Session.SetJson("Cart", cart);
            return Redirect(Request.Headers["Referer"].ToString());
        }
        #endregion
        #region کاهش از سبد خرید
        public IActionResult Decrease(int id)
        {
            var product = _productService.GetProductByProductId(id);
            var cart = HttpContext.Session.GetJson<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();

            var cartItem = cart.FirstOrDefault(c => c.ProductId == id);

            if (cartItem?.Count > 1)
            {
                cartItem.Count -= 1;
                cartItem.Price -= product.Price;

            }
            else
            {
                cart.RemoveAll(p => p.ProductId == id);
            }

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }
        #endregion
        #region حذف از سبد خرید
        public IActionResult Remove(int id)
        {
            var cart = HttpContext.Session.GetJson<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();

            cart.RemoveAll(p => p.ProductId == id);

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }
        #endregion
        #region حذف سبد خرید
        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");
            return Redirect(Request.Headers["Referer"].ToString());
        }
        #endregion
        #region نمایش سبد خرید
        public IActionResult ShowOrder()
        {
            List<ShopCartShowViewModel> list = new List<ShopCartShowViewModel>();
            if (SessionHelper.GetObjectFromJson<List<ShopCartItemViewModel>>(HttpContext.Session, "Shopcart") != null)
            {
                List<ShopCartItemViewModel> cart = SessionHelper.GetObjectFromJson<List<ShopCartItemViewModel>>(HttpContext.Session, "Shopcart");

                foreach (var item in cart)
                {
                    var product = _productService.GetProductByProductId(item.productId);

                    list.Add(new ShopCartShowViewModel()
                    {
                        //productImage =item.Product.ProductImageName,
                        productGuantity = ViewBag.Count,
                        productId = item.productId,
                        productTitleFa = product.Title,
                    });
                }
            }
            return View(list);
        }


        #endregion
        #region پاک کردن از سبد خرید از صفحه اصلی
        public ActionResult RemoveFromCartFromIndex(int id)
        {
            List<ShopCartItemViewModel> cart = SessionHelper.GetObjectFromJson<List<ShopCartItemViewModel>>(HttpContext.Session, "Shopcart");
            int index = cart.FindIndex(p => p.productId == id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "Shopcart", cart);
            return Redirect("/");
        }
        #endregion
        #region نمایش لیست خرید
        public IActionResult ShowCart()
        {
            List<ShopCartTest> list = new List<ShopCartTest>();

            if (HttpContext.Session.Get("Cart") != null)
            {
                List<ShopCartitemTest> cart = HttpContext.Session.GetJson<List<ShopCartitemTest>>("Cart");
                foreach (var shopCartItem in cart)
                {
                    var product = _productService.GetProductByProductId(shopCartItem.productId);
                    list.Add(new ShopCartTest()
                    {
                        ProductId = shopCartItem.productId,
                        Count = shopCartItem.count,
                        Title = product.Title,
                        Price = product.Price,
                        Sum = shopCartItem.count * product.Price
                    });
                }
            }
            return View(list);
        }
        #endregion
        #region حذف آیتم از سبد خرید
        public IActionResult RemoveFromCart(int id)
        {
            //توی ی لیستی سشن دریافت می کنیم
            List<ShopCartitemTest> cart = HttpContext.Session.GetJson<List<ShopCartitemTest>>("Cart");
            //اول محصول با شناسه وارد شئه پیدا میکنیم و یدونه به تعدادش اضافه میکنیم
            //جایگاه محصول توی لیست پیدا میکنیم
            var index = cart.FindIndex(p => p.productId == id);
            cart.RemoveAt(index);
            HttpContext.Session.SetJson("Cart", cart);
            return RedirectToAction("ShowCart");
        }
        #endregion
        #region نمایش تعداد اقلام در سبد خرید
        public Tuple<int,int> ShopCartCount()
        {
            int count = 0;
            int Price = 0;
            if (HttpContext.Session.Get("Cart") != null)
            {
                //توی ی لیستی سشن دریافت می کنیم
                List<ShopCartitemTest> cart = HttpContext.Session.GetJson<List<ShopCartitemTest>>("Cart");
                count = cart.Sum(p => p.count);
                Price = cart.Sum(p => p.price);
            }

            return Tuple.Create(count,Price);
        }
        #endregion












        public ActionResult UpdateCartFromIndex(int id, int count)
        {
            List<ShopCartItemViewModel> cart = SessionHelper.GetObjectFromJson<List<ShopCartItemViewModel>>(HttpContext.Session, "Shopcart");
            int index = cart.FindIndex(p => p.productId == id);
            cart[index].quantity = count;
            SessionHelper.SetObjectAsJson(HttpContext.Session, "Shopcart", cart);
            return Redirect("/");
        }










        public JsonResult ShopCartQuantity()
        {
            var cart = HttpContext.Session.GetJson<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();
            if (cart.Count == 0)
            {
                return Json(new { Success = true, Counter = 0, Cart = 0 });
            }
            return Json(new { Success = true, Counter = cart.Sum(p => p.Quantity), Cart = cart.Sum(p => p.Total) });
        }

        public JsonResult UpdateToCart(int productId, int count)
        {
            var product = _context.Products.Find(productId);

            var cart = HttpContext.Session.GetJson<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();

            var cartItem = cart.FirstOrDefault(c => c.ProductId == productId);

            if (cartItem == null)
            {
                cart.Add(new CartItemViewModel(product));
            }
            else
            {
                cartItem.Quantity = count;
            }
            return Json(new { Success = true, Counter = cart.Sum(p => p.Quantity), Cart = cart.Sum(p => p.Price) });
        }
    }

}
