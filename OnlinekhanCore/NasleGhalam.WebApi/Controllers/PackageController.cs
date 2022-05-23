using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Package;
using System.Web;
using System;
using System.Collections.Generic;
using System.IO;
using NasleGhalam.WebApi.Extensions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: hoseinshary   
	///     date: 13/8/98
	/// </author>
	public class PackageController : ApiController
    {
        private readonly PackageService _packageService;
        private readonly LogService _logService;
        public PackageController(PackageService packageService, LogService logService)
        {
            _packageService = packageService;
            _logService = logService;
        }

        [HttpGet, CheckUserAccess(ActionBits.PackageReadAccess)]
        public IHttpActionResult GetAllByEducationTreeId([FromUri]IEnumerable<int> ids)
        {
            return Ok(_packageService.GetAllByEducationTreeId(ids));
        }

        [HttpGet, CheckUserAccess(ActionBits.PackageReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var package = _packageService.GetById(id);
            if (package == null)
            {
                return NotFound();
            }
            return Ok(package);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.PackageCreateAccess)]
        [CheckModelValidation]
        [CheckImageValidationNotRequired("img", 1024)]
        public IHttpActionResult Create([FromUri]PackageCreateViewModel packageViewModel)
        {
            var postedFile = HttpContext.Current.Request.Files.Get("img");
            if (postedFile != null && postedFile.ContentLength > 0)
            {
                packageViewModel.ImageFile = $"{Guid.NewGuid()}{Path.GetExtension(postedFile.FileName)}";
            }

            var msgRes = _packageService.Create(packageViewModel);

            if (msgRes.MessageType == MessageType.Success && !string.IsNullOrEmpty(packageViewModel.ImageFile))
            {
                postedFile?.SaveAs($"{SitePath.PackageRelPath}{packageViewModel.ImageFile}".ToAbsolutePath());
            }
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Create, "Package", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.PackageUpdateAccess)]
        [CheckModelValidation]
        [CheckImageValidationNotRequired("img", 1024)]
        public IHttpActionResult Update([FromUri]PackageUpdateViewModel packageViewModel)
        {

            var postedFile = HttpContext.Current.Request.Files.Get("img");
            var oldFile = packageViewModel.ImageFile;

            if (postedFile != null && postedFile.ContentLength > 0)
                packageViewModel.ImageFile = $"{Guid.NewGuid()}{Path.GetExtension(postedFile.FileName)}";

            var msgRes = _packageService.Update(packageViewModel);
            if (msgRes.MessageType == MessageType.Success && !string.IsNullOrEmpty(packageViewModel.ImageFile))
            {
                if (File.Exists($"{SitePath.PackageRelPath}{oldFile}".ToAbsolutePath()))
                    File.Delete($"{SitePath.PackageRelPath}{oldFile}".ToAbsolutePath());

                postedFile?.SaveAs($"{SitePath.PackageRelPath}{packageViewModel.ImageFile}".ToAbsolutePath());
            }
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Update, "Package", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost, CheckUserAccess(ActionBits.PackageDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _packageService.Delete(id);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Delete, "Package", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }
    }
}
