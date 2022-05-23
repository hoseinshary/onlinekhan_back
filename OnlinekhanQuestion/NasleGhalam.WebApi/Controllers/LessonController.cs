using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Lesson;
using NasleGhalam.WebApi.Extensions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
    /// <author>
    ///     name: حسین شری
    ///     date: 9/8/97
    /// </author>
    public class LessonController : ApiController
    {
        private readonly LessonService _lessonService;
        private readonly LogService _logService;
        public LessonController(LessonService lessonService, LogService logService)
        {
            _lessonService = lessonService;
            _logService = logService;
        }

        [HttpGet, CheckUserAccess(ActionBits.LessonReadAccess, ActionBits.TopicReadAccess)]
        public IHttpActionResult GetAllByLessonDepartmentId([FromUri] int id)
        {
            return Ok(_lessonService.GetAllByDepartmentId(id));
        }


        //[HttpGet, CheckUserAccess(ActionBits.LessonReadAccess, ActionBits.TopicReadAccess)]
        //public IHttpActionResult GetAllByEducationTreeIds([FromUri] IEnumerable<int> ids)
        //{
        //    return Ok(_lessonService.GetAllByEducationTreeIds(ids));
        //}
        [HttpGet]
        //[CheckUserAccess(ActionBits.QuestionReadAccess)]
        public HttpResponseMessage GetPictureFile(string id)
        {
            var stream = new MemoryStream();
            id += ".jpeg";
            var filestraem = File.OpenRead(SitePath.GetLessonAbsPath(id));
            filestraem.CopyTo(stream);

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(stream.ToArray())
            };
            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = id
                };
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");
            filestraem.Dispose();
            stream.Dispose();
            return result;
        }


        [HttpGet, CheckUserAccess(ActionBits.Lesson_UserReadAccess, ActionBits.LessonReadAccess, ActionBits.TopicReadAccess,
             ActionBits.AssayReadAccess, ActionBits.EducationBookReadAccess, ActionBits.QuestionReadAccess, ActionBits.QuestionGroupReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_lessonService.GetAll());
        }

        [HttpGet, CheckUserAccess(ActionBits.LessonReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var lesson = _lessonService.GetById(id);
            if (lesson == null)
                return NotFound();

            return Ok(lesson);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.LessonCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(LessonCreateViewModel lessonViewModel)
        {
            var msgRes = _lessonService.Create(lessonViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Create, "Lesson", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.LessonUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(LessonUpdateViewModel lessonViewModel)
        {
            var msgRes = _lessonService.Update(lessonViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Update, "Lesson", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost, CheckUserAccess(ActionBits.LessonDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _lessonService.Delete(id);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Delete, "Lesson", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }
    }
}
