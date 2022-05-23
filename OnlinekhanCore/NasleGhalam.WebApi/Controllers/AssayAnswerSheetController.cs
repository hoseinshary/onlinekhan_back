using System;
using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.AssayAnswerSheet;
using NasleGhalam.WebApi.Extensions;


namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: hosein shary 
	///     date:  10 aban 1400
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


        [HttpGet]
        [CheckUserAccess(ActionBits.AssayAnswerSheetReadAccess)]
        [CheckModelValidation]
        public IHttpActionResult Report(/*int id*/)
        {
            int id = Request.GetUserId();
            return Ok(_assayAnswerSheetService.Report(id));
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.AssayAnswerSheetCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(AssayAnswerSheetCreateViewModel assayAnswerSheetViewModel)
        {
            assayAnswerSheetViewModel.UserId = Request.GetUserId();
            assayAnswerSheetViewModel.DateTime = DateTime.Now;
            
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
