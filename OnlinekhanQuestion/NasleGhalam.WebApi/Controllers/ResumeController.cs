using System;
using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Resume;
using NasleGhalam.WebApi.Extensions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
    /// <author>
    ///     name: hosein shary
    ///     date: 26/07/2019
    /// </author>
    public class ResumeController : ApiController
    {
        private readonly ResumeService _resumeService;
        private readonly LogService _logService;
        public ResumeController(ResumeService resumeService, LogService logService)
        {
            _resumeService = resumeService;
            _logService = logService;
        }

        [HttpGet]
        [CheckUserAccess(ActionBits.ResumeReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_resumeService.GetAll());
        }

        [HttpGet]
        [CheckUserAccess(ActionBits.ResumeReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var resume = _resumeService.GetById(id);
            if (resume == null)
            {
                return NotFound();
            }
            return Ok(resume);
        }

        [HttpPost]
        //[CheckUserAccess(ActionBits.ResumeCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(ResumeViewModel resumeViewModel)
        {
            resumeViewModel.CreationDateTime=DateTime.Now;
            var msgRes = _resumeService.Create(resumeViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Create, "Resume", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.ResumeDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _resumeService.Delete(id);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Delete, "Resume", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }
    }
}
