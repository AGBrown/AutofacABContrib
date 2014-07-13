using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Remember.Service;

namespace Remember.MvcWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");

            AreaRegistration.RegisterAllAreas();

            routes.MapRoute(
                "Default",                              // Route Name
                "{controller}/{action}/{id}",           // Route URL (pattern)
                new
                {                                   // Route Detauls
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                },
                new[] { "Remember.Web.Controllers" }      // Route Namespaces that take preference
            );
        }
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Remember.Web.Service.FakeLogger>()
                   .As<Remember.Web.Service.ILogger>()
                   .InstancePerRequest();
            builder.RegisterType<CaptchaService>().As<ICaptchaService>();

            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterFilterProvider();

            //builder.RegisterModule<NHibernateModule>();

            // Change controller action parameter injection by changing web.config.
            builder.RegisterType<ExtensibleActionInvoker>().As<IActionInvoker>().InstancePerRequest();

            // MVC integration test items
            //builder.RegisterType<InvokerDependency>().As<IInvokerDependency>();

            // DomainServices
            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AssignableTo<DomainService>();
            //builder.RegisterModule<AutofacDomainServiceModule>();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            //DomainService.Factory = new AutofacDomainServiceFactory(new MvcContainerProvider());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
