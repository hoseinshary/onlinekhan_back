using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.ViewModels.Question;
using NasleGhalam.ViewModels.Report;
using NasleGhalam.WebApi.FilterAttribute;
//using NasleGhalam.ViewModels.Report;
using NasleGhalam.WebApi.Extensions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: hosein shary
	///     date: 1398/11/2
	/// </author>
	public class ReportController : ApiController
    {
        private readonly QuestionService _questionService;
        private readonly QuestionJudgeService _questionJudgeService;
        private readonly LessonService _lessonService;

        public ReportController(QuestionService questionService , QuestionJudgeService questionJudgeService , LessonService lessonService)
        {
            _questionService = questionService;
            _questionJudgeService = questionJudgeService;
            _lessonService = lessonService;

        }

        [HttpGet, CheckUserAccess(ActionBits.ReportReadAccess)]
        public IList<AllQuestionOfEachLessonViewModel> GetAllQuestionOfEachLesson()
        {
            return _lessonService.GetAllQuestionOfEachLesson();
        }


        [HttpGet, CheckUserAccess(ActionBits.ReportReadAccess)]
        public IList<AllUsersReporQuestionViewModel> GetAllUsersReport()
        {
            return _questionService.GetAllUsersReport();
        }

        [HttpGet, CheckUserAccess(ActionBits.ReportReadAccess)]
        [CheckModelValidation]

        public IList<QuestionReportViewModel> GetAllQuestionsReport(int id )
        {
            return _questionService.GetAllQuestionsReport(new FilterQuestionReportViewModel{LessonId = id});
        }

        [HttpGet]
        [CheckModelValidation]

        public HttpResponseMessage GetAllQuestionsReportExcel(int id)
        {
            var fileName = _questionService.GetAllQuestionsReportExcel(new FilterQuestionReportViewModel { LessonId = id });

            var stream = new MemoryStream();
            
            var filestraem = File.OpenRead(fileName);
            filestraem.CopyTo(stream);

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(stream.ToArray())
            };
            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "resultReport.xlsx"
                };
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");
            filestraem.Dispose();
            stream.Dispose();
            return result;
        }




    }
}
