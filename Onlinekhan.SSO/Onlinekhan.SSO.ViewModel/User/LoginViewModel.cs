using System.Security.Policy;
using Onlinekhan.SSO.DomainClasses.Entities;
namespace Onlinekhan.SSO.ViewModels.User
{
    public class LoginViewModel
    {
        
        public string UserName { get; set; }

        public string Password { get; set; }

        public DomainClasses.Entities.Site Site { get; set; }
    }
}
