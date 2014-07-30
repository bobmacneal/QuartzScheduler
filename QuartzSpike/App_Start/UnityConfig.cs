using System.Web.Http;
using Microsoft.Practices.Unity;
using QuartzSpike.Ioc;

namespace QuartzSpike
{
    /// <summary>
    ///     UnityConfig initializes the Unity container and registers the DependencyResolver with the framework. This is where
    ///     we register our dependencies using interfaces that are linked to concrete implementations
    /// </summary>
    public static class UnityConfig
    {
        /// <summary>
        ///     RegisterComponents is where interfaces get wired up to implementations and where the global Dependency Resolver
        ///     gets set
        /// </summary>
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            RWS.Jobs.Config.UnityConfig.RegisterComponents(container);

            QuartzScheduler.InitializeQuartzJobs(container);


            //Wire up Unity in the global configuration of Web Api
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}