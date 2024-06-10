using Microsoft.EntityFrameworkCore;
using Shop2City.Core.Convertors;
using Shop2City.Core.DTOs.Account;
using Shop2City.Core.DTOs.Users;
using Shop2City.Core.Generator;
using Shop2City.Core.Security;
using Shop2City.DataLayer.Context;
using Shop2City.DataLayer.Entities;
using Shop2City.DataLayer.Entities.Users;

namespace Shop2City.Core.Services.Users
{
    public class UserService : IUserService
    {

        private readonly Shop2CityContext _context;

        #region Ctor
        public UserService(Shop2CityContext context)
        {
            _context = context;
        }
        #endregion

     

        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.Id;
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

            return AddUser(user);
        }




        public void ChangeUserPassword(string userName, string newPassword)
        {
            var user = GetUserByUserName(userName);
            user.Password = PasswordHelper.EncodePasswordMd5(newPassword);
            UpdateUser(user);
        }

        public bool CompareOldPassword(string userName, string oldPassword)
        {
            string hashOldPassword = PasswordHelper.EncodePasswordMd5(oldPassword);
            return _context.Users
                .Any(u => u.UserName == userName && u.Password == hashOldPassword);
        }

        public void DeleteUser(int userId)
        {
            User user = GetUserByUserId(userId);
            user.IsDelete = true;
            UpdateUser(user);
        }
        public void EditUserFromAdmin(EditUserViewModel editUser)
        {
            var user = GetUserByUserId(editUser.userId);
            user.CellPhone = editUser.cellPhone;
            user.TelPhone = editUser.tellPhone;
            user.Address = editUser.address;
            editUser.firstName = editUser.firstName;
            editUser.lastName = editUser.lastName;
            if (!string.IsNullOrEmpty(editUser.password))
                editUser.password = PasswordHelper.EncodePasswordMd5(editUser.password);


        }

       

        public UserForAdminViewModel GetDeleteUsers(int pageId = 1, string filterEmail = "", string filterUserName = "")
        {
            IQueryable<User> result = _context.Users
                .IgnoreQueryFilters()
                .Where(u => u.IsDelete);



            if (!string.IsNullOrEmpty(filterUserName))
            {
                result = result.Where(u => u.UserName.Contains(filterUserName));
            }

            #region Show Item In Page
            int take = 20;
            int skip = (pageId - 1) * take;

            #endregion

            var list = new UserForAdminViewModel
            {
                currentPage = pageId,
                countPage = result.Count() / take,
                users = result.OrderBy(u => u.CreateDate).Skip(skip).Take(take).ToList()
            };


            return list;
        }

        public InformationUserViewModel GetInformationUser(int userId)
        {
            var user = GetUserByUserId(userId);
            InformationUserViewModel informationUser = new InformationUserViewModel();
            informationUser.userName = user.UserName;
            informationUser.registerDate = user.CreateDate;
            informationUser.fullName = user.FirstName + user.LastName;
            informationUser.cellPhone = user.CellPhone;
            return informationUser;
        }



        public int GetRoleIdByUserId(int userId)
        {
            return _context.UserRoles.SingleOrDefault(u => u.userId == userId).roleId;
        }







        public User GetUserByUserId(int userId)
        {
            return _context.Users.Find(userId);
        }

        public User GetUserByUserName(string userName)
        {
            if (userName == null) return null;
            var getUserId = _context.Users
                .SingleOrDefault(u => u.UserName == userName);
            return getUserId ?? null;

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

        public int GetUserIdByUserName(string userName)
        {
            return _context.Users
                .Single(u => u.UserName == userName).Id;
        }

        public UserForAdminViewModel GetAllUsers(int pageId = 1, string filterFirstName = "")
        {
            IQueryable<User> result = _context.Users;

            if (!string.IsNullOrEmpty(filterFirstName))
            {
                result = result
                    .Where(u => u.FirstName
                    .Contains(filterFirstName));
            }
            #region Show Item In Page
            const int take = 40;
            var skip = (pageId - 1) * take;
            #endregion
            var list = new UserForAdminViewModel
            {
                currentPage = pageId,
                countPage = result.Count() / take,
                users = result
                    .OrderBy(u => u.CreateDate)
                    .Skip(skip)
                    .Take(take)
                    .ToList()
            };
            return list;
        }

        public bool IsExistCellPhone(string cellPhone)
        {
            return _context.Users
                .Any(u => u.CellPhone == cellPhone);
        }



        public bool IsExistUserName(string userName)
        {
            return _context.Users
                .Any(u => u.UserName == userName);
        }

        public User LoginUser(LoginViewModel loginViewModel)
        {
            var hashPassword = PasswordHelper.EncodePasswordMd5(loginViewModel.password);
            //string email = FixedText.FixedEmail(loginViewModel.Email);
            return _context.Users
                .SingleOrDefault(u => u.UserName == loginViewModel.userName && u.Password == hashPassword);
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void UpdateAddressUser(int userId, string address)
        {
            var user = GetUserByUserId(userId);
            user.Address = address;
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        #region SMS Part
        public void AddSMS(Sms sms)
        {
            _context.Sms.Add(sms);
            _context.SaveChanges();
        }

        public int CountSMSs()
        {
            var count = _context.Sms.Count();
            return count;
        }

        public int CountUsers()
        {
            var countUser = _context.Users.Count();
            return countUser;
        }
        #endregion

    }
}
