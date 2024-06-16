﻿
using System;
using System.Collections.Generic;
using System.Text;
using Shop2City.DataLayer.Entities.Permissions;
using Shop2City.DataLayer.Entities.Users;

namespace Shop2City.Core.Services.Permissions
{
    public interface IPermissionService
    {
        #region Roles
        List<Role> GetRoles();

        int AddRole(Role role);

        Role GetRoleById(int roleId);

        void UpdateRole(Role role);

        void DeleteRole(Role role);

        void AddRolesToUser(List<int> roleIds, int userId);

        void EditRolesToUser(int userId, List<int> roleIds);

        #endregion

        #region Permission
        List<Permission> GetPermissions();

        void AddPermissionsToRole(int roleId, List<int> permissions);

        List<int> PermissionsRole(int roleId);

        void UpdatePermissionsToRole(int roleId, List<int> permissions);

        bool CheckPermission(int permissionId, string userName);

        bool CheckPermission(int permissionId);
        #endregion
    }
}
