using Microsoft.Practices.Unity;

namespace RWS.Repositories.Config
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
            container
                .RegisterType<IOrderRequestRespository, OrderRequestRespository>()
                .RegisterType<IM3OrderRespository, M3OrderRespository>();
        }
    }
}