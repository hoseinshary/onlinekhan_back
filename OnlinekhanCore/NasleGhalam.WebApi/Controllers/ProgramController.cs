using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Program;
using NasleGhalam.WebApi.Extensions;
using System;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: hosein shary
	///     date: 19/6/2020
	/// </author>
	public class ProgramController : ApiController
    {
        private readonly ProgramService _programService;
        private readonly LogService _logService;
        public ProgramController(ProgramService programService,LogService logService)
        {
            _programService = programService;
            _logService = logService;
        }

        [HttpGet, CheckUserAccess(ActionBits.ProgramReadAccess)]
        public IHttpActionResult GetAll()
        {
            int userid = Request.GetUserId();
            return Ok(_programService.GetAll(userid));
        }

        [HttpGet, CheckUserAccess(ActionBits.ProgramReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var program = _programService.GetById(id);
            if (program == null)
            {
                return NotFound();
            }
            return Ok(program);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.ProgramCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(ProgramCreateViewModel programViewModel)
        {
            var result = _programService.Create(programViewModel);
            if(result.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Create, "Program", result.Obj,Request.GetUserId());
            }
            return Ok(result);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.ProgramUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(ProgramCreateViewModel programViewModel)
        {
            var msgRes = _programService.Update(programViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Update, "Program", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost, CheckUserAccess(ActionBits.ProgramDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _programService.Delete(id);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Delete, "Program", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }
    }
}
