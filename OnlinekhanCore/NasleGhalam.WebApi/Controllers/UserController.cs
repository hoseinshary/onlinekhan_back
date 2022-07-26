using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.ViewModels.User;
using NasleGhalam.WebApi.Extensions;
using NasleGhalam.WebApi.FilterAttribute;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
    /// <author>
    ///     name: علیرضا اعتمادی
    ///     date: 1397.03.28
    /// </author>
    public class UserController : ApiController
    {
        private readonly UserService _userService;
        private readonly LogService _logService;
        public UserController(UserService userService, LogService logService)
        {
            _userService = userService;
            _logService = logService;
        }

        [HttpGet]
        [CheckUserAccess(ActionBits.AssayReadAccess)]
        public IHttpActionResult GetUserAssay()
        {
            return Ok(_userService.GetUserAssay( Request.GetUserId()));
        }
       
        [HttpPost]
        public IHttpActionResult GetUserToken(UserTokenViewModel token)
        {
            return Ok(_userService.GetUserToken(token));
        }

        
    }
}
