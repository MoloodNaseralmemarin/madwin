using Shop2City.Core.DTOs.Account;
using Shop2City.Core.DTOs.Users;
using Shop2City.DataLayer.Entities;
using Shop2City.DataLayer.Entities.Users;

namespace Shop2City.Core.Services.Users
{
    public interface IUserService
    {
        #region IsExist....
        bool IsExistUserName(string userName);
        bool IsExistCellPhone(string cellPhone);
        #endregion

        #region RegisterUser
        Task<int> AddUserAsync(User user);
        void UpdateUser(User user);
        void DeleteUser(int userId);
        #endregion

        #region LoginUser
        User LoginUser(LoginViewModel loginViewModel);
        #endregion


        #region GetUserBy...
        Task<User> GetUserByUserId(int userId);
        int GetUserIdByUserName(string userName);

        Task<int> GetUserIdByUserId(int userId);

        Task<int> GetUserIdByFactorId(int factorId);

        Task<int> GetUserIdByOrderId(int orderId);

        Task<string> GetCellPhoneByUserId(int userId);


        int GetRoleIdByUserId(int userId);


        User GetUserByUserName(string userName);
        #endregion

        #region UserPanel


        bool CompareOldPassword(string userName, string oldPassword);

        void ChangeUserPassword(string userName, string newPassword);
        #endregion

        #region Admin Panel
        UserForAdminViewModel GetAllUsers(int pageId = 1, string filterFirstName = "");

        Task<UserForAdminViewModel> GetDeleteUsers(int pageId = 1, string filterEmail = "", string filterUserName = "");

        Task<int> AddUserFromAdmin(CreateUserViewModel createUser);

        EditUserViewModel GetUserForShowEditMode(int userId);

        void EditUserFromAdmin(EditUserViewModel editUser);


        #endregion


        Task AddSMSAsync(Sms sms);

        int CountSMSs();

        int CountUsers();

    }
}