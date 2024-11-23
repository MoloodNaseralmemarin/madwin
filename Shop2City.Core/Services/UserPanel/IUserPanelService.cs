using System.Collections.Generic;
using Shop2City.Core.DTOs.Account;
using Shop2City.DataLayer.Entities.Products;

namespace Shop2City.Core.Services.UserPanel
{
    public interface IUserPanelService
    {
        InformationUserViewModel GetInformationUser(string userName);

        Task<InformationUserViewModel> GetInformationUser(int userId);

        Task<SideBarUserPanelViewModel> GetSideBarUserPanelDataAsync(int id);

        EditProfileViewModel GetDataForEditProfileUser(string userName);

        void EditProfile(string userName, EditProfileViewModel editProfile);

        bool CompareOldPassword(string userName, string oldPassword);

        void ChangeUserPassword(string userName, string newPassword);

        
    }
}
