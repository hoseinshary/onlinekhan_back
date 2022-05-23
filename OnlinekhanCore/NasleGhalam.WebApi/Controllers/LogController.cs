using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Log;
using System;
using System.IO;
using System.IO.Compression;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: 
	///     date: 
	/// </author>
	public class LogController : ApiController
    {
        private readonly LogService _logService;
        public LogController(LogService logService)
        {
            _logService = logService;
        }

        [HttpGet, CheckUserAccess(ActionBits.UserCreateAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_logService.GetAll());
        }

        [HttpGet, CheckUserAccess(ActionBits.UserCreateAccess)]
        public IHttpActionResult GetById(int id)
        {
            var log = _logService.GetById(id);
            if (log == null)
            {
                return NotFound();
            }
            
            
            return Ok(log);
        }
        [HttpGet, CheckUserAccess(ActionBits.UserCreateAccess)]
        public IHttpActionResult GetValueById(int id)
        {
            var log = _logService.GetValueById(id);
            if (log == null)
            {
                return NotFound();
            }


            return Ok(log);
        }


        

    }
}
