using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop2City.Core.DTOs.DisCounts;
using Shop2City.Core.Services.DisCounts;
using Shop2City.DataLayer.Entities.DisCounts;

namespace Shop2City.WebHost.Pages.Admin.DisCounts
{
    public class IndexModel : PageModel
    {
        private readonly IDisCountService _disCountService;
        public IndexModel(IDisCountService disCountService)
        {
            _disCountService = disCountService;
        }

        public List<DisCountViewModel> disCount { get; set; }


        public void OnGet()
        {

            disCount = _disCountService.GetAllDisCounts();
        }

        public IActionResult OnPost(int itemId)
        {
            if (_disCountService == null)
            {
                return NotFound();
            }
            var discount =  _disCountService.GetDisCountByDisCountId(itemId);
            if (discount == null)
            {
                return NotFound();
            }
            if (_disCountService == null)
            {

                return Page();
            }
            _disCountService.DeleteDisCount(itemId);
            TempData["AlertSuccess"] = "مورد با موفقیت حذف شد.";
            return RedirectToPage(nameof(Index), new { deletedItemId = itemId });
        }

    }
}
