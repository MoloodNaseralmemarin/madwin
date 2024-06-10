using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop2City.Core.DTOs.Users;
using Shop2City.Core.Services.Users;

namespace Shop2City.WebHost.Pages.Admin.Users
{
  //  [PermissionChecker(2)]
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public UserForAdminViewModel userForAdmin { get; set; }

        public void OnGet(int pageId=1,string filterFirstName = "")
        {
            userForAdmin = _userService.GetAllUsers(pageId, filterFirstName);
        }

        public IActionResult OnPost(int userId)
        {
            _userService.DeleteUser(userId);
            return RedirectToPage("Index");
        }

    }
}