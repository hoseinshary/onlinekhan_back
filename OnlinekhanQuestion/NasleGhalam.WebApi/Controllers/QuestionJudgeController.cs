using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.QuestionJudge;
using NasleGhalam.WebApi.Extensions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: hosein shary  
	///     date: 1397/12/10
	/// </author>
	public class QuestionJudgeController : ApiController
    {
        private readonly QuestionJudgeService _questionJudgeService;
        private readonly LogService _logService;
        public QuestionJudgeController(QuestionJudgeService questionJudgeService, LogService logService)
        {
            _questionJudgeService = questionJudgeService;
            _logService = logService;
        }


        [HttpGet, CheckUserAccess(ActionBits.QuestionJudgeReadAccess)]
        public IHttpActionResult GetAllByQuestionId(int id)
        {
            return Ok(_questionJudgeService.GetAllByQuestionId(id,Request.GetUserId() , Request.GetRoleLevel()));
        }


        [HttpGet, CheckUserAccess(ActionBits.QuestionJudgeReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var questionJudge = _questionJudgeService.GetById(id);
            if (questionJudge == null)
            {
                return NotFound();
            }
            return Ok(questionJudge);
        }

        //[HttpGet, CheckUserAccess(ActionBits.QuestionJudgeReadAccess)]
        //public IHttpActionResult CorrectAllJudges()
        //{
        //    _questionJudgeService.CorrectAllJudges();
            
        //    return Ok();
        //}

        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionJudgeCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(QuestionJudgeCreateViewModel questionJudgeViewModel)
        {
            var msgRes = _questionJudgeService.Create(questionJudgeViewModel, Request.GetUserId());
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Create, "QuestionJudge", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);

        }


        [HttpPost]
        [CheckUserAccess(ActionBits.QuestionJudgeUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(QuestionJudgeUpdateViewModel questionJudgeViewModel)
        {
            var msgRes = _questionJudgeService.Update(questionJudgeViewModel, Request.GetUserId());
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Update, "QuestionJudge", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }


        [HttpPost, CheckUserAccess(ActionBits.QuestionJudgeDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _questionJudgeService.Delete(id);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Delete, "QuestionJudge", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }
    }
}
