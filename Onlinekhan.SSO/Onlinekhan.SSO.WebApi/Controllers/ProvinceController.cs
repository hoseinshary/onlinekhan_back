using System.Web.Http;
using Onlinekhan.SSO.Common;
using Onlinekhan.SSO.ServiceLayer.Services;
using Onlinekhan.SSO.ViewModels.Province;
using Onlinekhan.SSO.WebApi.Extensions;
using Onlinekhan.SSO.WebApi.FilterAttribute;

namespace Onlinekhan.SSO.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: هاشم معین
	///     date: 11/4/1397
	/// </author>
	public class ProvinceController : ApiController
    {
        private readonly ProvinceService _provinceService;
        private readonly LogService _logService;
        public ProvinceController(ProvinceService provinceService,LogService logService)
        {
            _provinceService = provinceService;
            _logService = logService;
        }

        [HttpGet/*, CheckUserAccess(ActionBits.ProvinceReadAccess,ActionBits.PublicAccess)*/]
        public IHttpActionResult GetAll()
        {
            return Ok(_provinceService.GetAll());
        }

        [HttpGet, CheckUserAccess(ActionBits.ProvinceReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var province = _provinceService.GetById(id);
            if (province == null)
            {
                return NotFound();
            }
            return Ok(province);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.ProvinceCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(ProvinceViewModel provinceViewModel, LogService logService)
        {
            var msgRes = _provinceService.Create(provinceViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Create, "Province", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.ProvinceUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(ProvinceViewModel provinceViewModel)
        {
            var msgRes = _provinceService.Update(provinceViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Update, "Province", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost, CheckUserAccess(ActionBits.ProvinceDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _provinceService.Delete(id);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Delete, "Province", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }
    }
}
