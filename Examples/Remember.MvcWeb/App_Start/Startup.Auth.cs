using Autofac;
using Autofac.Integration.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Google;
using Owin;
using System;
using Remember.MvcWeb.Models;

namespace Remember.MvcWeb
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            var container = IoCConfig.RegisterDependencies();

            // IMPORTANT! This should be the first middleware added to the IAppBuilder.
            app.UseAutofacMiddleware(container);

            //  Configure the db context and user manager to use a single instance per request
            //      Note: this is now done through Autofac, and the boilerplate code is therefore altered as follows:

            //  1. This is not required if you inject ApplicationDbContext through Autofac in controllers instead of doing
            //      HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            //app.CreatePerOwinContext(ApplicationDbContext.Create);
            
            //  2. This is required due to the internals of some of the Mvc Owin security methods, but 
            //      we modify to inject from the autofac lifetime instead
            //  Old: app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationUserManager>((options, context) =>
            {
                //  Split this out for clarity and debugging, but could be a simple lambda
                var lts = context.GetAutofacLifetimeScope();
                var aum = lts.Resolve<ApplicationUserManager>();
                return aum;
            });

            app.UseAutofacMvc();

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }
    }
}