using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;

namespace QuartzSpike.Ioc
{
    /// <summary>
    ///     Inherits an implementation of IDepedencyScope and implements IDependencyResolver
    /// </summary>
    public class UnityDependencyResolver : UnityDependencyScope, IDependencyResolver
    {
        /// <summary>
        ///     Constructor that injects the Unity IOC container
        /// </summary>
        /// <param name="container"></param>
        public UnityDependencyResolver(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Begin Scope
        /// </summary>
        /// <returns>IDependencyScope</returns>
        public IDependencyScope BeginScope()
        {
            IUnityContainer childContainer = Container.CreateChildContainer();

            return new UnityDependencyScope(childContainer);
        }
    }
}