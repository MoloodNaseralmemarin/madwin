using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop2City.Core.Convertors;
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

        public IActionResult OnPost(string startDate, string endDate)
        {
            if (startDate == null || endDate == null)
            {
                disCount.startDate = DateTime.Now.Date;
                disCount.endDate = DateTime.Now.Date;
            }
            else
            {
                disCount.startDate = startDate.ToGreDateTime().Date;
                disCount.endDate = endDate.ToGreDateTime().Date;
            }
            _disCountService.AddDisCount(disCount);
            return Redirect("/Admin/DisCounts");


        }
    }
}
