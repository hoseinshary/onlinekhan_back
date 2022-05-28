using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using Onlinekhan.SSO.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.AssayAnswerSheet;
using Onlinekhan.SSO.WebApi.Extentions;

namespace Onlinekhan.SSO.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: 
	///     date: 
	/// </author>
	public class AssayAnswerSheetController : ApiController
	{
        private readonly AssayAnswerSheetService _assayAnswerSheetService;
		public AssayAnswerSheetController(AssayAnswerSheetService assayAnswerSheetService)
        {
            _assayAnswerSheetService = assayAnswerSheetService;
        }

		[HttpGet, CheckUserAccess(ActionBits.AssayAnswerSheetReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_assayAnswerSheetService.GetAll());
        }

		[HttpGet, CheckUserAccess(ActionBits.AssayAnswerSheetReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var assayAnswerSheet = _assayAnswerSheetService.GetById(id);
            if (assayAnswerSheet == null)
            {
                return NotFound();
            }
            return Ok(assayAnswerSheet);
        }

		[HttpPost]
        [CheckUserAccess(ActionBits.AssayAnswerSheetCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(AssayAnswerSheetCreateViewModel assayAnswerSheetViewModel)
        {
            return Ok(_assayAnswerSheetService.Create(assayAnswerSheetViewModel));
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.AssayAnswerSheetUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(AssayAnswerSheetUpdateViewModel assayAnswerSheetViewModel)
        {
            return Ok(_assayAnswerSheetService.Update(assayAnswerSheetViewModel));
        }

        [HttpPost, CheckUserAccess(ActionBits.AssayAnswerSheetDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            return Ok(_assayAnswerSheetService.Delete(id));
        }
	}
}
