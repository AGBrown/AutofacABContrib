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
            //  this will fail if the Logger is null (i.e. property injection fails)
            var msg = "<div style='color:black;background-color:{0};z-index:1000;height:50px;padding-top:20px;'>CustomAuthorize - Logger was {1}injected</div>";
            msg = String.Format(msg, Logger == null ? "red" : "green", Logger == null ? "not " : "");
            httpContext.Response.Write(msg);

            var result = base.AuthorizeCore(httpContext);
            return result;
        }
    }
}
