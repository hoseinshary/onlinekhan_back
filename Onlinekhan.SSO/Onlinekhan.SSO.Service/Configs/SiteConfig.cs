using AutoMapper;
using Onlinekhan.SSO.ServiceLayer.MapperProfile;

namespace Onlinekhan.SSO.ServiceLayer.Configs
{
    public static class SiteConfig
    {
        public static int MaxDdlCount = 30;
        public static void RegisterAutoMapper()
        {
            Mapper.Initialize(cfg => cfg.AddProfiles(typeof(UserProfile).Assembly));
        }
    }
}
