using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
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
            container.RegisterType<IUpgradeableRepository, UpgradeableRepository>();
            container.RegisterType<ISellableRepository, SellableRepository>();
            #endregion
            #region Services
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<ITownService, TownService>();
            container.RegisterType<IBuildingService, BuildingService>();
            container.RegisterType<IItemService, ItemService>();
            container.RegisterType<IUpgradeableService, UpgradeableService>();
            container.RegisterType<ICraftableService, CraftableService>();
            container.RegisterType<IProductionBuildingService, ProductionBuildingService>();
            #endregion
            #region Settings
            container.RegisterSingleton<IAppSettings, AppSettings>();
            #endregion
            config.DependencyResolver = new UnityResolver(container);

            // JSON formatting
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings
                .ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
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

        private static readonly string Baseurl = "https://localhost:44366/";
        public static async Task<bool> CheckForInitialBuildingsAsync()
        {
            bool hasData = false;

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                // Clear any previously defined headers
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage response = await client.GetAsync("api/building/check-initial-template-data");

                //Checking the response is successful or not which is sent using HttpClient  
                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var responseResult = response.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the User  
                    dynamic result = JsonConvert.DeserializeObject<dynamic>(responseResult);
                    hasData = result.HasData;
                }
                //returning the user info  
                return hasData;
            }
        }

        public static async Task<bool> CheckForInitialItemsAsync()
        {
            bool hasData = false;

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                // Clear any previously defined headers
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage response = await client.GetAsync("api/item/check-initial-template-data");

                //Checking the response is successful or not which is sent using HttpClient  
                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var responseResult = response.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the User  
                    dynamic result = JsonConvert.DeserializeObject<dynamic>(responseResult);
                    hasData = result.HasData;
                }
                //returning the user info  
                return hasData;
            }
        }

        public static async Task CreateInitialBuildingsAsync()
        {
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                // Clear any previously defined headers
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage response = await client.PostAsync("api/building/add-initial-template-data", null);

                //Checking the response is successful or not which is sent using HttpClient  
                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var responseResult = response.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the User  
                    dynamic result = JsonConvert.DeserializeObject<dynamic>(responseResult);
                }
                //returning the user info  
                return;
            }
        }

        public static async Task CreateInitialItemsAsync()
        {
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                // Clear any previously defined headers
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage response = await client.PostAsync("api/item/add-initial-template-data", null);

                //Checking the response is successful or not which is sent using HttpClient  
                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var responseResult = response.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the User  
                    dynamic result = JsonConvert.DeserializeObject<dynamic>(responseResult);
                }
                //returning the user info  
                return;
            }
        }
    }
}