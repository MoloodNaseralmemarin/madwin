using Microsoft.AspNetCore.Mvc;
using Shop2City.Core.Services.Products;

namespace Shop2City.WebHost.ViewComponents
{
    public class CategoryThreeComponent : ViewComponent
    {
        private readonly IProductService _productService;
        public CategoryThreeComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = _productService.GetAllProductsByGroupId(6);
            if (!products.Any())
                return Content("");
            return await Task.FromResult((IViewComponentResult)View("CategoryThree", products));
        }
    }
}
