using System.Web;
using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Question;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using NasleGhalam.WebApi.Extensions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
    /// <author>
    ///     name: حسین شری
    ///     date: 22/8/97
    /// </author>
    public class QuestionController : ApiController
    {
        private readonly QuestionService _questionService;
        private readonly LogService _logService;
        public QuestionController(QuestionService questionService, LogService logService)
        {
            _questionService = questionService;
            _logService = logService;
        }

        [HttpGet, CheckUserAccess(ActionBits.QuestionReadAccess)]
        public IHttpActionResult GetAllByTopicIds([FromUri] IEnumerable<int> ids)
        {
            return Ok(_questionService.GetAllByTopicIds(ids));
        }

        [HttpGet, CheckUserAccess(ActionBits.QuestionReadAccess)]
        public IHttpActionResult GetAllByTopicIdsNoJudge([FromUri] IEnumerable<int> ids)
        {
            return Ok(_questionService.GetAllByTopicIdsNoJudge(ids, Request.GetUserId(), Request.GetRoleLevel()));
        }

        [HttpGet, CheckUserAccess(ActionBits.QuestionReadAccess)]
        public IHttpActionResult GetAllByTopicIdsNoAnswer([FromUri] IEnumerable<int> ids)
        {
            return Ok(_questionService.GetAllByTopicIdsNoAnswer(ids));
        }
        [HttpGet, CheckUserAccess(ActionBits.QuestionReadAccess)]
        public IHttpActionResult GetAllByTopicIdsNoAnswerJudge([FromUri] IEnumerable<int> ids)
        {
            return Ok(_questionService.GetAllByTopicIdsNoAnswerJudge(ids, Request.GetUserId(), Request.GetRoleLevel()));
        }

        

        [HttpGet, CheckUserAccess(ActionBits.QuestionReadAccess, ActionBits.QuestionGroupReadAccess)]
        public IHttpActionResult GetAllByQuestionGroupId(int id)
        {
            return Ok(_questionService.GetAllByQuestionGroupId(id));
        }

        [HttpGet, CheckUserAccess(ActionBits.QuestionReadAccess, ActionBits.QuestionGroupReadAccess)]
        public IHttpActionResult GetAllJudgedByUserIdByLessonId( int id)
        {
            return Ok(_questionService.GetAllJudgedByUserIdByLessonId(Request.GetUserId() , Request.GetRoleLevel() ,id));
        }

        [HttpGet, CheckUserAccess(ActionBits.QuestionReadAccess, ActionBits.QuestionGroupReadAccess)]
        public IHttpActionResult GetAllByLessonId(int id)
        {
            return Ok(_questionService.GetAllByLessonId(id));
        }

        [HttpGet, CheckUserAccess(ActionBits.QuestionReadAccess, ActionBits.QuestionGroupReadAccess)]
        public IHttpActionResult GetAllNoTopicByLessonId(int id)
        {
            return Ok(_questionService.GetAllNoTopicByLessonId(id));
        }

        [HttpGet, CheckUserAccess(ActionBits.QuestionReadAccess, ActionBits.QuestionGroupReadAccess)]
        public IHttpActionResult GetAllActiveByLessonId(int id)
        {
            return Ok(_questionService.GetAllActiveByLessonId(id));
        }

        [HttpGet, CheckUserAccess(ActionBits.QuestionReadAccess, ActionBits.QuestionGroupReadAccess)]
        public IHttpActionResult GetAllUnActiveByLessonId(int id)
        {
            return Ok(_questionService.GetAllUnActiveByLessonId(id));
        }

        [HttpGet, CheckUserAccess(ActionBits.QuestionReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var question = _questionService.GetById(id);
            if (question == null)
            {
                return NotFound();
            }
            return Ok(question);
        }

        [HttpGet]
        //[CheckUserAccess(ActionBits.QuestionReadAccess)]
        public HttpResponseMessage GetWordFile(string id)
        {
            var stream = new MemoryStream();
            id += ".docx";
            var filestraem = File.OpenRead(SitePath.GetQuestionAbsPath(id));
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

        [HttpGet]
        //[CheckUserAccess(ActionBits.QuestionReadAccess)]
        public HttpResponseMessage GetPictureFile(string id)
        {
            var stream = new MemoryStream();
            id += ".png";
            var filestraem = File.OpenRead(SitePath.GetQuestionAbsPath(id));
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

        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionCreateAccess)]
        [CheckModelValidation]
        //[CheckWordFileValidation("word", 1024)]
        public IHttpActionResult Create(QuestionCreateViewModel questionViewModel)
        {
            var wordFile = HttpContext.Current.Request.Files.Get("word");
            questionViewModel.UserId = Request.GetUserId();

            var msgRes = _questionService.Create(questionViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Create, "Question", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionCreateAccess)]
        [CheckModelValidation]
        //[CheckWordFileValidation("word", 1024)]
        //[CheckImageQuestionValidation("png")]
        public IHttpActionResult CreateForWindowsApp(QuestionCreateWindowsViewModel questionViewModel)
        {
            questionViewModel.UserId = Request.GetUserId();
            

            var msgRes = _questionService.CreateForWindowsApp(questionViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Create, "Question-WindowsApp", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionUpdateAccess)]
        [CheckModelValidation]
        //[CheckWordFileUpdateValidation("word", 1024)]
        public IHttpActionResult Update(QuestionUpdateViewModel questionViewModel)
        {
            var wordFile = HttpContext.Current.Request.Files.Get("word");
            questionViewModel.UserId = Request.GetUserId();
            
            var msgRes = _questionService.Update(questionViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Update, "Question", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionUpdateImportAccess , ActionBits.QuestionUpdateFinalImportAccess)]
        [CheckModelValidation]
        //[CheckWordFileUpdateValidation("word", 1024)]
        public IHttpActionResult UpdateImport(QuestionUpdateImportViewModel questionViewModel)
        {
            var wordFile = HttpContext.Current.Request.Files.Get("word");

            questionViewModel.UserId = Request.GetUserId();
            var msgRes = _questionService.UpdateImport(questionViewModel );
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Update, "Question-Import", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }


        [HttpPost]
        [CheckUserAccess( ActionBits.QuestionUpdateFinalImportAccess)]
        [CheckModelValidation]
        //[CheckWordFileUpdateValidation("word", 1024)]
        public IHttpActionResult UpdateFinalImport(QuestionUpdateImportViewModel questionViewModel)
        {
            var wordFile = HttpContext.Current.Request.Files.Get("word");

            questionViewModel.UserId = Request.GetUserId();
            var msgRes = _questionService.UpdateFinalImport(questionViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Update, "Question-FinalImport", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionUpdateTopicAccess)]
        [CheckModelValidation]
        //[CheckWordFileUpdateValidation("word", 1024)]
        public IHttpActionResult UpdateTopic(QuestionUpdateTopicViewModel questionViewModel)
        {
            var wordFile = HttpContext.Current.Request.Files.Get("word");

            questionViewModel.UserId = Request.GetUserId();
            var msgRes = _questionService.UpdateTopic(questionViewModel, Request.GetUserId());
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Update, "Question-Topic", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost, CheckUserAccess(ActionBits.QuestionDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgResult = _questionService.Delete(id);
            if (msgResult.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Delete, "Question", msgResult.Obj, Request.GetUserId());
            }
            return Ok(msgResult);
        }
    }
}
