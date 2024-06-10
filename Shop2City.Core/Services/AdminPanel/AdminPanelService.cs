using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop2City.Core.Convertors;
using Shop2City.Core.DTOs.Users;
using Shop2City.Core.Generator;
using Shop2City.Core.Security;
using Shop2City.Core.Services.Users;
using Shop2City.DataLayer.Context;
using Shop2City.DataLayer.Entities.Users;

namespace Shop2City.Core.Services.AdminPanel
{
    public class AdminPanelService : IAdminPanelService
    {
        private readonly Shop2CityContext _context;
        private readonly IUserService _userService;
        public AdminPanelService(Shop2CityContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }
        public int AddUserFromAdmin(CreateUserViewModel createUser)
        {
            var user = new User
            {
                CellPhone = createUser.cellPhone,
                TelPhone = createUser.tellPhone,
                Address = createUser.address,
                IsDelete = false,
                FirstName = createUser.firstName,
                LastName = createUser.lastName,
                Password = PasswordHelper.EncodePasswordMd5(createUser.password),
                CreateDate = DateTime.Now,
                LastUpdateDate=DateTime.Now,
                Description="توضیحی درج نشده است",
                UserName = createUser.userName
            };
            return _userService.AddUser(user);
        }

        public void DeleteUserFromAdmin(int userId)
        {
            User user = _userService.GetUserByUserId(userId);
            user.IsDelete = true;
            _userService.UpdateUser(user);
        }

        public void EditUserFromAdmin(EditUserViewModel editUser)
        {
            var user = _userService.GetUserByUserId(editUser.userId);
            user.CellPhone = editUser.cellPhone;
            user.TelPhone = editUser.tellPhone;
            user.Address = editUser.address;
            editUser.firstName = editUser.firstName;
            editUser.lastName = editUser.lastName;
            if (!string.IsNullOrEmpty(editUser.password))
                editUser.password = PasswordHelper.EncodePasswordMd5(editUser.password);
        }


        public UserForAdminViewModel GetAllUsers(int pageId = 1, string filterFirstName = "", string filterLastName = "")
        {
            IQueryable<User> result = _context.Users;

            if (!string.IsNullOrEmpty(filterFirstName))
            {
                result = result
                    .Where(u => u.FirstName
                    .Contains(filterFirstName));
            }
            if (!string.IsNullOrEmpty(filterLastName))
            {
                result = result
                    .Where(u => u.LastName
                    .Contains(filterLastName));
            }
            #region Paging
            int take = 3;// تعداد نمایش اطلاعات در هر صفحه
            int skip = (pageId - 1) * take;
            UserForAdminViewModel list = new UserForAdminViewModel()
            {
                currentPage = pageId,
                countPage = (int)Math.Ceiling(decimal.Divide(result.Count(), take)),
                users = result.OrderBy(u => u.CreateDate)
                .Skip(skip)
                .Take(take)
                .ToList()
            };
            #endregion
            return list;
        }
        public UserForAdminViewModel GetDeleteUsers(int pageId = 1, string filterFirstName = "", string filterLastName = "")
        {

            IQueryable<User> result = _context.Users.IgnoreQueryFilters();

            if (!string.IsNullOrEmpty(filterFirstName))
            {
                result = result
                    .Where(u => u.FirstName
                    .Contains(filterFirstName));
            }
            if (!string.IsNullOrEmpty(filterLastName))
            {
                result = result
                    .Where(u => u.LastName
                    .Contains(filterLastName));
            }
            #region Paging
            int take = 3;// تعداد نمایش اطلاعات در هر صفحه
            int skip = (pageId - 1) * take;
            UserForAdminViewModel list = new UserForAdminViewModel()
            {
                currentPage = pageId,
                countPage = (int)Math.Ceiling(decimal.Divide(result.Count(), take)),
                users = result.OrderBy(u => u.CreateDate)
                .Skip(skip)
                .Take(take)
                .ToList()
            };
            #endregion
            return list;
        }

        public EditUserViewModel GetUserForShowEditMode(int userId)
        {
            return _context.Users
                .Where(u => u.Id == userId)
                .Select(u => new EditUserViewModel
                {
                    cellPhone = u.CellPhone,
                    tellPhone = u.TelPhone,
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    userId = u.Id,
                    userName = u.UserName,
                    address = u.Address,
                    userRoles = u.UserRoles.Select(r => r.roleId).ToList(),
                }).Single();
        }
    }
}
