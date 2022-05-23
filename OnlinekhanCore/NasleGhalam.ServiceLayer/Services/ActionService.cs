using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.ViewModels.Action;
using Action = NasleGhalam.DomainClasses.Entities.Action;

namespace NasleGhalam.ServiceLayer.Services
{
    public class ActionService
    {
        private readonly IDbSet<Action> _actions;
        private readonly Lazy<RoleService> _roleService;

        public ActionService(IUnitOfWork uow, Lazy<RoleService> roleService)
        {
            _actions = uow.Set<Action>();
            _roleService = roleService;
        }

        /// <summary>
        /// گرفتن  اکشن بیت
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionViewModel GetActionById(int id)
        {
            return _actions
                .Where(current => id == current.Id)
                .Select(current => new ActionViewModel()
                {
                    Id = current.Id,
                    ActionFaName = current.FaName,
                    ActionBit = current.ActionBit
                }).FirstOrDefault();
        }

        /// <summary>
        /// گرفتن اکشن های یک کنترلر
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userRoleLevel"></param>
        /// <returns></returns>
        public IList<ActionViewModel> GetAllActions(int roleId, byte userRoleLevel)
        {
            var role = _roleService.Value.GetById(roleId, userRoleLevel);
            if (role == null)
                return new List<ActionViewModel>();

            return _actions
                .Include(current => current.Controller)
                .OrderBy(current => current.Controller.Priority)
                .ThenBy(current => current.Priority)
                .AsEnumerable()
                .Select(current => new ActionViewModel
                {
                    Id = current.Id,
                    ActionFaName = current.FaName,
                    ControllerFaName = current.Controller.FaName,
                    IsChecked = Utility.HasAccess(role.SumOfActionBit, current.ActionBit),
                    ModuleId = current.Controller.ModuleId,
                    ControllerId = current.ControllerId
                }).ToList();
        }

        /// <summary>
        /// گرفتن  منو با اعمال دسترسی
        /// </summary>
        /// <returns></returns>
        public IList<MenuViewModel> GetMenu(string userAccess)
        {
            return GetAllModuleQuery(userAccess).ToList();
        }

        /// <summary>
        /// گرفتن زیر منو
        /// </summary>
        /// <param name="userAccess"></param>
        /// <returns></returns>
        public IList<SubMenuViewModel> GetSubMenu(string userAccess)
        {
            return _actions
                .Include(current => current.Controller.Actions)
                .Where(current => current.IsIndex)
                .OrderBy(current => current.Controller.Priority)
                .AsEnumerable()
                .Where(current => Utility.HasAccess(userAccess, current.ActionBit))
                .Select(current => new SubMenuViewModel()
                {
                    ModuleId = current.Controller.ModuleId,
                    ControllerId = current.ControllerId,
                    FaName = current.Controller.FaName,
                    EnName = current.Controller.EnName,
                    Icon = current.Controller.Icone,
                    UserAccess = current.Controller.Actions
                        .Where(x => !x.IsIndex)
                        .Where(x => Utility.HasAccess(userAccess, x.ActionBit))
                        .Select(x => x.FaName).ToArray()
                }).ToList();

            //return _modules
            //    .Include(current => current.Controllers)
            //    .OrderBy(current => current.Priority)
            //    .Select(current => new
            //    {
            //        current.Name,
            //        controllers = current.Controllers
            //            .Where(ctrl => ctrl.Actions.Any(act => act.IsIndex))
            //            .OrderBy(ctrl => ctrl.Priority)
            //            .Select(ctrl => new
            //            {
            //                ctrl.FaName,
            //                ctrl.EnName,
            //                Icon = ctrl.Icone,
            //                Actions = ctrl.Actions
            //                    .Where(act => !act.IsIndex)
            //                    .Select(x => new
            //                    {
            //                        x.ActionBit,
            //                        x.FaName
            //                    })
            //            })
            //    })
            //    .AsEnumerable()
            //    .Select(current => new MenuViewModel
            //    {
            //        Name = current.Name,
            //        SubMenus = current.controllers
            //            .Where(ctrl => ctrl.Actions.Any(act => Utility.HasAccess(userAccess, act.ActionBit)))
            //            .Select(ctrl => new SubMenuViewModel
            //            {
            //                FaName = ctrl.FaName,
            //                EnName = ctrl.EnName,
            //                Icon = ctrl.Icon,
            //                UserAccess = ctrl.Actions
            //                    .Where(x => Utility.HasAccess(userAccess, x.ActionBit))
            //                    .Select(x => x.FaName).ToArray()
            //            }).ToArray()
            //    })
            //    .ToList();
        }

        /// <summary>
        /// گرفتن کوئری ماژول برای لیست کشویی با اعمال دسترسی
        /// </summary>
        /// <param name="userAccess"></param>
        /// <returns></returns>
        public IEnumerable<MenuViewModel> GetAllModuleQuery(string userAccess)
        {
            // get all modules that user has access(has repetive moduleId)
            var accessModuleIds = _actions
                .Where(current => current.IsIndex)
                .OrderBy(current => current.Controller.Module.Priority)
                .GroupBy(current => new
                {
                    current.Controller.ModuleId,
                    current.ActionBit
                })
                .Select(current => new
                {
                    current.Key.ModuleId,
                    current.Key.ActionBit
                })
                .AsEnumerable()
                .Where(current => Utility.HasAccess(userAccess, current.ActionBit))
                .Select(current => current.ModuleId).ToList();

            // get all modules
            var allModules = _actions
                .Where(current => current.IsIndex)
                .OrderBy(current => current.Controller.Module.Priority)
                .GroupBy(current => new
                {
                    current.Controller.ModuleId,
                    ModuleName = current.Controller.Module.Name,
                })
                .Select(current => new MenuViewModel
                {
                    ModuleId = current.Key.ModuleId,
                    ModuleName = current.Key.ModuleName,
                }).ToList();


            return allModules
                .Where(current => accessModuleIds.Contains(current.ModuleId));
        }
    }
}
