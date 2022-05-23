using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.ViewModels.User;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Writer;
using NasleGhalam.WebApi.Extensions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
    /// <author>
    ///     name: حسین شری
    ///     date: 2015-05-15
    /// </author>
    public class WriterController : ApiController
    {
        private readonly WriterService _writerService;
        private readonly LogService _logService;
        public WriterController(WriterService writerService, LogService logService)
        {
            _writerService = writerService;
            _logService = logService;
        }

        [HttpGet, CheckUserAccess(ActionBits.WriterReadAccess, ActionBits.WritersCodeReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_writerService.GetAll());
        }

        [HttpGet, CheckUserAccess(ActionBits.WriterReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var writer = _writerService.GetById(id);
            if (writer == null)
            {
                return NotFound();
            }
            return Ok(writer);
        }

        [HttpGet]
        public HttpResponseMessage GetPictureFile(string id = null)
        {
            var stream = new MemoryStream();
            id += ".jpg";
            var filestraem = File.OpenRead(SitePath.GetWriterAbsPath(id));
            filestraem.CopyTo(stream);

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(stream.ToArray())
            };
            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = id
                };
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");
            filestraem.Dispose();
            stream.Dispose();
            return result;
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.WriterCreateAccess)]
        [CheckModelValidation]
        [CheckImageValidationProfileNotRequired("img", 2048)]
        public IHttpActionResult Create([FromUri]WriterCreateViewModel writerViewModel)
        {
            var postedFile = HttpContext.Current.Request.Files.Get("img");
            if (postedFile != null && postedFile.ContentLength > 0)
            {
                writerViewModel.ProfilePic = $"{Guid.NewGuid()}";

                var msgRes = _writerService.Create(writerViewModel);

                if (msgRes.MessageType == MessageType.Success && !string.IsNullOrEmpty(writerViewModel.ProfilePic))
                {
                    postedFile?.SaveAs($"{SitePath.WriterPictureRelPath}{writerViewModel.ProfilePic}{Path.GetExtension(postedFile.FileName)}".ToAbsolutePath());
                }
                if (msgRes.MessageType == MessageType.Success)
                {
                    _logService.Create(CrudType.Create, "Writer", msgRes.Obj, Request.GetUserId());
                }
                return Ok(msgRes);
            }

            return NotFound();

        }

        [HttpPost]
        [CheckUserAccess(ActionBits.WriterUpdateAccess)]
        [CheckModelValidation]
        [CheckImageValidationProfileNotRequired("img", 2048)]
        public IHttpActionResult Update([FromUri] WriterUpdateViewModel writerViewModel)
        {
            var postedFile = HttpContext.Current.Request.Files.Get("img");
            if (postedFile != null && postedFile.ContentLength > 0)
            {
                /*{Path.GetExtension(postedFile.FileName)}*/
                WriterViewModel writerViewModel2 = _writerService.GetById(writerViewModel.Id);
                var previusFile = writerViewModel2.ProfilePic;
                writerViewModel.ProfilePic = $"{Guid.NewGuid()}";

                var msgRes = _writerService.Update(writerViewModel);

                if (msgRes.MessageType == MessageType.Success && !string.IsNullOrEmpty(writerViewModel.ProfilePic))
                {
           

                    postedFile?.SaveAs($"{SitePath.WriterPictureRelPath}{writerViewModel.ProfilePic}{Path.GetExtension(postedFile.FileName)}".ToAbsolutePath());
                    if (File.Exists(
                        $"{SitePath.WriterPictureRelPath}{previusFile}{Path.GetExtension(postedFile.FileName)}"
                            .ToAbsolutePath()))
                    {
                        File.Delete($"{SitePath.WriterPictureRelPath}{previusFile}{Path.GetExtension(postedFile.FileName)}"
                            .ToAbsolutePath());
                    }
                    
                    
                }
                if (msgRes.MessageType == MessageType.Success)
                {
                    _logService.Create(CrudType.Update, "Writer", msgRes.Obj, Request.GetUserId());
                }
                return Ok(msgRes);
            }

            return NotFound();
        }

        [HttpPost, CheckUserAccess(ActionBits.WriterDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _writerService.Delete(id);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Delete, "Writer", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }
    }
}
