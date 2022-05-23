using System;
using System.IO;
using System.Linq;
using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.QuestionGroup;
using System.Web;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using NasleGhalam.WebApi.Extensions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: hosein shary
	///     date: 24-9-97
	/// </author>
	public class QuestionGroupController : ApiController
    {
        private readonly QuestionGroupService _questionGroupService;
        private readonly LogService _logService;
        public QuestionGroupController(QuestionGroupService questionGroupService, LogService logService)
        {
            _questionGroupService = questionGroupService;
            _logService = logService;
        }


        [HttpGet, CheckUserAccess(ActionBits.QuestionGroupReadAccess)]
        public IHttpActionResult GetAllByLessonId(int id)
        {
            return Ok(_questionGroupService.GetAll().Where(current => current.LessonId == id));
        }


        [HttpGet, CheckUserAccess(ActionBits.QuestionGroupReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var questionGroup = _questionGroupService.GetById(id);
            if (questionGroup == null)
            {
                return NotFound();
            }
            return Ok(questionGroup);
        }


        [HttpGet/*, CheckUserAccess(ActionBits.QuestionReadAccess)*/]
        public HttpResponseMessage GetExcelFile(string id)
        {
            id += ".xlsx";

            var stream = new MemoryStream();
            var fileStream = File.OpenRead(SitePath.GetQuestionGroupAbsPath(id));
            fileStream.CopyTo(stream);

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
            fileStream.Dispose();
            stream.Dispose();
            return result;
        }


        [HttpGet/*, CheckUserAccess(ActionBits.QuestionReadAccess)*/]
        public HttpResponseMessage GetWordFile(string id)
        {
            id += ".docx";

            var stream = new MemoryStream();
            var fileStream = File.OpenRead(SitePath.GetQuestionGroupAbsPath(id));
            fileStream.CopyTo(stream);

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
            fileStream.Dispose();
            stream.Dispose();
            return result;
        }

        
        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionGroupCreateAccess)]
        public IHttpActionResult UpdateQuestionsWriter()
        {
            return Ok(_questionGroupService.UpdateQuestionsWriter());
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionGroupCreateAccess)]
        [CheckModelValidation]
        [CheckWordFileValidation("word", 1024)]
        public IHttpActionResult PreCreate([FromUri]QuestionGroupCreateViewModel questionGroupViewModel)
        {
            var wordFile = HttpContext.Current.Request.Files.Get("word");
            if (wordFile == null)
                return NotFound();

            questionGroupViewModel.File = $"{Guid.NewGuid()}{Path.GetExtension(wordFile.FileName)}";
            questionGroupViewModel.UserId = Request.GetUserId();
            var msgRes = _questionGroupService.PreCreate(questionGroupViewModel, wordFile);

            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionGroupCreateAccess)]
        [CheckModelValidation]
        [CheckWordFileValidation("word", 1024)]
        [CheckExcelFileValidation("excel", 1024)]
        public IHttpActionResult Create([FromUri]QuestionGroupCreateViewModel questionGroupViewModel)
        {
            var wordFile = HttpContext.Current.Request.Files.Get("word");
            var excelFile = HttpContext.Current.Request.Files.Get("excel");

            if (wordFile != null && wordFile.ContentLength > 0 &&
                excelFile != null && excelFile.ContentLength > 0)
            {
                questionGroupViewModel.File = $"{Guid.NewGuid()}";
            }

            questionGroupViewModel.UserId = Request.GetUserId();
            var msgRes = _questionGroupService.Create(questionGroupViewModel, wordFile, excelFile);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Create, "QuestionGroup", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionGroupCreateAccess)]
        [CheckModelValidation]
        [CheckWordFileValidation("word", 2048)]
        [CheckExcelFileValidation("excel", 2048)]
        public IHttpActionResult CreateForWindowsApp([FromUri]QuestionGroupCreateViewModel questionGroupViewModel)
        {
            var wordFile = HttpContext.Current.Request.Files.Get("word");
            var excelFile = HttpContext.Current.Request.Files.Get("excel");

            if (wordFile != null && wordFile.ContentLength > 0 &&
                excelFile != null && excelFile.ContentLength > 0)
            {
                questionGroupViewModel.File = $"{Guid.NewGuid()}";
            }

            questionGroupViewModel.UserId = Request.GetUserId();
            
            var msgRes = _questionGroupService.CreateForWindowsApp(questionGroupViewModel, wordFile, excelFile);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Create, "QuestionGroup-WindowsApp", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionGroupUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(QuestionGroupUpdateViewModel questionGroupViewModel)
        {
            var msgRes = _questionGroupService.Update(questionGroupViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Update, "QuestionGroup", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost, CheckUserAccess(ActionBits.QuestionGroupDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _questionGroupService.Delete(id);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Delete, "QuestionGroup", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }
    }
}
