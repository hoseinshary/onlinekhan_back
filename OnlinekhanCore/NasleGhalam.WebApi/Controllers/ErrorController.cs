using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Jwt;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.ViewModels.Sale;
using NasleGhalam.ViewModels.Teacher;
using NasleGhalam.WebApi.App_Start;
using NasleGhalam.WebApi.Extensions;
using NasleGhalam.WebApi.FilterAttribute;


namespace NasleGhalam.WebApi.Controllers
{
    public class ErrorController : ApiController
    {
        private readonly ErrorService _errorService;

        public ErrorController(ErrorService errorService)
        {
            _errorService = errorService;
            HttpNotFoundAwareControllerActionSelector.errorController = this;
        }

        [HttpGet, HttpPost, HttpPut, HttpDelete, HttpHead, HttpOptions, AcceptVerbs("PATCH")]
        public HttpResponseMessage Handle404(ErrorCreateViewModel errorViewModel)
        {
            string token = null;
            if (Request.Headers.TryGetValues("Token", out var values))
            {
                token = values.FirstOrDefault();
            }

            if (token != null)
            {
                try
                {
                    // decode token and convert to JwtPayload
                    var jsonPayload = JsonWebToken.Decode(token);
                    var lst = jsonPayload.Value.Split('_');
                    errorViewModel.UserId = Convert.ToInt32(lst[2]);
                }
                catch
                {
                }
            }
            _errorService.Create(errorViewModel);

            var responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
            responseMessage.ReasonPhrase = "The requested resource is not found";
            return responseMessage;
        }

        [HttpPost]
        [CheckUserAccess()]
        [CheckModelValidation]
        public IHttpActionResult Create(ErrorCreateViewModel errorCreateViewModel)
        {
            return Ok(_errorService.Create(errorCreateViewModel));
        }
      

        
    }
}