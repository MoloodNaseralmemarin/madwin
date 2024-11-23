using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop2City.Core.Services.DisCounts;
using Shop2City.DataLayer.Entities.DisCounts;
using Shop2City.Core.Convertors;

namespace Shop2City.WebHost.Pages.Admin.DisCounts
{
    public class EditModel : PageModel
    {
        private readonly IDisCountService _disCountService;
        public EditModel(IDisCountService disCountService)
        {
            _disCountService = disCountService;
        }

        [BindProperty]
        public DisCount editDisCount { get; set; }
        public void OnGet(int id)
        {
            editDisCount = _disCountService.GetDisCountByDisCountId(id);
        }

        public IActionResult OnPost(string startDate,string endDate)
        {
            editDisCount.startDate = startDate.ToGreDateTime().Date;
            editDisCount.endDate = endDate.ToGreDateTime().Date;
            _disCountService.UpdateDisCount(editDisCount);
            return RedirectToPage("Index");
        }
    }
}
