using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop2City.Core.Services.Wages;
using Shop2City.DataLayer.Entities.Calculations;
using Shop2City.DataLayer.Entities.Ordering;


namespace Shop2City.Web.Pages.Admin.Wages
{
    public class CreateModel : PageModel
    {
        private readonly IWageService _wageService;

        public CreateModel(IWageService wageService)
        {
            _wageService = wageService;
        }

        [BindProperty]
        public WageModel editWage { get; set; }
        public void OnGet(int id)
        {
            editWage = _wageService.GetWagesById(id);
        }

        public  IActionResult OnPost()
        {
            _wageService.UpdateWage(editWage);
            return RedirectToPage("Index");
        }
    }
}
