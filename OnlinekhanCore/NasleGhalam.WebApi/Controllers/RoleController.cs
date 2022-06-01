using System.Net;
using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.ViewModels.Role;
using NasleGhalam.WebApi.Extensions;
using NasleGhalam.WebApi.FilterAttribute;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
    /// <author>
    ///     name: علیرضا اعتمادی
    ///     date: 1397.03.28
    /// </author>
    public class RoleController : ApiController
    {
        private readonly RoleService _roleService;
        private readonly LogService _logService;
        public RoleController(RoleService roleService, LogService logService)
        {
            _roleService = roleService;
            _logService = logService;
        }

        [HttpGet, CheckUserAccess(ActionBits.RoleReadAccess)]
        public IHttpActionResult GetAll()
        {
            

            return Ok(_roleService.GetAll(Request.GetRoleLevel()));
        }
        [HttpGet]
        public IHttpActionResult GetByIdForSSO(int id)
        {
            var role = _roleService.GetByIdForSSO(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpGet, CheckUserAccess(ActionBits.RoleReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var role = _roleService.GetById(id, Request.GetRoleLevel());
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.RoleCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(RoleCreateViewModel roleViewModel)
        {
            var msgRes = _roleService.Create(roleViewModel, Request.GetRoleLevel());
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Create, "Role", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.RoleUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(RoleUpdateViewModel roleViewModel)
        {
            var msgRes = _roleService.Update(roleViewModel, Request.GetRoleLevel());
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Update, "Role", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost, CheckUserAccess(ActionBits.RoleDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _roleService.Delete(id, Request.GetRoleLevel());
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Delete, "Role", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }
    }
}
