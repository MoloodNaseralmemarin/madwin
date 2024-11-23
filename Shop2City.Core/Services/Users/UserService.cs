using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shop2City.Core.DTOs.Account;
using Shop2City.Core.DTOs.Users;
using Shop2City.Core.Security;
using Shop2City.DataLayer.Context;
using Shop2City.DataLayer.Entities;
using Shop2City.DataLayer.Entities.Orders;
using Shop2City.DataLayer.Entities.Users;

namespace Shop2City.Core.Services.Users
{
    public class UserService : IUserService
    {

        private readonly Shop2CityContext _context;
        private readonly ILogger<UserService> _logger;

        #region Ctor
        public UserService(Shop2CityContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }
        #endregion
        public async Task<int> AddUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }

            try
            {
                if (string.IsNullOrEmpty(user.FirstName) ||
                    string.IsNullOrEmpty(user.LastName) ||
                    string.IsNullOrEmpty(user.CellPhone) ||
                    string.IsNullOrEmpty(user.Address) ||
                    string.IsNullOrEmpty(user.UserName) ||
                    string.IsNullOrEmpty(user.Password))
                {
                    throw new ArgumentException("User FirstName and LastName and CellPhone and Address and UserName and Password are required.");
                }

                #region Add user to database
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                _logger.LogInformation("User with ID {UserId} added successfully.", user.Id);
                return user.Id;
                #endregion
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "An error occurred while adding a user.");
                throw new ApplicationException("There was an issue saving the user data.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                throw new ApplicationException("An unexpected error occurred while adding the user.");
            }
        }


        public async Task<int> AddUserFromAdmin(CreateUserViewModel createUser)
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
                LastUpdateDate = DateTime.Now,
                Description = "توضیحی درج نشده است",
                UserName = createUser.userName
            };

            return await AddUserAsync(user);
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

        public async Task DeleteUser(int userId)
        {
            User user = await GetUserByUserId(userId);
            user.IsDelete = true;
            UpdateUser(user);
        }
        public async Task EditUserFromAdmin(EditUserViewModel editUser)
        {
            var user =await GetUserByUserId(editUser.userId);
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

        public async Task<InformationUserViewModel> GetInformationUser(int userId)
        {
            var user =await GetUserByUserId(userId);
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







        public async Task<User> GetUserByUserId(int userId)
        {
            return await _context.Users.FindAsync(userId);
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

        public async void UpdateAddressUser(int userId, string address)
        {
            var user =await GetUserByUserId(userId);
            user.Address = address;
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        #region SMS Part
        public async Task AddSMSAsync(Sms sms)
        {
            if (sms == null)
            {
                throw new ArgumentNullException(nameof(sms), "Sms object cannot be null");
            }

            try
            {
                // افزودن داده به پایگاه داده
                await _context.Sms.AddAsync(sms);
                await _context.SaveChangesAsync(); // ذخیره تغییرات به صورت غیرهمزمان
            }
            catch (DbUpdateException ex)
            {
                // در صورتی که خطای مربوط به پایگاه داده پیش آید
                _logger.LogError(ex, "Error occurred while adding SMS to the database.");
                throw new Exception("An error occurred while saving SMS data. Please try again later.");
            }
            catch (Exception ex)
            {
                // در صورتی که خطای عمومی رخ دهد
                _logger.LogError(ex, "Unexpected error occurred while adding SMS.");
                throw new Exception("An unexpected error occurred. Please try again later.");
            }
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

        public async Task<int> GetUserIdByUserId(int userId)
        {
            return await _context.Users
                .Where(u => u.Id == userId)
                .Select(u => u.Id)
                .SingleAsync();
        }

        public async Task<int> GetUserIdByFactorId(int factorId)
        {
            var userId=await _context.Factors
                .Where(u => u.Id == factorId)
                .Select(u => u.UserId)
                .SingleAsync();
            return userId;
        }

        public async Task<int> GetUserIdByOrderId(int orderId)
        {
            var userId = await _context.Orders
                .Where(u => u.Id == orderId)
                .Select(u => u.UserId)
                .SingleAsync();
            return userId;
        }

        public async Task<string> GetCellPhoneByUserId(int userId)
        {
            var cellPhone = await _context.Users
                .Where(u => u.Id == userId)
                .Select(u => u.CellPhone)
                .SingleAsync();
            return cellPhone;
        }

        void IUserService.DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        Task<UserForAdminViewModel> IUserService.GetDeleteUsers(int pageId, string filterEmail, string filterUserName)
        {
            throw new NotImplementedException();
        }

        void IUserService.EditUserFromAdmin(EditUserViewModel editUser)
        {
            throw new NotImplementedException();
        }
    }
    #endregion

}
