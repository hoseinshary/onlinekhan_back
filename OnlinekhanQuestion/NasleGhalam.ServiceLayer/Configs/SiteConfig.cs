using AutoMapper;
using NasleGhalam.ServiceLayer.MapperProfile;

namespace NasleGhalam.ServiceLayer.Configs
{
    public static class SiteConfig
    {
        public static int MaxDdlCount = 30;
        public static void RegisterAutoMapper()
        {
            Mapper.Initialize(cfg => cfg.AddProfiles(typeof(RoleProfile).Assembly));
        }
    }
}
