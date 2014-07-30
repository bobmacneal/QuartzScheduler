using Microsoft.Practices.Unity;
using Quartz;

namespace RWS.Jobs.Config
{
    /// <summary>
    ///     Provides a static method that takes in a container and registers the app's services, repositories, etc.
    /// </summary>
    public static class UnityConfig
    {
        /// <summary>
        ///     RegisterComponents is where interfaces get wired up to implementations and registered in the container.
        /// </summary>
        public static void RegisterComponents(IUnityContainer container)
        {
            Repositories.Config.UnityConfig.RegisterComponents(container);
            //container
            //    .RegisterType<IJob, IncomingOrderJob>();

            //var orderRequestRespository = container.Resolve<IOrderRequestRespository>();
            //var m3OrderRespository = container.Resolve<IM3OrderRespository>();
            //var incomingOrderJob = new IncomingOrderJob(orderRequestRespository, m3OrderRespository);
            //container.RegisterInstance<IJob>(incomingOrderJob);
        }
    }
}