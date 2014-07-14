// This software is part of the Autofac IoC container
// Copyright © 2014 Autofac Contributors
// http://autofac.org
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.

using System.ComponentModel;
using System.Security;
using System.Web.Mvc;

namespace Autofac.Integration.Mvc.Owin
{
    /// <summary>
    ///     Extension methods for configuring the OWIN pipeline's AutofacDependencyResolver.
    /// </summary>
    [SecuritySafeCritical]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class AutofacDependencyResolverExtensions
    {
        /// <summary>
        ///     Once the <see cref="AutofacDependencyResolver"/> is created with the roo container
        ///     and set for Mvc to use with <see cref="DependencyResolver.SetResolver(IDependencyResolver)"/> 
        ///     use this method to register the resolver with the root container so that it is available to Owin.
        /// </summary>
        /// <param name="dependencyResolver">The <see cref="AutofacDependencyResolver"/>
        ///     for the root container</param>
        /// <param name="rootContainer">The root container from <c>builder.Build()</c></param>
        [SecuritySafeCritical]
        public static void RegisterOwinMvcDependencyResolver(this AutofacDependencyResolver dependencyResolver, IContainer rootContainer)
        {
            //  Register the dependency resolver back into the root container
            var updateBuilder = new ContainerBuilder();
            updateBuilder.RegisterInstance(dependencyResolver)
                         .AsSelf()
                         .AsImplementedInterfaces()
                         .SingleInstance();
            updateBuilder.Update(rootContainer);
        }
    }
}
