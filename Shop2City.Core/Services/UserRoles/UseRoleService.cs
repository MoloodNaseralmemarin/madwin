using System;
using System.Collections.Generic;
using System.Text;
using Shop2City.DataLayer.Context;
using Shop2City.DataLayer.Entities.Users;

namespace Shop2City.Core.Services.UserRoles
{
    public class UseRoleService : IUseRoleService
    {
        private readonly Shop2CityContext _context;

        public UseRoleService(Shop2CityContext context)
        {
            _context = context;
        }
        public int AddUseRole(UserRole userRole)
        {
            _context.UserRoles.Add(userRole);
            _context.SaveChanges();
            return userRole.userRoleId;
        }
    }
}
