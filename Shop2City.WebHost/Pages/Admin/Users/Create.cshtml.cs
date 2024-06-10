using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop2City.Const;
using Shop2City.Core.Convertors;
using Shop2City.Core.DTOs.Users;
using Shop2City.Core.Services.Permissions;
using Shop2City.Core.Services.Users;

namespace Shop2City.WebHost.Pages.Admin.Users
{
    //[PermissionChecker(3)]
    public class CreateModel : PageModel
    {
        private  IUserService _userService;
        private  IPermissionService _permissionService;

        public CreateModel(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }

        [BindProperty]
        public CreateUserViewModel createUser { get; set; }

        public void OnGet()
        {
            ViewData["Roles"] = _permissionService.GetRoles();
        }

        public IActionResult OnPost(List<int> SelectedRoles)
        {
            if (!ModelState.IsValid)
                return Page();
            if (_userService.IsExistCellPhone(createUser.cellPhone))
            {
                ModelState.AddModelError("cellPhone", ErrorMessage.InvalidCellPhone);
                ViewData["Roles"] = _permissionService.GetRoles();
                return Page();
            }



            if (createUser.userName != null && _userService.IsExistUserName(createUser.userName))
            {
                ModelState.AddModelError("userName", ErrorMessage.InvalidUserName);
                ViewData["Roles"] = _permissionService.GetRoles();
                return Page();
            }
            int userId = _userService.AddUserFromAdmin(createUser);
            //TODO: Add Roles
            #region Add Roles
            _permissionService.AddRolesToUser(SelectedRoles, userId);
            #endregion
            return Redirect("/Admin/Users");

        }
    }
}