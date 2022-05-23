using System.Collections.Generic;

using Onlinekhan.SSO.Common;
using Onlinekhan.SSO.ViewModel.Action;

namespace Onlinekhan.SSO.ViewModel.User
{
    public class LoginResultViewModel
    {
        public MessageType MessageType { get; set; }

        public string Message { get; set; }

        public string Token { get; set; }

        public string FullName { get; set; }

        public string ProfilePic { get; set; }

        public string DefaultPage { get; set; }

        public IList<SubMenuViewModel> SubMenus { get; set; }

        public IList<MenuViewModel> Menus { get; set; }
    }
}