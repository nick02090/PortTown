using System.Web.Http;
using Unity;
using WebAPI.Interfaces;
using WebAPI.Repositories;
using WebAPI.Resolver;
using WebAPI.Services;
using WebAPI.Settings;

namespace WebAPI.Configuration
{
    public static class WebAPIConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            #region Repositories
            container.RegisterType<ITownRepository, TownRepository>();
            container.RegisterType<ICraftableRepository, CraftableRepository>();
            container.RegisterType<IBuildingRepository, BuildingRepository>();
            container.RegisterType<IItemRepository, ItemRepository>();
            container.RegisterType<IProductionBuildingRepository, ProductionBuildingRepository>();
            container.RegisterType<IResourceBatchRepository, ResourceBatchRepository>();
            container.RegisterType<IStorageRepository, StorageRespository>();
            container.RegisterType<IUserRepository, UserRespository>();
            #endregion
            #region Services
            container.RegisterType<IUserService, UserService>();
            #endregion
            #region Settings
            container.RegisterSingleton<IAppSettings, AppSettings>();
            #endregion
            config.DependencyResolver = new UnityResolver(container);

            // JSON formatting
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings
                .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters
                .Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}