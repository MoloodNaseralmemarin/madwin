using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop2City.Core.DTOs.Users;
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

        public List<DisCount> disCount { get; set; }
        public void OnGet()
        {
            disCount = _disCountService.GetAllDisCounts();
        }
    }
}
