using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop2City.Core.DTOs.Account;
using Shop2City.Core.Security;
using Shop2City.Core.Services.UserPanel;
using Shop2City.Core.Services.Users;

namespace Shop2City.WebHost.Pages.Admin.Users
{
   // [PermissionChecker(5)]
    public class DeleteModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IUserPanelService _userPanelService;
        public DeleteModel(IUserService userService, IUserPanelService userPanelService)
        {
            _userService = userService;
            _userPanelService = userPanelService;
        }

        public InformationUserViewModel informationUser { get; set; }
        public void OnGet(int id)
        {
            ViewData["UserId"] = id;
          //  informationUser = _userPanelService.GetInformationUser(id);
        }

        public IActionResult OnPost(int UserId)
        {
            _userService.DeleteUser(UserId);
            return RedirectToPage("Index");
        }
    }
}