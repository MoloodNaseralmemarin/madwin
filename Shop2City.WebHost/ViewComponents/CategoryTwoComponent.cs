using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop2City.Core.Services.Products;

namespace Shop2City.Web.ViewComponents
{
    public class CategoryTwoComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public CategoryTwoComponent(IProductService productService)
        {
            _productService = productService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = _productService.GetAllProductsByGroupId(5);
            if (!products.Any())
                return Content("");
            return await Task.FromResult((IViewComponentResult)View("CategoryTwo", products));
        }
    }
}

