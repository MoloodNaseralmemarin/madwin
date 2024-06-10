using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop2City.Core.Services.Products;
using Shop2City.DataLayer.Entities.Products;

namespace Shop2City.Web.Pages.Admin.Products
{
    public class DeleteModel : PageModel
    {
        private readonly IProductService _productService;

        public DeleteModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public Product DeleteProduct { get; set; }
        public void OnGet(int id)
        {
            DeleteProduct = _productService.GetProductByProductId(id);
        }

        public IActionResult OnPost()
        {
            var userName = User.Identity.Name;
            _productService.DeleteProduct(DeleteProduct, userName);
            return RedirectToPage("Index");
        }


    }
}