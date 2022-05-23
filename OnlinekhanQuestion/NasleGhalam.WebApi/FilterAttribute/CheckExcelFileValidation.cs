using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using NasleGhalam.Common;

namespace NasleGhalam.WebApi.FilterAttribute
{
    public class CheckExcelFileValidation : ActionFilterAttribute
    {
        private readonly string _excelFileName;
        private readonly int _excelFileSize;
        public CheckExcelFileValidation(string excelFileName, int excelFileSize)
        {
            _excelFileName = excelFileName;
            _excelFileSize = excelFileSize;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var postedFile = HttpContext.Current.Request.Files.Get(_excelFileName);
            if (postedFile == null || postedFile.ContentLength <= 0)
            {
                actionContext.Response = actionContext
                    .ControllerContext.Request
                    .CreateResponse(HttpStatusCode.OK,
                        new ClientMessageResult
                        {
                            Message = $"وارد نشده است Excel فایل ",
                            MessageType = MessageType.Error
                        });
                return;
            }

            var fileExt = Path.GetExtension(postedFile.FileName);
            if (!Utility.CheckExcelFileExtension(fileExt))
            {
                actionContext.Response = actionContext
                    .ControllerContext.Request
                    .CreateResponse(HttpStatusCode.OK,
                        new ClientMessageResult
                        {
                            Message = $"صحیح نمی باشد Excel فرمت فایل",
                            MessageType = MessageType.Error
                        });
            }

            if (postedFile.ContentLength > (_excelFileSize * 1024))
            {
                actionContext.Response = actionContext
                    .ControllerContext.Request
                    .CreateResponse(HttpStatusCode.OK,
                        new ClientMessageResult
                        {
                            Message = $" فایل اکسل ارسالی باید کمتر از {_excelFileSize} کیلو بایت باشد.",
                            MessageType = MessageType.Error
                        });
            }
        }
    }
}