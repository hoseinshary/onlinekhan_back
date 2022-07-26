using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Student;
using NasleGhalam.WebApi.Extensions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: علیرضا اعتمادی
	///     date: 1397.07.20
	/// </author>
	public class StudentController : ApiController
    {
        private readonly StudentService _studentService;
        private readonly LogService _logService;
        public StudentController(StudentService studentService, LogService logService)
        {
            _studentService = studentService;
            _logService = logService;
        }

        [HttpGet, CheckUserAccess(ActionBits.StudentReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_studentService.GetAll());
        }


        [HttpPost, CheckUserAccess(ActionBits.StudentReadAccess)]
        public IHttpActionResult GetQuestionAssayReportByLessonId(GetQuestionAssayReportViewModel assayReportViewModel)
        {
            return Ok(_studentService.GetQuestionAssayReportByLessonId(assayReportViewModel.LessonId,assayReportViewModel.StudentId));
        }


        //[HttpPost, CheckUserAccess(ActionBits.StudentReadAccess)]
        //public IHttpActionResult GetQuestionAssayReportByLessonIds(GetQuestionAssayReportForTopicViewModel assayReportForTopicViewModel)
        //{
        //    return Ok(_studentService.GetQuestionAssayReportByLessonIds(assayReportForTopicViewModel.LessonIds, assayReportForTopicViewModel.StudentId));
        //}


        [HttpGet, CheckUserAccess(ActionBits.StudentReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var student = _studentService.GetById(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }
        [HttpGet, CheckUserAccess(ActionBits.StudentReadAccess)]
        public IHttpActionResult GetMyInfo()
        {
            var student = _studentService.GetById(Request.GetUserId());
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }
        [HttpPost]
        [CheckModelValidation]
        public IHttpActionResult CreateTemp([FromUri]int userId)
        {
            var msgRes = _studentService.CreateTemp(userId);
           
            return Ok(msgRes);
        }
        [HttpPost]
        [CheckUserAccess()]
        [CheckModelValidation]
        public IHttpActionResult UpdateStudentMajorListData(StudentMajorListDataViewModel studentViewModel)
        {
            var msgRes = _studentService.Update(studentViewModel, Request.GetUserId());
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Update, "Student", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }
        [HttpPost]
        [CheckUserAccess(ActionBits.StudentUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(StudentUpdateViewModel studentViewModel)
        {
            var msgRes = _studentService.Update(studentViewModel, Request.GetRoleLevel());
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Update, "Student", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost, CheckUserAccess(ActionBits.StudentDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _studentService.Delete(id);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Delete, "Student", id, Request.GetUserId());
            }
            return Ok(msgRes);
        }
    }
}
