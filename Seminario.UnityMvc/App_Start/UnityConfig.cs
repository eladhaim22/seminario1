using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Seminario.NHibernate;

namespace Seminario.UnityMvc
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
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}