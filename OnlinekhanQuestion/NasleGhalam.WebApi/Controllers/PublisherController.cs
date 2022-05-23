using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Publisher;
using NasleGhalam.WebApi.Extensions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: هاشم معین
	///     date: 07/06/1397
	/// </author>
	public class PublisherController : ApiController
    {
        private readonly PublisherService _publisherService;
        private readonly LogService _logService;
        public PublisherController(PublisherService publisherService, LogService logService)
        {
            _publisherService = publisherService;
            _logService = logService;
        }

        [HttpGet, CheckUserAccess(ActionBits.PublisherReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_publisherService.GetAll());
        }

        [HttpGet, CheckUserAccess(ActionBits.PublisherReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var publisher = _publisherService.GetById(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return Ok(publisher);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.PublisherCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(PublisherViewModel publisherViewModel)
        {
            var msgRes = _publisherService.Create(publisherViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Create, "Publisher", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.PublisherUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(PublisherViewModel publisherViewModel)
        {
            var msgRes = _publisherService.Update(publisherViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Update, "Publisher", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost, CheckUserAccess(ActionBits.PublisherDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _publisherService.Delete(id);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Delete, "Publisher", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }
    }
}
