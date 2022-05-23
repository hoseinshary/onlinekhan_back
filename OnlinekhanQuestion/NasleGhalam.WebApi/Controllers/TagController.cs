using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Tag;
using NasleGhalam.WebApi.Extensions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: هاشم معین
	///     date: 11/06/1397
	/// </author>
	public class TagController : ApiController
    {
        private readonly TagService _tagService;
        private readonly LogService _logService;
        public TagController(TagService tagService, LogService logService)
        {
            _tagService = tagService;
            _logService = logService;
        }

        [HttpGet, CheckUserAccess(ActionBits.TagReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_tagService.GetAll());
        }

        [HttpGet, CheckUserAccess(ActionBits.TagReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var tag = _tagService.GetById(id);
            if (tag == null)
            {
                return NotFound();
            }
            return Ok(tag);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.TagCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(TagViewModel tagViewModel)
        {
            var msgRes = _tagService.Create(tagViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Create, "Tag", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);

        }

        [HttpPost]
        [CheckUserAccess(ActionBits.TagUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(TagViewModel tagViewModel)
        {
            var msgRes = _tagService.Update(tagViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Update, "Tag", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost, CheckUserAccess(ActionBits.TagDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _tagService.Delete(id);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Delete, "Tag", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }
    }
}
