using Onlinekhan.SSO.Common;

namespace Onlinekhan.SSO.ViewModels.Role
{
    public class RoleViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte Level { get; set; }

        public string SumOfActionBit { get; set; }

        public UserType UserType { get; set; }

        public string UserTypeName => UserType.GetDisplayName();
    }
}
