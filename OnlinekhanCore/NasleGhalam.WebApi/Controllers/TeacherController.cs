using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Teacher;
using NasleGhalam.WebApi.Extensions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: hosein shary
	///     date: 2020.02.2
	/// </author>
	public class TeacherController : ApiController
    {
        private readonly TeacherService _teacherService;
        private readonly LogService _logService;
        public TeacherController(TeacherService teacherService, LogService logService)
        {
            _teacherService = teacherService;
            _logService = logService;
        }

        [HttpGet, CheckUserAccess(ActionBits.TeacherReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_teacherService.GetAll());
        }

        [HttpGet, CheckUserAccess(ActionBits.TeacherReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var teacher = _teacherService.GetById(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return Ok(teacher);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.TeacherCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(TeacherCreateViewModel teacherViewModel)
        {
            var msgRes = _teacherService.Create(teacherViewModel, Request.GetRoleLevel());
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Create, "Teacher", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.TeacherUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(TeacherUpdateViewModel teacherViewModel)
        {
            var msgRes = _teacherService.Update(teacherViewModel, Request.GetRoleLevel());
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Update, "Teacher", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost, CheckUserAccess(ActionBits.TeacherDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _teacherService.Delete(id);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Delete, "Teacher", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }
    }
}
