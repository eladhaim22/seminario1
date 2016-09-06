using FluentValidation;
using Microsoft.Practices.Unity;
using Seminario.Model;
using Seminario.NHibernate;
using Seminario.Validationes;
using Seminario.WebServices;
using Seminario.WebServices.Contracts;

namespace Seminario.Unity
{
	public static class UnityConfig
	{
		public static void RegisterComponents()
		{
			var container = new UnityContainer();

			// register all your components with the container here
			// it is NOT necessary to register your controllers

			container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager(),
			new InjectionConstructor());

			container.RegisterType<ISimulacionService, SimulacionService>();
			container.RegisterType<IProductoService, ProductoService>();
			container.RegisterType<IProvinciaService, ProvinciaService>();
			container.RegisterType<IEmpleadoService, EmpleadoService>();
			container.RegisterType<IDatosTTService, DatosTTService>();
			System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
			//RegisterValidators(container);
			System.Web.Mvc.DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
		}
	}
}
