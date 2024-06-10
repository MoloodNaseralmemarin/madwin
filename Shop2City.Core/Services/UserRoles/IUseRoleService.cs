using System;
using System.Collections.Generic;
using System.Text;
using Shop2City.DataLayer.Entities.Users;

namespace Shop2City.Core.Services.UserRoles
{
     public interface IUseRoleService
    {
        int AddUseRole(UserRole userRole);
    }
}
