using dot48.Application.Models.ViewModels;
using System.Collections.Generic;

namespace dot48.Application.Interfaces
{
    public interface IUserService
    {
        List<UserViewModel> GetUsers();
        UserViewModel GetUserByNameAndPassword(string nameUser, string password);
        UserViewModel GetUserByCodeOperator(string codeOperator);
        List<ProfileViewModel> GetProfiles();
        void SaveUser(UserViewModel userAdd);
        UserViewModel GetUserByIdUser(int IdUser);
    }
}
