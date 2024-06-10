using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop2City.Core.Services.DisCounts;
using Shop2City.DataLayer.Entities.DisCounts;

namespace Shop2City.WebHost.Pages.Admin.DisCounts
{
    public class CreateModel : PageModel
    {
        private readonly IDisCountService _disCountService;
        public CreateModel(IDisCountService disCountService)
        {
            _disCountService = disCountService;
        }
        [BindProperty]
        public DisCount disCount { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            _disCountService.AddDisCount(disCount);
            return Redirect("/Admin/DisCounts");
        }
    }
}
