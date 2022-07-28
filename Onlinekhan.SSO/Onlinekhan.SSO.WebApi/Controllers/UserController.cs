using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Onlinekhan.SSO.ServiceLayer.Services;
using Onlinekhan.SSO.Common;
using Onlinekhan.SSO.Service.Services;
using Onlinekhan.SSO.ViewModels.User;
using Onlinekhan.SSO.WebApi.Extensions;
using Onlinekhan.SSO.WebApi.FilterAttribute;


namespace Onlinekhan.SSO.WebApi.Controllers
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
        private readonly PhoneVerificationService _phoneVerificationService;
        public UserController(UserService userService, LogService logService,PhoneVerificationService phoneVerificationService)
        {
            _userService = userService;
            _logService = logService;
            _phoneVerificationService = phoneVerificationService;
        }

        [HttpGet, CheckUserAccess(ActionBits.UserReadAccess)]
        public IHttpActionResult GetAll(UserType userType = UserType.Organ)
        {
            return Ok(_userService.GetAll(Request.GetRoleLevel(), userType));
        }

        

        [HttpGet, CheckUserAccess(ActionBits.UserReadAccess,ActionBits.WritersCodeReadAccess)]
        public IHttpActionResult GetAllSupervisors()
        {
            return Ok(_userService.GetAllSupervisors());
        }

        [HttpGet, CheckUserAccess(ActionBits.WriterCreateAccess, ActionBits.WriterCreateAccess)]
        public IHttpActionResult Search(string nationalNo, string family, string name)
        {
            return Ok(_userService.Search(nationalNo, family, name, Request.GetRoleLevel()));
        }

        [HttpGet, CheckUserAccess(ActionBits.UserReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var user = _userService.GetById(id, Request.GetRoleLevel());
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet, CheckUserAccess(ActionBits.PanelAdminReadAccess , ActionBits.PanelTeacherReadAccess , ActionBits.PanelExpertReadAccess,ActionBits.PanelStudentReadAccess)]
        public IHttpActionResult GetUserData()
        {
            var user = _userService.GetByIdPrivate(Request.GetUserId(), Request.GetRoleLevel());
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        
        /// <summary>
        /// ارسال کد احراز هویت
        /// </summary>
        /// <param name="UserPreRegisterViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult>SendVerificationCode([FromUri] string PhoneNumber)
        {
            var result = await _phoneVerificationService.SendVerificationCode(PhoneNumber);
            if(result >= 2000)
            {
                ClientMessageResult msgRes = new ClientMessageResult() { Message = "کد احراز هویت با موفقیت ارسال شد", MessageType = MessageType.Success };
                return Ok(msgRes);
            }
            else
            {
                ClientMessageResult msgRes = new ClientMessageResult() { Message = "خطایی در ارسال کد احراز هویت وجود امده است", MessageType = MessageType.Error };
                return Ok(msgRes);
            }
            
        }

        [HttpPost]
        public async Task<IHttpActionResult> CheckVerificationCode([FromUri] string PhoneNumber, string Code)
        {
            var result = await _phoneVerificationService.CheckVerificationCode(PhoneNumber, Code);
            return Ok(result);
        }

        /// <summary>
        /// فراموشی رمز عبور
        /// </summary>
        /// <param name="ForgotPasswordViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [CheckModelValidation]
        public async Task<IHttpActionResult> ForgotPassword(ForgotPasswordViewModel forgotViewModel)
        {
            var result = await _phoneVerificationService.CheckVerificationCode(forgotViewModel.Mobile, forgotViewModel.VerificationCode);
            if (result)
            {
                var msgRes = _userService.ForgotPassword(forgotViewModel);
                return Ok(msgRes);
            }
            else
            {
                ClientMessageResult msgRes = new ClientMessageResult() { Message = "کد احراز هویت صحیح نمی باشد", MessageType = MessageType.Error };
                return Ok(msgRes);
            }

        }

        /// <summary>
        /// ثبت نام توسط کاربر پس از دریافت کد احراز هویت
        /// </summary>
        /// <param name="UserPreRegisterViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [CheckModelValidation]
        public async Task<IHttpActionResult> PreRegister(UserPreRegisterViewModel userViewModel)
        {
            var result = await _phoneVerificationService.CheckVerificationCode(userViewModel.Mobile, userViewModel.VerificationCode);
            if (result)
            {
                var msgRes = _userService.PreRegister(userViewModel);
                return Ok(msgRes);
            }
            else
            {
                ClientMessageResult msgRes = new ClientMessageResult() {Message="کد احراز هویت صحیح نمی باشد" ,MessageType=MessageType.Error};
                return Ok(msgRes);
            }

        }
        
        [HttpPost]
        [CheckModelValidation]
        [CheckImageValidationProfileNotRequired("img", 1024)]
        public IHttpActionResult Register([FromUri]UserCreateViewModel userViewModel)
        {
            var postedFile = HttpContext.Current.Request.Files.Get("img");
            if (postedFile != null && postedFile.ContentLength > 0)
            {
                /*{Path.GetExtension(postedFile.FileName)}*/
                userViewModel.ProfilePic = $"{Guid.NewGuid()}";
            }

            var msgRes = _userService.Register(userViewModel);
            if (msgRes.MessageType == MessageType.Success && !string.IsNullOrEmpty(userViewModel.ProfilePic))
            {
                postedFile?.SaveAs(SitePath.ToAbsolutePath($"{SitePath.UserProfileRelPath}{userViewModel.ProfilePic}{Path.GetExtension(postedFile.FileName)}"));
            }
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Create, "User-Register", userViewModel,0);
            }
            return Ok(msgRes);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.UserCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(UserCreateViewModel userViewModel)
        {
            var msgRes = _userService.Create(userViewModel, Request.GetRoleLevel());
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Create, "User", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.UserUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(UserUpdateViewModel userViewModel)
        {
            var msgRes = _userService.Update(userViewModel, Request.GetRoleLevel());
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Update, "User", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.PanelAdminReadAccess, ActionBits.PanelTeacherReadAccess, ActionBits.PanelExpertReadAccess, ActionBits.PanelStudentReadAccess)]
        [CheckModelValidation]
        public IHttpActionResult UpdateUser(UserUpdateViewModel userViewModel)
        {
            var msgRes = _userService.UpdateUser(userViewModel, Request.GetRoleLevel());
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Update, "User-Update", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }
        /*
        [HttpPost]
        [CheckUserAccess(ActionBits.PanelAdminReadAccess, ActionBits.PanelTeacherReadAccess, ActionBits.PanelExpertReadAccess, ActionBits.PanelStudentReadAccess)]
        [CheckModelValidation]
        public IHttpActionResult UpdateUserPassword(UserChangePasswordViewModel userViewModel)
        {
            var msgRes = _userService.UpdateUserPassword(userViewModel, Request.GetRoleLevel() , Request.GetUserId());
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Update, "User-Password", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        */
        [HttpPost, CheckUserAccess(ActionBits.UserDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _userService.Delete(id, Request.GetRoleLevel());
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Delete, "User", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost]
        public IHttpActionResult Login(LoginViewModel login)
        {

            return Ok(_userService.Login(login));
        }

        [HttpGet]
        public HttpResponseMessage GetPictureFile(string id = null)
        {
            var stream = new MemoryStream();
            id += ".jpg";
            var filestraem = File.OpenRead(SitePath.GetUserAbsPath(id));
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
        [CheckModelValidation]
        [CheckUserAccess(ActionBits.PanelAdminReadAccess, ActionBits.PanelTeacherReadAccess, ActionBits.PanelExpertReadAccess, ActionBits.PanelStudentReadAccess)]
        [CheckImageValidationProfileNotRequired("img", 2048)]
        public IHttpActionResult UpdateUserImage()
        {
            var postedFile = HttpContext.Current.Request.Files.Get("img");
            if (postedFile != null && postedFile.ContentLength > 0)
            {
                /*{Path.GetExtension(postedFile.FileName)}*/
                UserViewModel userViewModel = _userService.GetByIdPrivate(Request.GetUserId(), Request.GetRoleLevel());
                var previusFile = userViewModel.ProfilePic;
                userViewModel.ProfilePic = $"{Guid.NewGuid()}";

                var msgRes = _userService.UpdateImage(userViewModel,Request.GetRoleLevel());

                if (msgRes.MessageType == MessageType.Success && !string.IsNullOrEmpty(userViewModel.ProfilePic))
                {
                    postedFile?.SaveAs(SitePath.ToAbsolutePath($"{SitePath.UserProfileRelPath}{userViewModel.ProfilePic}{Path.GetExtension(postedFile.FileName)}"));
                    if (File.Exists(
                        SitePath.ToAbsolutePath($"{SitePath.UserProfileRelPath}{previusFile}{Path.GetExtension(postedFile.FileName)}")))
                    {
                        File.Delete(SitePath.ToAbsolutePath($"{SitePath.UserProfileRelPath}{previusFile}{Path.GetExtension(postedFile.FileName)}"));
                    }
                }
                if (msgRes.MessageType == MessageType.Success)
                {
                    //_logService.Create(CrudType.Update, "User-Image", msgRes.Obj, Request.GetUserId());
                }
                return Ok(msgRes);
            }

            return NotFound();
        }
    }
}
