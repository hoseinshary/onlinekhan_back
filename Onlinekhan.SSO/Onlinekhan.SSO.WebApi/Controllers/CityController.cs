using System.Web.Http;
using Onlinekhan.SSO.Common;
using Onlinekhan.SSO.ServiceLayer.Services;
using Onlinekhan.SSO.WebApi.FilterAttribute;
using Onlinekhan.SSO.ViewModels.City;
using Onlinekhan.SSO.WebApi.Extensions;

namespace Onlinekhan.SSO.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: هاشم معین
	///     date: 25/04/1397
	/// </author>
	public class CityController : ApiController
    {
        private readonly CityService _cityService;
        private readonly LogService _logService;
        public CityController(CityService cityService, LogService logService)
        {
            _cityService = cityService;
            _logService = logService;
        }

        [HttpGet/*, CheckUserAccess(ActionBits.CityReadAccess,ActionBits.PublicAccess)*/]
        public IHttpActionResult GetAll()
        {
            return Ok(_cityService.GetAll());
        }

        [HttpGet, CheckUserAccess(ActionBits.CityReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var city = _cityService.GetById(id);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.CityCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(CityCreateViewModel cityViewModel)
        {
            var msgRes = _cityService.Create(cityViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Create, "City", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.CityUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(CityUpdateViewModel cityViewModel)
        {
            var msgRes = _cityService.Update(cityViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Update, "City", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost, CheckUserAccess(ActionBits.CityDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _cityService.Delete(id);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Delete, "City", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }
    }
}
