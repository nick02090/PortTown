using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.Models.Buildings;
using WebApp.Models.Resources;
using WebApp.Models.TownViewModels;
using WebApp.Utils;
using System.Diagnostics;

namespace WebApp.Controllers
{
    public class TownController : Controller
    {
        //Hosted web API REST Service base url
        readonly string Baseurl = "https://localhost:44366/";
        private Town Town;

        public async Task<ActionResult> Index()
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
                HttpResponseMessage response = await client.GetAsync("api/town");

                //Checking the response is successful or not which is sent using HttpClient  
                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var responseResult = response.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Town  
                    Town = JsonConvert.DeserializeObject<Town>(responseResult);
                }
                //returning the town info to view
                return View(Town);
            }
        }

        [ChildActionOnly]
        public ActionResult ProductionBuildings()
        {
            TownBuildingsViewModel buildings = new TownBuildingsViewModel();

            buildings.BuildingsList.Add(new ProductionBuilding("Quarry", 1, BuildingsInfo.QUARRY_INFO, "~/Content/img/Quarry.jpg", new Resource(ResourceType.STONE)));
            buildings.BuildingsList.Add(new ProductionBuilding("Coal mine", 1, BuildingsInfo.COAL_MINE, "~/Content/img/Coal mine.jpg", new Resource(ResourceType.COAL)));
            buildings.BuildingsList.Add(new ProductionBuilding("Iron mine", 1, BuildingsInfo.IRON_MINE, "~/Content/img/Gold mine.jpg", new Resource(ResourceType.IRON)));
            buildings.BuildingsList.Add(new ProductionBuilding("Sawmill", 1, BuildingsInfo.SAMWILL_INFO, "~/Content/img/Sawmill.jpg", new Resource(ResourceType.WOOD)));
            
            return PartialView("ProductionBuildings", buildings);
        }

        [ChildActionOnly]
        public ActionResult StorageBuildings()
        {
            TownBuildingsViewModel buidlings = new TownBuildingsViewModel();
            List<Resource> FoodStored = new List<Resource>
            {
                new Resource(ResourceType.FOOD, 420),
                new Resource(ResourceType.COAL, 69)
            };
            buidlings.BuildingsList.Add(new StorageBuilding("Warehouse", 1, BuildingsInfo.FARM_INFO, "~/Content/img/Farm.jpg", FoodStored));
            return PartialView("StorageBuildings", buidlings);
        }

        public ActionResult ProductionBuildingDetails(string Name)
        {
            return View(new ProductionBuilding(Name, 0, BuildingsInfo.NameToInfo[Name], "~/Content/img/Farm.jpg", new Resource(ResourceType.FOOD)));
        }

        public ActionResult StorageBuildingDetails(string Name)
        {
            List<Resource> FoodStored = new List<Resource>
            {
                new Resource(ResourceType.FOOD, 420),
                new Resource(ResourceType.COAL, 69)
            };
            return View(new StorageBuilding(Name, 0, BuildingsInfo.NameToInfo[Name], "~/Content/img/Farm.jpg", FoodStored));
        }

        [ChildActionOnly]
        public ActionResult Items()
        {
            List<Resource> FoodStored = new List<Resource>
            {
                new Resource(ResourceType.FOOD, 420),
                new Resource(ResourceType.COAL, 69)
            };
            TownItemsViewModel items = new TownItemsViewModel();
            items.Items.Add(new Models.Items.Item(Models.Items.ItemType.JEWELRY, 5, FoodStored));
            items.Items.Add(new Models.Items.Item(Models.Items.ItemType.POTTERY, 10, FoodStored));
            return PartialView("Items", items);
        }

        public ActionResult Build()
        {
            TownBuildingsViewModel buildings = new TownBuildingsViewModel();
            buildings.BuildingsList.Add(new ProductionBuilding("Farm", 1, "~/Content/img/Farm.jpg", BuildingsInfo.FARM_INFO, new Resource(ResourceType.FOOD)));
            // buildings.BuildingsList.Add(new ProductionBuilding("Silo", 1, BuildingsInfo.SILO_INFO));
            buildings.BuildingsList.Add(new ProductionBuilding("Sawmill", 1,"~/Content/img/Farm.jpg", BuildingsInfo.SAMWILL_INFO, new Resource(ResourceType.WOOD)));
            return View("Build", buildings);
        }
    }
}