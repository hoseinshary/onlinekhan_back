using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using NasleGhalam.Common;

namespace NasleGhalam.WebApi.FilterAttribute
{
    public class CheckWordFileUpdateValidation : ActionFilterAttribute
    {
        private readonly string _wordFileName;
        private readonly int _wordFileSize;
        public CheckWordFileUpdateValidation(string wordFileName, int wordFileSize)
        {
            _wordFileName = wordFileName;
            _wordFileSize = wordFileSize;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var postedFile = HttpContext.Current.Request.Files.Get(_wordFileName);
            
            var fileExt = Path.GetExtension(postedFile.FileName);
            if (!Utility.CheckWordFileExtension(fileExt))
            {
                actionContext.Response = actionContext
                    .ControllerContext.Request
                    .CreateResponse(HttpStatusCode.OK,
                        new ClientMessageResult
                        {
                            Message = $"صحیح نمی باشد word فرمت فایل",
                            MessageType = MessageType.Error
                        });
            }

            if (postedFile.ContentLength > (_wordFileSize * 1024)) // todo: check length necessary?
            {
                actionContext.Response = actionContext
                    .ControllerContext.Request
                    .CreateResponse(HttpStatusCode.OK,
                        new ClientMessageResult
                        {
                            Message = $" فایل ورد ارسالی باید کمتر از {_wordFileSize} کیلو بایت باشد.", // todo: عکس :D
                            MessageType = MessageType.Error
                        });
            }
        }
    }
}