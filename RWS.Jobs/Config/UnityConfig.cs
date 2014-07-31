﻿using Microsoft.Practices.Unity;

namespace Jobs.Config
{
    public static class UnityConfig
    {
        public static void RegisterComponents(IUnityContainer container)
        {
            Repositories.Config.UnityConfig.RegisterComponents(container);
        }
    }
}