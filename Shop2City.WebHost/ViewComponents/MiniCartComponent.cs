using Microsoft.AspNetCore.Mvc;
using Shop2City.Core.DTOs.ShoppingCart;
using Shop2City.Core.Helpers;

namespace Shop2City.WebHost.ViewComponents
{
    public class MiniCartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            List<CartItemViewModel> cart = HttpContext.Session.GetJson<List<CartItemViewModel>>("Cart");
            SmallCartViewModel smallCartVM;

            if (cart == null || cart.Count == 0)
            {
                smallCartVM = null;
            }
            else
            {
                smallCartVM = new()
                {
                    NumberOfItems = cart.Sum(x => x.Quantity),
                    TotalAmount = cart.Sum(x => x.Quantity * x.Price)
                };
            }

            return View(smallCartVM);
        }
    }
}