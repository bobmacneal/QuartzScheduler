using Microsoft.Practices.Unity;

namespace Services.Config
{
    public static class UnityConfig
    {
        public static void RegisterComponents(IUnityContainer container)
        {
            Repositories.Config.UnityConfig.RegisterComponents(container);
            container
                .RegisterType<IOrderService, OrderService>();
        }
    }
}