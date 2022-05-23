using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.QuestionAnswerJudge;
using NasleGhalam.WebApi.Extensions;


namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: hosein shary
	///     date: 1398-08-03
	/// </author>
	public class QuestionAnswerJudgeController : ApiController
    {
        private readonly QuestionAnswerJudgeService _questionAnswerJudgeService;
        private readonly LogService _logService;
        public QuestionAnswerJudgeController(QuestionAnswerJudgeService questionAnswerJudgeService, LogService logService)
        {
            _questionAnswerJudgeService = questionAnswerJudgeService;
            _logService = logService;
        }

        [HttpGet, CheckUserAccess(ActionBits.QuestionAnswerJudgeReadAccess)]
        public IHttpActionResult GetAllByQuestionAnswerId(int id)
        {
            return Ok(_questionAnswerJudgeService.GetAllByQuestionAnswerId(id, Request.GetUserId(),Request.GetRoleLevel()));
        }

        [HttpGet, CheckUserAccess(ActionBits.QuestionAnswerJudgeReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var questionAnswerJudge = _questionAnswerJudgeService.GetById(id);
            if (questionAnswerJudge == null)
            {
                return NotFound();
            }
            return Ok(questionAnswerJudge);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionAnswerJudgeCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(QuestionAnswerJudgeCreateViewModel questionAnswerJudgeViewModel)
        {
            questionAnswerJudgeViewModel.UserId = Request.GetUserId();
            var msgRes = _questionAnswerJudgeService.Create(questionAnswerJudgeViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Create, "QuestionAnswerJudge", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionAnswerJudgeUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(QuestionAnswerJudgeUpdateViewModel questionAnswerJudgeViewModel)
        {
            questionAnswerJudgeViewModel.UserId = Request.GetUserId();
            var msgRes = _questionAnswerJudgeService.Update(questionAnswerJudgeViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Update, "QuestionAnswerJudge", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost, CheckUserAccess(ActionBits.QuestionAnswerJudgeDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _questionAnswerJudgeService.Delete(id);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Delete, "QuestionAnswerJudge", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }
    }
}
