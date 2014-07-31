using Microsoft.Practices.Unity;

namespace Repositories.Config
{
    public static class UnityConfig
    {
        public static void RegisterComponents(IUnityContainer container)
        {
            container
                .RegisterType<IOrderRequestRespository, OrderRequestRespository>()
                .RegisterType<IErpOrderRespository, ErpOrderRespository>();
        }
    }
}