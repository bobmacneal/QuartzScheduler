using System.Web.Http;
using Microsoft.Practices.Unity;
using QuartzSpike.Ioc;

namespace QuartzSpike
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            Jobs.Config.UnityConfig.RegisterComponents(container);
            Services.Config.UnityConfig.RegisterComponents(container);
            QuartzScheduler.InitializeQuartzJobs(container);
            //Wire up Unity in the global configuration of Web Api
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}