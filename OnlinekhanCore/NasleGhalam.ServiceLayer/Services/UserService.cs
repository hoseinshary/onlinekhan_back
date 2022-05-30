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
                        loginResult.SubMenus = _actionService.Value.GetSubMenu(user.Role.SumOfActionBit);
                        if (loginResult.SubMenus.Count == 0)
                        {
                            loginResult.Message = "شما به صفحه ای دسترسی ندارید";
                        }
                        else
                        {
                            var defaultPage = "";

                            if (user.Role.Level == 3)
                            {
                                defaultPage = "/panel/expertpanel";
                            }
                            else if (user.Role.Level < 3)
                            {
                                defaultPage = "/panel/adminpanel";
                            }
                            else if (user.Role.Id == 2015)
                            {
                                defaultPage = "/panel/teacherPanel";
                            }
                            else if (user.Role.Level == 100)
                            {
                                defaultPage = "/panel/studentPanel";
                            }
                            else
                            {

                                foreach (var item in loginResult.SubMenus)
                                {
                                    if (item.EnName == "/User")
                                    {
                                        defaultPage = item.EnName;
                                        break;
                                    }
                                }

                                if (string.IsNullOrEmpty(defaultPage))
                                {
                                    defaultPage = loginResult.SubMenus[0].EnName;
                                }
                            }

                            loginResult.Message = "ورود موفقیت آمیز";
                            loginResult.MessageType = MessageType.Success;

                            loginResult.Menus = _actionService.Value.GetMenu(user.Role.SumOfActionBit);
                            loginResult.DefaultPage = defaultPage;

                            loginResult.FullName = user.Name + " " + user.Family;
                            loginResult.ProfilePic = $"/Api/User/GetPictureFile/{user.ProfilePic}".ToFullRelativePath();

                            loginResult.Token = JsonWebToken.CreateToken(user.Role.Level,
                                user.IsAdmin, user.Id, user.Role.SumOfActionBit, user.Role.UserType);
                        }
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
