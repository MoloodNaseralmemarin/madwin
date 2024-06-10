using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop2City.Const;
using Shop2City.Core.Convertors;
using Shop2City.Core.DTOs.Users;
using Shop2City.Core.Security;
using Shop2City.Core.Services.Permissions;
using Shop2City.Core.Services.Users;

namespace Shop2City.Web.Pages.Admin.Users
{
   // [PermissionChecker(4)]
    public class EditModel : PageModel
    {
        private IUserService _userService;
        private IPermissionService _permissionService;

        public EditModel(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }
        [BindProperty]
        public EditUserViewModel editUser { get; set; }
        public void OnGet(int id)
        {
            editUser = _userService.GetUserForShowEditMode(id);
            ViewData["Roles"] = _permissionService.GetRoles();
        }

        public IActionResult OnPost(List<int> SelectedRoles)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Roles"] = _permissionService.GetRoles();
                return Page();
            }
            if (_userService.IsExistCellPhone(editUser.cellPhone))
            {
                ModelState.AddModelError("cellPhone", ErrorMessage.InvalidCellPhone);
                ViewData["Roles"] = _permissionService.GetRoles();
                return Page();
            }



            if (_userService.IsExistUserName(editUser.userName))
            {
                ModelState.AddModelError("userName", ErrorMessage.InvalidUserName);
                ViewData["Roles"] = _permissionService.GetRoles();
                return Page();
            }
            _userService.EditUserFromAdmin(editUser);

            //Edit Roles
            _permissionService.EditRolesToUser(editUser.userId,SelectedRoles);
            return RedirectToPage("Index");
        }
    }
}