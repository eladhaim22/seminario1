using System;
using System.Reflection;
using FluentValidation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Seminario.Model;
using Seminario.NHibernate;
using Seminario.Validationes;
using Seminario.WebServices;
using Seminario.WebServices.Contracts;

namespace Seminario.Ioc
{
	public static class UnityConfig
	{
		public static void RegisterComponents()
		{
			var container = new UnityContainer();
			container.AddNewExtension<Interception>();
			// register all your components with the container here
			// it is NOT necessary to register your controllers
			container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager(),
			new InjectionConstructor());
			var interceptor = new InterceptionBehavior<ExceptionInterceptor>();
			container.RegisterType<ISimulacionService, SimulacionService>(interceptor);
			container.RegisterType<IProductoService, ProductoService>(interceptor);
			container.RegisterType<IProvinciaService, ProvinciaService>(interceptor);
			container.RegisterType<IEmpleadoService, EmpleadoService>(interceptor);
			container.RegisterType<IDatosTTService, DatosTTService>(interceptor);
			System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
			System.Web.Mvc.DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
		}
	}
}
