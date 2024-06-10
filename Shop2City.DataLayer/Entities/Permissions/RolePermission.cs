using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Shop2City.DataLayer.Entities.Permissions;
using Shop2City.DataLayer.Entities.Users;

namespace Shop2City.DataLayer.Entities.Permissions
{
   public class RolePermission
    {
        [Key]
        public int rolePermissionId { get; set; }
        public int roleId { get; set; }
        public int permissionId { get; set; }

        public Role role { get; set; }
        public Permission permission { get; set; }

    }
}
