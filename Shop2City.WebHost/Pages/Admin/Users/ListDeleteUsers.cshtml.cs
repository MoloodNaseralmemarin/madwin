using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop2City.Core.DTOs.Users;
using Shop2City.Core.Services.Users;

namespace Shop2City.Web.Pages.Admin.Users
{
    public class ListDeleteUsersModel : PageModel
    {
        private IUserService _userService;

        public ListDeleteUsersModel(IUserService userService)
        {
            _userService = userService;
        }

        public UserForAdminViewModel userForAdmin { get; set; }

        public async void OnGet(int pageId = 1, string filterUserName = "", string filterEmail = "")
        {
            userForAdmin =await _userService.GetDeleteUsers(pageId,filterEmail,filterUserName);
        }

    }
}