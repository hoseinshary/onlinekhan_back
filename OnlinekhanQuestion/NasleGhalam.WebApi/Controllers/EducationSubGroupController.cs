using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.EducationSubGroup;
using NasleGhalam.WebApi.Extensions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: هاشم معین
	///     date: 06/08/1397
	/// </author>
	public class EducationSubGroupController : ApiController
    {
        private readonly EducationSubGroupService _educationSubGroupService;
        private readonly LogService _logService;
        public EducationSubGroupController(EducationSubGroupService educationSubGroupService, LogService logService)
        {
            _educationSubGroupService = educationSubGroupService;
            _logService = logService;
        }

        [HttpGet, CheckUserAccess(ActionBits.EducationSubGroupReadAccess,ActionBits.TopicReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_educationSubGroupService.GetAll());
        }

        [HttpGet, CheckUserAccess(ActionBits.EducationSubGroupReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var educationSubGroup = _educationSubGroupService.GetById(id);
            if (educationSubGroup == null)
            {
                return NotFound();
            }
            return Ok(educationSubGroup);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.EducationSubGroupCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(EducationSubGroupCreateViewModel educationSubGroupViewModel)
        {
            var msgRes = _educationSubGroupService.Create(educationSubGroupViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Create, "EducationSubGroup", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.EducationSubGroupUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(EducationSubGroupUpdateViewModel educationSubGroupViewModel)
        {
            var msgRes = _educationSubGroupService.Update(educationSubGroupViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Update, "EducationSubGroup", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost, CheckUserAccess(ActionBits.EducationSubGroupDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _educationSubGroupService.Delete(id);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Delete, "EducationSubGroup", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }
    }
}
