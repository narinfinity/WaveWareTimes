using Microsoft.Practices.Unity;
using Microsoft.Owin.Security;
using WaveWareTimes.Infrastructure.Data;
using System.Web;
using System;
using WaveWareTimes.Core.Entities.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WaveWareTimes.Core.Interfaces.Common;
using WaveWareTimes.Core.Interfaces.Service.Domain;
using WaveWareTimes.Infrastructure.Data.DbContext;
using WaveWareTimes.Service.Domain;

namespace WaveWareTimes.Infrastructure.DependencyResolution
{
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IAuthenticationManager>(new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));
            container.RegisterType<IUserStore<User>, UserStore<User>>(new HierarchicalLifetimeManager(), new InjectionConstructor(new AppDbContext()));

            //PerRequestLifetime context registration
            container.RegisterType<IDataContext, DataContext>(new PerRequestLifetimeManager());

            //Unit of work Repository registration
            //container.RegisterType(typeof(IRepository<,>), typeof(Repository<,>), new PerRequestLifetimeManager());
            //container.RegisterType<IWorkTimeRecordRepository, WorkTimeRecordRepository>(new PerRequestLifetimeManager());

            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager());

            //Services
            container.RegisterType<IWorkTimeRecordService, WorkTimeRecordService>(new PerRequestLifetimeManager());

        }
    }
}