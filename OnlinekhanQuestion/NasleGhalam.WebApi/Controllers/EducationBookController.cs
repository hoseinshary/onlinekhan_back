using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.EducationBook;
using NasleGhalam.WebApi.Extensions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: علیرضا اعتمادی
	///     date: 1397/06/09
	/// </author>
	public class EducationBookController : ApiController
    {
        private readonly EducationBookService _educationBookService;
        private readonly LogService _logService;
        public EducationBookController(EducationBookService educationBookService, LogService logService)
        {
            _educationBookService = educationBookService;
            _logService = logService;
        }


        [HttpGet, CheckUserAccess(ActionBits.EducationBookReadAccess)]
        public IHttpActionResult GetAllByLessonId(int id)
        {
            return Ok(_educationBookService.GetAllByLessonId(id));
        }


        [HttpGet, CheckUserAccess(ActionBits.EducationBookReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var educationBook = _educationBookService.GetById(id);
            if (educationBook == null)
            {
                return NotFound();
            }
            return Ok(educationBook);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.EducationBookCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(EducationBookCreateViewModel educationBookViewModel)
        {
            var msgRes = _educationBookService.Create(educationBookViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Create, "EducationBook", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.EducationBookUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(EducationBookCreateViewModel educationBookViewModel)
        {
            var msgRes = _educationBookService.Update(educationBookViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Update, "EducationBook", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }


        [HttpPost, CheckUserAccess(ActionBits.EducationBookDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _educationBookService.Delete(id);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Delete, "EducationBook", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }
    }
}
