using System;
using System.Threading;
using Onlinekhan.SSO.DataAccess.Context;
//using Onlinekhan.SSO.ServiceLayer.Services;
using StructureMap;
using StructureMap.Web;

namespace Onlinekhan.SSO.ServiceLayer.Configs
{
    public static class StructureMapConfig
    {
        private static readonly Lazy<Container> _containerBuilder =
            new Lazy<Container>(defaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

        public static IContainer Container => _containerBuilder.Value;

        private static Container defaultContainer()
        {
            return new Container(cfg =>
            {
                //cfg.For<IUnitOfWork>().Transient().Use<DBContext>();
                //cfg.For<IUnitOfWork>().Singleton().Use<DBContext>();
                //cfg.For<IUnitOfWork>().ContainerScoped().Use<DBContext>();
                //cfg.For<IUnitOfWork>().AlwaysUnique().Use<DBContext>();
                //cfg.For<IUnitOfWork>().ThreadLocal().Use<DBContext>();

                cfg.For<IUnitOfWork>().HybridHttpOrThreadLocalScoped().Use<DBContext>();

                cfg.Scan(scan =>
                {
                //    scan.AssemblyContainingType<ActionService>();
                    scan.WithDefaultConventions();
                });
            });
        }
    }
}
