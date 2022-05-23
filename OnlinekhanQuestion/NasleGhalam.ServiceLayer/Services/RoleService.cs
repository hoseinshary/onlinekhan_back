using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Role;

namespace NasleGhalam.ServiceLayer.Services
{
    public class RoleService
    {
        private const string Title = "نقش";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Role> _roles;
        private readonly Lazy<ActionService> _actionService;

        public RoleService(IUnitOfWork uow, Lazy<ActionService> actionService)
        {
            _uow = uow;
            _roles = uow.Set<Role>();
            _actionService = actionService;
        }

        /// <summary>
        /// گرفتن  نقش با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userRoleLevel"></param>
        /// <returns></returns>
        public RoleViewModel GetById(int id, byte userRoleLevel)
        {
            return _roles
                .Where(current => current.Id == id)
                .Where(current => current.Level > userRoleLevel)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<RoleViewModel>)
                .FirstOrDefault();
        }

        /// <summary>
        /// گرفتن  نقش با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userRoleLevel"></param>
        /// <returns></returns>
        public RoleViewModel GetByIdPrivate(int id, byte userRoleLevel)
        {
            return _roles
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<RoleViewModel>)
                .FirstOrDefault();
        }

        /// <summary>
        /// گرفتن همه نقش ها
        /// </summary>
        /// <param name="userRoleLevel"></param>
        /// <returns></returns>
        public IList<RoleViewModel> GetAll(byte userRoleLevel)
        {
            return _roles
                .Where(current => current.Level > userRoleLevel)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<RoleViewModel>)
                .ToList();
        }

        /// <summary>
        /// ثبت نقش
        /// </summary>
        /// <param name="roleViewModel"></param>
        /// <param name="userRoleLevel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(RoleCreateViewModel roleViewModel, byte userRoleLevel)
        {
            // سطح نقش باید بزرگتر از سطح نقش کاربر ثبت کننده باشد
            if (roleViewModel.Level <= userRoleLevel)
            {
                return new ClientMessageResult()
                {
                    Message = $"سطح نقش باید بزرگتر از ({userRoleLevel}) باشد",
                    MessageType = MessageType.Error
                };
            }
            var role = Mapper.Map<Role>(roleViewModel);
            role.SumOfActionBit = "0";
            _roles.Add(role);

            var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(role.Id, userRoleLevel);

            return clientResult;
        }

        /// <summary>
        /// ویرایش نقش
        /// </summary>
        /// <param name="roleViewModel"></param>
        /// <param name="userRoleLevel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(RoleUpdateViewModel roleViewModel, byte userRoleLevel)
        {
            // سطح نقش باید بزرگتر از سطح نقش کاربر ویرایش کننده باشد
            if (roleViewModel.Level <= userRoleLevel)
            {
                return new ClientMessageResult()
                {
                    Message = $"سطح نقش باید بزرگتر از ({userRoleLevel}) باشد",
                    MessageType = MessageType.Error
                };
            }
            
            var role = Mapper.Map<Role>(roleViewModel);
            _uow.ExcludeFieldsFromUpdate(role, x=>x.SumOfActionBit);
            _uow.ValidateOnSaveEnabled(false);

            var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(role.Id, userRoleLevel);

            return clientResult;
        }

        /// <summary>
        /// حذف نقش
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userRoleLevel"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id, byte userRoleLevel)
        {
            var roleViewModel = GetById(id, userRoleLevel);
            if (roleViewModel == null)
                return ClientMessageResult.NotFound();

            var role = Mapper.Map<Role>(roleViewModel);
            _uow.MarkAsDeleted(role);
            var msgRes = _uow.CommitChanges(CrudType.Create, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(msgRes);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = id;
            return clientResult;
        }

        /// <summary>
        /// ویرایش دسترسی
        /// </summary>
        /// <param name="roleAccess"></param>
        /// <param name="userAccess"></param>
        /// <param name="userRoleLevel"></param>
        /// <returns></returns>
        public ClientMessageResult ChangeAccess(RoleAccessViewModel roleAccess, string userAccess, byte userRoleLevel)
        {
            var roleViewModel = GetById(roleAccess.RoleId, userRoleLevel);
            var action = _actionService.Value.GetActionById(roleAccess.ActionId);

            if (roleViewModel == null || action == null)
                return ClientMessageResult.NotFound();


            // اگر در کلاینت چک خورده باشد ولی در دیتابیس چک نخورده باشد
            // باید به دسترسی آن اضاف کنیم
            if (roleAccess.IsChecked && Utility.HasAccess(userAccess, action.ActionBit))
            {
                roleViewModel.SumOfActionBit = Utility.AddAccess(roleViewModel.SumOfActionBit, action.ActionBit);
            }
            // اگر در کلاینت چک نخورده باشد ولی در دیتابیس چک خورده باشد
            // باید از دسترسی آن کم کنیم
            else if (Utility.HasAccess(userAccess, action.ActionBit))
            {
                roleViewModel.SumOfActionBit = Utility.RemoveAccess(roleViewModel.SumOfActionBit, action.ActionBit);
            }

            var role = Mapper.Map<Role>(roleViewModel);
            _uow.MarkAsChanged(role);
            var msgRes = _uow.CommitChanges(CrudType.Update, Title);
            return Mapper.Map<ClientMessageResult>(msgRes);
        }


        
    }
}
