using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop2City.Core.Services.Wages;
using Shop2City.DataLayer.Entities.Ordering;

namespace Shop2City.WebHost.Pages.Admin.Wages
{
    public class IndexModel : PageModel
    {
        private readonly IWageService _wageService;
        public IndexModel(IWageService wageService)
        {
            _wageService = wageService;
        }

        public List<WageModel> listWage { get; set; }
        public void OnGet()
        {
            listWage = _wageService.GetWages();
        }
    }
}
