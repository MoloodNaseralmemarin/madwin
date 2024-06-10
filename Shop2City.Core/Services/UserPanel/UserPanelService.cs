using Microsoft.EntityFrameworkCore;
using Shop2City.Core.DTOs.Account;
using Shop2City.Core.Generator;
using Shop2City.Core.Security;
using Shop2City.Core.Services.Users;
using Shop2City.DataLayer.Context;

namespace Shop2City.Core.Services.UserPanel
{
    public class UserPanelService : IUserPanelService
    {
        private readonly IUserService _userService;
        private readonly Shop2CityContext _context;
        public UserPanelService(IUserService userServic, Shop2CityContext context)
        {
            _userService = userServic;
            _context = context;
        }
        public InformationUserViewModel GetInformationUser(string username)
        {
            var user = _userService.GetUserByUserName(username);
            var informationUser = new InformationUserViewModel
            {
                userName = user.UserName,
                registerDate = user.CreateDate,
                fullName = user.FirstName + " " + user.LastName,
                cellPhone = user.CellPhone,
            };
            return informationUser;
        }

        public SideBarUserPanelViewModel GetSideBarUserPanelData(string? userName)
        {
            return _context.Users
                .Where(u => u.UserName == userName)
                .Select(u => new SideBarUserPanelViewModel
                {
                    fullName = u.FirstName + " " + u.LastName,
                    registerDate = u.CreateDate

                }).Single();
        }

        public void EditProfile(string userName, EditProfileViewModel editProfile)
        {
            if (editProfile.userAvatarImageName != null)
            {
                var imagePath = "";

                #region DeleteImagePath
                if (editProfile.userAvatarImageName != "Defult.jpg")
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/useravatar", editProfile.userAvatarImageName);
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }

                }
                #endregion
                editProfile.userAvatarImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(editProfile.userAvatarFileName.FileName);
                #region saveImagePath
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/useravatar", editProfile.userAvatarImageName);
                using var stream = new FileStream(imagePath, FileMode.Create);
                editProfile.userAvatarFileName.CopyTo(stream);
                #endregion
            }
            var user = _userService.GetUserByUserName(userName);
            user.TelPhone = editProfile.telPhone;
            user.CellPhone = editProfile.cellPhone;
            user.Address = editProfile.address;
            user.UserName = editProfile.userName;
            _userService.UpdateUser(user);
        }

        public EditProfileViewModel GetDataForEditProfileUser(string userName)
        {
            return _context.Users
                .Where(u => u.UserName == userName)
                .Select(u => new EditProfileViewModel
                {
                    telPhone = u.TelPhone,
                    cellPhone = u.CellPhone,
                    address = u.Address,
                    userName = u.UserName
                }).Single();
        }

        public void ChangeUserPassword(string userName, string newPassword)
        {
            var user = _userService.GetUserByUserName(userName);
            user.Password = PasswordHelper.EncodePasswordMd5(newPassword);
            _userService.UpdateUser(user);
        }

        public bool CompareOldPassword(string userName, string oldPassword)
        {
            string hashOldPassword = PasswordHelper.EncodePasswordMd5(oldPassword);
            return _context.Users
                .Any(u => u.UserName == userName && u.Password == hashOldPassword);
        }

        public InformationUserViewModel GetInformationUser(int userId)
        {
            var user = _userService.GetUserByUserId(userId);
            var informationUser = new InformationUserViewModel
            {
                userName = user.UserName,
                registerDate = user.CreateDate,
                fullName = user.FirstName + " " + user.LastName,
                cellPhone = user.CellPhone,
  
            };
            return informationUser;
        }

  
  
    }
}
