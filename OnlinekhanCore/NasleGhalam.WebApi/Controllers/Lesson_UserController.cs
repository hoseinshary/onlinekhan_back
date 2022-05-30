using System.Collections.Generic;
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
    ///     date: 1398/02/28
    /// </author>
    public class Lesson_UserController : ApiController
    {
        private readonly Lesson_UserService _lesson_UserService;
        private readonly LogService _logService;
        public Lesson_UserController(Lesson_UserService lesson_UserService, LogService logService)
        {
            _lesson_UserService = lesson_UserService;
            _logService = logService;
        }


        [HttpGet, CheckUserAccess(ActionBits.MyLessonAccess)]
        public IHttpActionResult GetAllMyLesson()
        {
            return Ok(_lesson_UserService.GetAllMyLesson(Request.GetUserId()));
        }

        [HttpGet, CheckUserAccess(ActionBits.Lesson_UserReadAccess)]
        public IHttpActionResult GetAllByLessonIds([FromUri]IEnumerable<int> ids)
        {
            return Ok(_lesson_UserService.GetAllByLessonIds(ids));
        }

        [HttpGet, CheckUserAccess(ActionBits.Lesson_UserReadAccess)]
        public IHttpActionResult GetAllByUserIds([FromUri]IEnumerable<int> ids)
        {
            return Ok(_lesson_UserService.GetAllByUserIds(ids));
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.Lesson_UserCreateDeleteAccess)]
        [CheckModelValidation]
        public IHttpActionResult SubmitChanges(Lesson_UserViewModel lesson_UserViewModel)
        {
            var msgRes = _lesson_UserService.SubmitChanges(lesson_UserViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Create, "Lesson_User", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.LessonBuyAccess)]
        [CheckModelValidation]
        public IHttpActionResult BuyLesson(BuyLessonViewModel lesson_UserViewModel)
        {
            lesson_UserViewModel.UserId = Request.GetUserId();
            var msgRes = _lesson_UserService.BuyLesson(lesson_UserViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Create, "buyLesson", msgRes.Obj, Request.GetUserId());
            }

            return Ok(msgRes);
         
        }
    }
}
