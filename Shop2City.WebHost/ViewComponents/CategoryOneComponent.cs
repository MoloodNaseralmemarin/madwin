using Microsoft.AspNetCore.Mvc;
using Shop2City.Core.Services.Products;

namespace Shop2City.WebHost.ViewComponents
{
    public class CategoryOneComponent : ViewComponent
    {
        private readonly IProductService _productService;
        public CategoryOneComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = _productService.GetAllProductsByGroupId(4);
            if (!products.Any())
                return Content("");
            return await Task.FromResult((IViewComponentResult)View("CategoryOne", products));
        }
    }
}
