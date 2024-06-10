using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop2City.Core.Security;
using Shop2City.Core.Services.Permissions;
using Shop2City.DataLayer.Entities.Users;

namespace Shop2City.Web.Pages.Admin.Roles
{
    //[PermissionChecker(6)]
    public class IndexModel : PageModel
    {
        private IPermissionService _permissionService;

        public IndexModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public List<Role> listRoles { get; set; }

       
        public void OnGet()
        {
            listRoles = _permissionService.GetRoles();
        }

       
    }
}