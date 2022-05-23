using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.LessonDepartment;
using NasleGhalam.WebApi.Extensions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: hosein shary  
	///     date: 1398/03/02
	/// </author>
	public class LessonDepartmentController : ApiController
    {
        private readonly LessonDepartmentService _lessonDepartmentService;
        private readonly LogService _logService;
        public LessonDepartmentController(LessonDepartmentService lessonDepartmentService, LogService logService)
        {
            _lessonDepartmentService = lessonDepartmentService;
            _logService = logService;
        }

        [HttpGet, CheckUserAccess(ActionBits.LessonDepartmentReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_lessonDepartmentService.GetAll());
        }

        [HttpGet, CheckUserAccess(ActionBits.LessonDepartmentReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var lessonDepartment = _lessonDepartmentService.GetById(id);
            if (lessonDepartment == null)
            {
                return NotFound();
            }
            return Ok(lessonDepartment);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.LessonDepartmentCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(LessonDepartmentViewModel lessonDepartmentViewModel)
        {
            var msgRes = _lessonDepartmentService.Create(lessonDepartmentViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Create, "LessonDepartment", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.LessonDepartmentCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Assign(LessonDepartmentAssignViewModel lessonDepartmentViewModel)
        {
            var msgRes = _lessonDepartmentService.Assign(lessonDepartmentViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Create, "LessonDepartment-Assign", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.LessonDepartmentUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(LessonDepartmentViewModel lessonDepartmentViewModel)
        {
            var msgRes = _lessonDepartmentService.Update(lessonDepartmentViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Update, "LessonDepartment", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost, CheckUserAccess(ActionBits.LessonDepartmentDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _lessonDepartmentService.Delete(id);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Delete, "LessonDepartment", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }
    }
}
