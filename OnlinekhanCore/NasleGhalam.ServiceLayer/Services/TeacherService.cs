using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Teacher;

namespace NasleGhalam.ServiceLayer.Services
{
    public class TeacherService
    {
        private const string Title = "دبیر";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Teacher> _teachers;
        private readonly Lazy<RoleService> _roleService;

        public TeacherService(IUnitOfWork uow,
            Lazy<RoleService> roleService)
        {
            _uow = uow;
            _teachers = uow.Set<Teacher>();
            _roleService = roleService;
        }

        /// <summary>
        /// گرفتن  دبیر با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TeacherViewModel GetById(int id)
        {
            return _teachers
                .Include(current => current.User.City)
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<TeacherViewModel>)
                .FirstOrDefault();
        }

        /// <summary>
        /// گرفتن همه دبیر ها
        /// </summary>
        /// <returns></returns>
        public IList<TeacherViewModel> GetAll()
        {
            return _teachers
                .Include(current => current.User.City)
                .Include(current => current.User.Role)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<TeacherViewModel>)
                .ToList();
        }

        /// <summary>
        /// ثبت دبیر
        /// </summary>
        /// <param name="teacherViewModel"></param>
        /// <param name="userRoleLevel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(TeacherCreateViewModel teacherViewModel, byte userRoleLevel)
        {
            // سطح نقش باید بزرگتر از سطح نقش کاربر ویرایش کننده باشد
            var role = _roleService.Value.GetById(teacherViewModel.User.RoleId, userRoleLevel);
            if (role.Level <= userRoleLevel)
            {
                return new ClientMessageResult()
                {
                    Message = $"سطح نقش باید بزرگتر از ({userRoleLevel}) باشد",
                    MessageType = MessageType.Error
                };
            }

            var teacher = Mapper.Map<Teacher>(teacherViewModel);
            teacher.User.LastLogin = DateTime.Now;
            _teachers.Add(teacher);

            var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(teacher.Id);

            return clientResult;
        }

        /// <summary>
        /// ویرایش دبیر
        /// </summary>
        /// <param name="teacherViewModel"></param>
        /// <param name="userRoleLevel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(TeacherUpdateViewModel teacherViewModel, byte userRoleLevel)
        {
            // سطح نقش باید بزرگتر از سطح نقش کاربر ویرایش کننده باشد
            var role = _roleService.Value.GetById(teacherViewModel.User.RoleId, userRoleLevel);
            if (role == null)
            {
                return new ClientMessageResult()
                {
                    Message = "نقش یافت نگردید",
                    MessageType = MessageType.Error
                };
            }

            if (role.Level <= userRoleLevel)
            {
                return new ClientMessageResult()
                {
                    Message = $"سطح نقش باید بزرگتر از ({userRoleLevel}) باشد",
                    MessageType = MessageType.Error
                };
            }

            var teacher = Mapper.Map<Teacher>(teacherViewModel);
            _uow.MarkAsChanged(teacher.User);
            _uow.MarkAsChanged(teacher);

            if (string.IsNullOrEmpty(teacher.User.Password))
            {
                _uow.ExcludeFieldsFromUpdate(teacher.User, x => x.Password, x => x.LastLogin);
                _uow.ValidateOnSaveEnabled(false);
            }
            else
            {
                _uow.ExcludeFieldsFromUpdate(teacher.User, x => x.LastLogin);
            }

            var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
            {
                clientResult.Obj = GetById(teacher.Id);
            }
            else if (serverResult.ErrorNumber == 2601 && serverResult.EnMessage.Contains("UK_User_NationalNo"))
            {
                clientResult.Message = "کد ملی تکراری می باشد";
            }
            else if (serverResult.ErrorNumber == 2601 && serverResult.EnMessage.Contains("UK_User_Username"))
            {
                clientResult.Message = "نام کاربری تکراری می باشد";
            }

            return clientResult;
        }

        /// <summary>
        /// حذف دبیر
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
            var teacherViewModel = GetById(id);
            if (teacherViewModel == null)
            {
                return ClientMessageResult.NotFound();
            }

            var teacher = Mapper.Map<Teacher>(teacherViewModel);
            var user = teacher.User;
            _uow.MarkAsDeleted(teacher);
            _uow.MarkAsDeleted(user);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(msgRes);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = id;
            return clientResult;
        }
    }
}
