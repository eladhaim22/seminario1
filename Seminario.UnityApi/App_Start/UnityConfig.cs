using Microsoft.Practices.Unity;
using Seminario.NHibernate;
using System.Web.Http;
using Unity.WebApi;

namespace Seminario.UnityApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager(),
new InjectionConstructor());
            container.RegisterType(typeof(IRepository<>), typeof(Repository<>));
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
            
        }
    }
}