using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.EducationYear;
using NasleGhalam.WebApi.Extensions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: هاشم معین
	///     date: 28/04/97
	/// </author>
	public class EducationYearController : ApiController
    {
        private readonly EducationYearService _educationYearService;
        private readonly LogService _logService;
        public EducationYearController(EducationYearService educationYearService, LogService logService)
        {
            _educationYearService = educationYearService;
            _logService = logService;
        }

        [HttpGet, CheckUserAccess(ActionBits.EducationYearReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_educationYearService.GetAll());
        }

        [HttpGet, CheckUserAccess(ActionBits.EducationYearReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var educationYear = _educationYearService.GetById(id);
            if (educationYear == null)
            {
                return NotFound();
            }
            return Ok(educationYear);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.EducationYearCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(EducationYearCreateViewModel educationYearViewModel)
        {
            var msgRes = _educationYearService.Create(educationYearViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Create, "EducationYear", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.EducationYearUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(EducationYearUpdateViewModel educationYearViewModel)
        {
            var msgRes = _educationYearService.Update(educationYearViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Update, "EducationYear", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost, CheckUserAccess(ActionBits.EducationYearDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _educationYearService.Delete(id);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Delete, "EducationYear", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }
    }
}
