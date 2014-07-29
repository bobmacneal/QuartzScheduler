using System;
using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;

namespace QuartzSpike.Ioc
{
    /// <summary>
    ///     Implementation of IDependencyScope
    /// </summary>
    public class UnityDependencyScope : IDependencyScope
    {
        /// <summary>
        ///     Constructor that injects the Unity IOC container
        /// </summary>
        /// <param name="container"></param>
        public UnityDependencyScope(IUnityContainer container)
        {
            Container = container;
        }

        /// <summary>
        ///     Unity Containter
        /// </summary>
        protected IUnityContainer Container { get; private set; }

        /// <summary>
        ///     Gest a service by service type
        /// </summary>
        /// <param name="serviceType">Service Type</param>
        /// <returns>Object matching service type</returns>
        public object GetService(Type serviceType)
        {
            if (typeof (IHttpController).IsAssignableFrom(serviceType))
            {
                return Container.Resolve(serviceType);
            }

            try
            {
                return Container.Resolve(serviceType);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///     Gets IEnumerable list of services by service type
        /// </summary>
        /// <param name="serviceType">Service Type</param>
        /// <returns>IEnumerable list of objects</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Container.ResolveAll(serviceType);
        }

        /// <summary>
        ///     Dispose of object
        /// </summary>
        public void Dispose()
        {
            Container.Dispose();
        }
    }
}