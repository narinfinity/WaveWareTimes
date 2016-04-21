using Microsoft.Practices.Unity.Mvc;
using System.Linq;
using System.Web.Mvc;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WaveWareTimes.Infrastructure.DependencyResolution.App_Start.UnityWebActivator), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(WaveWareTimes.Infrastructure.DependencyResolution.App_Start.UnityWebActivator), "Shutdown")]

namespace WaveWareTimes.Infrastructure.DependencyResolution.App_Start
{
    public static class UnityWebActivator
    {
        /// <summary>Integrates Unity when the application starts.</summary>
        public static void Start()
        {
            var container = UnityConfig.GetConfiguredContainer();

            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(container));


            //Set Resolver for ASP.NET MVC and WebAPI
            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);

            // TODO: Uncomment if you want to use PerRequestLifetimeManager
            Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));
        }

        /// <summary>Disposes the Unity container when the application is shut down.</summary>
        public static void Shutdown()
        {
            var container = UnityConfig.GetConfiguredContainer();
            container.Dispose();
        }
    }
}
