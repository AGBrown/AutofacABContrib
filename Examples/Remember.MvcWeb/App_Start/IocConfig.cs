using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Remember.MvcWeb.Models;

namespace Remember.MvcWeb
{
    public class IoCConfig
    {
        /// <summary>
        ///     Configure IoC with Autofac for MVC and this application
        /// </summary>
        /// <returns>The final, built, container</returns>
        public static IContainer RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            RegisterCustomTypes(builder);

            RegisterStandardMvcSetup(builder);

            IContainer container = builder.Build();
            
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            //DomainService.Factory = new AutofacDomainServiceFactory(new MvcContainerProvider());

            return container;
        }

        static void RegisterStandardMvcSetup(ContainerBuilder builder)
        {
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
        }

        static void RegisterCustomTypes(ContainerBuilder builder)
        {
            builder.RegisterType<Remember.Web.Service.FakeLogger>()
                .As<Remember.Web.Service.ILogger>()
                .InstancePerRequest();

            builder.RegisterType<Remember.Service.CaptchaService>()
                   .As<Remember.Service.ICaptchaService>();

            builder.RegisterType<UserStore<MvcWeb.Models.ApplicationUser>>()
                   .As<IUserStore<MvcWeb.Models.ApplicationUser>>()
                   .InstancePerRequest();

            builder.RegisterType<MvcWeb.ApplicationUserManager>()
                   .As<MvcWeb.ApplicationUserManager>()
                   .InstancePerRequest();

            //  note that UserStore<> takes a DbContext, not an ApplicationDbContext
            builder.RegisterType<MvcWeb.Models.ApplicationDbContext>()
                   .As<DbContext, ApplicationDbContext>()
                   .InstancePerRequest();
        }
    }
}