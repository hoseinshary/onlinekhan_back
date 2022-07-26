using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ServiceLayer.Configs;
using NasleGhalam.ServiceLayer.Jwt;
using NasleGhalam.ViewModels.Assay;
using NasleGhalam.ViewModels.User;

namespace NasleGhalam.ServiceLayer.Services
{
    public class UserService
    {
        private const string Title = "کاربر";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<User> _users;
        private readonly IDbSet<Student> _students;
        private readonly IDbSet<Teacher> _teachers;

        private readonly Lazy<RoleService> _roleService;
        private readonly Lazy<ActionService> _actionService;

        public UserService(IUnitOfWork uow,
            Lazy<RoleService> roleService,
            Lazy<ActionService> actionService)
        {
            _uow = uow;
            _users = _uow.Set<User>();
            _students = _uow.Set<Student>();
            _teachers = _uow.Set<Teacher>();
            _roleService = roleService;
            _actionService = actionService;
        }

        public IEnumerable<string> GetUserAssay(int userId)
        {
            var usr = _users.Include(x => x.Assays).Where(x => x.Id == userId).FirstOrDefault();
            return usr.Assays.Select(x=> x.Title);
        }

        /// <summary>
        /// گرفتن توکن از طرف SSO
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="EncryptedString"></param>
        /// <returns></returns>
        public LoginResultViewModel GetUserToken(UserTokenViewModel token)
        {
            var loginResult = new LoginResultViewModel
            {
                MessageType = MessageType.Error
            };

            var user = _users
                .Include(current => current.Role)
                .First(x => x.Id == token.UserId);

            if (user != null)
            {
                var tempEncrypt = Encryption.Encrypt(user.Username + user.Password);
                if (tempEncrypt == token.EncryptedString)
                {
                    if (!user.IsActive)
                    {
                        loginResult.Message = "نام کاربری شما فعال نمی باشد.";
                    }
                    else
                    {
                        

                            loginResult.Message = "ورود موفقیت آمیز";
                            loginResult.MessageType = MessageType.Success;

                            loginResult.DefaultPage = "/dashboard";

                          
                            loginResult.FullName = user.Name + " " + user.Family;
                            loginResult.ProfilePic = $"http://159.69.82.251:63840/Api/User/GetPictureFile/{user.ProfilePic}";

                            loginResult.Token = JsonWebToken.CreateToken(user.Role.Level,
                                user.IsAdmin, user.Id, user.Role.SumOfActionBit, user.Role.UserType);
                    }
                }
                else
                {
                    loginResult.Message = "عدم دسترسی.";
                }
            }
            else
            {
                loginResult.Message = "عدم دسترسی.";
            }

            return loginResult;



        }



    }
}
