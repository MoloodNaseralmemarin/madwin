using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop2City.Core.Services.Products;
using Shop2City.DataLayer.Entities.Products;

namespace Shop2City.WebHost.Pages.Admin.Products
{
    //[PermissionChecker(12)]
    public class EditModel : PageModel
    {
        private IProductService _productService;

        public EditModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public Product editProduct { get; set; }

        public void OnGet(int id)
        {
            editProduct = _productService.GetProductByProductId(id);

            #region Categories
            var groups = _productService.GetGroupForManageProduct();
            ViewData["ProductGroup"] = new SelectList(groups, "Value", "Text",editProduct.ProductGroupId);

            var category = _productService.GetCategoryForManageProduct(int.Parse(groups.First().Value));
            ViewData["Categories"] = new SelectList(category, "Value", "Text", editProduct.Category);

         
                var subCategory = _productService.GetSubCategoryForManageProduct(int.Parse(category.First().Value));
                ViewData["SubCategory"] = new SelectList(subCategory, "Value", "Text", editProduct.SubCategoryId ?? 0);

            #endregion
        }

        public IActionResult OnPost()
        {
            var userName = User.Identity.Name;
            _productService.UpdateProduct(editProduct,userName);
            return RedirectToPage("Index");
        }
    }
}