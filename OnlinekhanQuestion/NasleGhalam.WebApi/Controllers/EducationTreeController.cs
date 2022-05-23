using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.EducationTree;
using NasleGhalam.WebApi.Extensions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: هاشم معین
	///     date: 06/08/97
	/// </author>
	public class EducationTreeController : ApiController
    {
        private readonly EducationTreeService _educationTreeService;
        private readonly LogService _logService;
        public EducationTreeController(EducationTreeService educationTreeService, LogService logService)
        {
            _educationTreeService = educationTreeService;
            _logService = logService;
        }

        [HttpGet, CheckUserAccess(ActionBits.EducationTreeReadAccess, ActionBits.TopicReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_educationTreeService.GetAll());
        }

        [HttpGet, CheckUserAccess(ActionBits.QuestionReadAccess)]
        public IHttpActionResult GetAllByLessonId(int id)
        {
            return Ok(_educationTreeService.GetAllByLessonId(id));
        }

        [HttpGet, CheckUserAccess(ActionBits.EducationTreeReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var educationTree = _educationTreeService.GetById(id);
            if (educationTree == null)
            {
                return NotFound();
            }
            return Ok(educationTree);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.EducationTreeCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(EducationTreeCreateViewModel educationTreeViewModel)
        {
            var msgRes = _educationTreeService.Create(educationTreeViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Create, "EducationTree", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.EducationTreeUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(EducationTreeUpdateViewModel educationTreeViewModel)
        {
            var msgRes = _educationTreeService.Update(educationTreeViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Update, "EducationTree", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost, CheckUserAccess(ActionBits.EducationTreeDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _educationTreeService.Delete(id);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Delete, "EducationTree", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }
    }
}
