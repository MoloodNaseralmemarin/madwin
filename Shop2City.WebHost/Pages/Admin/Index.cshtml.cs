using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop2City.Core.Security;

namespace Shop2City.WebHost.Pages.Admin
{
    [PermissionChecker(1)]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
