using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Remember.Web.Service
{
    /// <summary>
    ///     A custom authorization attribute, as per 
    ///     https://github.com/autofac/Autofac/wiki/Mvc-Integration#inject-properties-into-filterattributes
    /// </summary>
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public ILogger Logger { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            Logger.Log("AuthorizeCore");
            return base.AuthorizeCore(httpContext);
        }
    }
}
