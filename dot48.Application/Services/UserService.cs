using dot48.Application.Extensions;
using dot48.Application.Interfaces;
using dot48.Application.Models.ViewModels;
using dot48.Domain.Entities;
using dot48.Domain.Interfaces.Repository;
using dot48.Infra.Persistency.Contexts;
using dot48.Infra.Persistency.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace dot48.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IProfileRepository profileRepository;
        private readonly IUnitOfWork unitOfWork;
        public UserService(IUserRepository _userRepository,
            IProfileRepository _profileRepository,
            IUnitOfWork _unitOfWork)
        {
            userRepository = _userRepository;
            profileRepository = _profileRepository;
            this.unitOfWork = _unitOfWork;
        }

        public List<UserViewModel> GetUsers()
        {

            List<UserViewModel> userViewModel = new List<UserViewModel>();
            var users = userRepository.GetAll("Profiles", "UserSettings");
            foreach (var item in users)
            {
                userViewModel.Add(ConverterUserToViewModel(item));
            }

            return userViewModel;

        }

        public UserViewModel GetUserByNameAndPassword(string nameUser, string password)
        {
            password = password.EncryptorSha256();
            var userResult = userRepository.GetByPredicate(u => u.Email.ToUpper().Equals(nameUser.ToUpper())
                                   && u.Password.Equals(password)
                                   && u.Enable, "Profiles");

            return userResult != null ? ConverterUserToViewModel(userResult) : null;
        }

        public UserViewModel GetUserByCodeOperator(string codeOperator)
        {
            var userResult = userRepository.GetByPredicate(u => u.CodeUser.ToUpper().Equals(codeOperator.ToUpper())
                                   && u.Enable, "Profiles");

            return userResult != null ? ConverterUserToViewModel(userResult) : null;
        }

        private static UserViewModel ConverterUserToViewModel(User userResult)
        {
            UserViewModel userViewModel = new UserViewModel
            {
                IdUser = userResult.Id,
                Name = userResult.Name,
                AliasName = userResult.AliasName,
                Nif = userResult.Nif,
                Email = userResult.Email,
                CodeUser = userResult.CodeUser,
                Enable = userResult.Enable,
                PhoneNumber = userResult.PhoneNumber
            };

            if (userResult.UserSettings != null)
            {
                userViewModel.IdUserSetting = userResult.UserSettings.Id;
                userViewModel.MO = userResult.UserSettings.MO;
                userViewModel.RecipientMO = userResult.UserSettings.RecipientMO;
                userViewModel.DIR = userResult.UserSettings.DIR;
                userViewModel.RecipientDIR = userResult.UserSettings.RecipientDIR;
                userViewModel.WorkHours = userResult.UserSettings.WorkHours;
            }

            if (userResult.Profiles != null && userResult.Profiles.Count > 0)
            {
                foreach (var item in userResult.Profiles)
                {
                    userViewModel.Profiles.Add(
                        new ProfileViewModel
                        {
                            IdProfile = item.Id,
                            CodeProfile = item.CodeProfile
                        });
                }

                userViewModel.IdProfile = userResult.Profiles.FirstOrDefault().Id;
            }

            return userViewModel;
        }

        public List<ProfileViewModel> GetProfiles()
        {
            return profileRepository.GetAll()
             .Select(a => new ProfileViewModel
             {
                 CodeProfile = a.CodeProfile,
                 IdProfile = a.Id
             }).ToList();
        }

        public void SaveUser(UserViewModel userAdd)
        {
            User userEntity = userAdd.IdUser > 0 ? UpdateUser(userAdd) : ConverterUserToEntity(userAdd);
            UpdateProfile(userAdd, userEntity);

            var user = userRepository.SaveOrUpdate(userEntity);
            this.unitOfWork.Commit();
        }

        private User UpdateUser(UserViewModel userAdd)
        {
            User userEntity = userRepository.GetByPredicate(a => a.Id.Equals(userAdd.IdUser), "UserSettings", "Profiles");

            userEntity.Name = userAdd.Name;
            userEntity.AliasName = userAdd.AliasName;
            userEntity.Nif = userAdd.Nif;
            userEntity.Email = userAdd.Email;
            userEntity.CodeUser = userAdd.CodeUser;
            userEntity.Enable = userAdd.Enable;
            userEntity.PhoneNumber = userAdd.PhoneNumber;
            userEntity.Password = string.IsNullOrEmpty(userAdd.Password)? userEntity.Password : userAdd.Password.EncryptorSha256();
            userEntity.CreateDate = DateTime.Now;
            userEntity.UpdateDate = DateTime.Now;
            userEntity.UserSettings.MO = userAdd.MO;
            userEntity.UserSettings.RecipientMO = userAdd.RecipientMO;
            userEntity.UserSettings.DIR = userAdd.DIR;
            userEntity.UserSettings.RecipientDIR = userAdd.RecipientDIR;
            userEntity.UserSettings.WorkHours = userAdd.WorkHours;

            return userEntity;
        }

        private void UpdateProfile(UserViewModel userAdd, User userEntity)
        {
            List<Profile> profilesRemove = profileRepository.GetAll(a => a.Id != userAdd.IdProfile).ToList();

            foreach (var profilesOld in profilesRemove)
            {
                userEntity.Profiles.Remove(profilesOld);
            }

            if (userAdd.IdProfile > 0)
            {
                var perfilNovo = profileRepository.GetByKey(userAdd.IdProfile);
                userEntity.Profiles.Add(perfilNovo);
            }
        }

        private static User ConverterUserToEntity(UserViewModel userAdd)
        {
            User userEntity = new User
            {
                Name = userAdd.Name,
                AliasName = userAdd.AliasName,
                Nif = userAdd.Nif,
                Email = userAdd.Email,
                CodeUser = userAdd.CodeUser,
                Enable = userAdd.Enable,
                PhoneNumber = userAdd.PhoneNumber,
                Password = userAdd.Password.EncryptorSha256(),
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                UserSettings = new UserSetting()
                {
                    MO = userAdd.MO,
                    RecipientMO = userAdd.RecipientMO,
                    DIR = userAdd.DIR,
                    RecipientDIR = userAdd.RecipientDIR,
                    WorkHours = userAdd.WorkHours
                }
            };

            return userEntity;
        }

        public UserViewModel GetUserByIdUser(int IdUser)
        {
            return ConverterUserToViewModel(userRepository.GetByPredicate(a => a.Id.Equals(IdUser), "UserSettings"));
        }
    }
}
