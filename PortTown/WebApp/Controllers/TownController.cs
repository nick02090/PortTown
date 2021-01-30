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
using Newtonsoft.Json.Linq;

namespace WebApp.Controllers
{
    public class TownController : Controller
    {
        //Hosted web API REST Service base url
        readonly string Baseurl = "https://localhost:44366/";

        public async Task<ActionResult> Index(Guid townID)
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
                HttpResponseMessage response = await client.GetAsync($"api/town/{townID}");

                //Checking the response is successful or not which is sent using HttpClient  
                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var responseResult = response.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Town  
                    var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseResult);
                    string townName = (string)dict["Name"];
                    int townLevel = Convert.ToInt32((long)dict["Level"]);
                    Town town = new Town(townID, townName, townLevel);

                    HttpResponseMessage buildingsHttpResponse = await client.GetAsync($"api/builings/town/{townID}");
                    if(buildingsHttpResponse.IsSuccessStatusCode)
                    {
                        var builingsResponseResult = buildingsHttpResponse.Content.ReadAsStringAsync().Result;
                        var buildingsJSONDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(builingsResponseResult);
                        var Buildings = ((Newtonsoft.Json.Linq.JArray)buildingsJSONDict["Buildings"]);
                        foreach (var building in Buildings)
                        {
                            Dictionary<string, object> buildingDict = building.ToObject<Dictionary<string, object>>();
                            int buildingType = Convert.ToInt32((long)buildingDict["BuildingType"]);
                            Guid buildingID = Guid.Parse((string)buildingDict["Id"]);
                            string buildingName = (string)buildingDict["Name"];
                            int buildingLevel = Convert.ToInt32((long)buildingDict["Level"]);
                            string imgPath = "~/Content/img/" + buildingName + ".jpg";
                            //Building b = buildingType == 0 ? new ProductionBuilding(buildingID, buildingLevel, BuildingsInfo.NameToInfo[buildingName])
                        }
                    }
                    
                }
                //returning the town info to view
                return View();
            }
        }

        [ChildActionOnly]
        public ActionResult ProductionBuildings(Town town)
        {
            TownBuildingsViewModel buildings = new TownBuildingsViewModel();

            buildings.BuildingsList.Add(new ProductionBuilding(Guid.NewGuid(), "Quarry", 1, BuildingsInfo.QUARRY_INFO, "~/Content/img/Quarry.jpg", new Resource(ResourceType.STONE)));
            buildings.BuildingsList.Add(new ProductionBuilding(Guid.NewGuid(), "Coal mine", 1, BuildingsInfo.COAL_MINE, "~/Content/img/Coal mine.jpg", new Resource(ResourceType.COAL)));
            buildings.BuildingsList.Add(new ProductionBuilding(Guid.NewGuid(), "Iron mine", 1, BuildingsInfo.IRON_MINE, "~/Content/img/Gold mine.jpg", new Resource(ResourceType.IRON)));
            buildings.BuildingsList.Add(new ProductionBuilding(Guid.NewGuid(), "Sawmill", 1, BuildingsInfo.SAMWILL_INFO, "~/Content/img/Sawmill.jpg", new Resource(ResourceType.WOOD)));
            
            return PartialView("ProductionBuildings", buildings);
        }

        [ChildActionOnly]
        public ActionResult StorageBuildings(Town town)
        {
            TownBuildingsViewModel buidlings = new TownBuildingsViewModel();
            List<Resource> FoodStored = new List<Resource>
            {
                new Resource(ResourceType.FOOD, 420),
                new Resource(ResourceType.COAL, 69)
            };
            buidlings.BuildingsList.Add(new StorageBuilding(Guid.NewGuid(), "Warehouse", 1, BuildingsInfo.FARM_INFO, "~/Content/img/Farm.jpg", FoodStored));
            return PartialView("StorageBuildings", buidlings);
        }

        public async Task<ActionResult> ProductionBuildingDetails(Guid id)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync($"api/building/{id}");
                var responseResult = response.Content.ReadAsStringAsync().Result;
                ProductionBuilding pb = JsonConvert.DeserializeObject<ProductionBuilding>(responseResult);
                return View();
            }
        }

        public ActionResult StorageBuildingDetails(Guid id)
        {
            List<Resource> FoodStored = new List<Resource>
            {
                new Resource(ResourceType.FOOD, 420),
                new Resource(ResourceType.COAL, 69)
            };
            return View();
            //return View(new StorageBuilding(Guid.NewGuid(), Name, 0, BuildingsInfo.NameToInfo[Name], "~/Content/img/Farm.jpg", FoodStored));
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
            buildings.BuildingsList.Add(new ProductionBuilding(Guid.NewGuid(), "Farm", 1, "~/Content/img/Farm.jpg", BuildingsInfo.FARM_INFO, new Resource(ResourceType.FOOD)));
            // buildings.BuildingsList.Add(new ProductionBuilding("Silo", 1, BuildingsInfo.SILO_INFO));
            buildings.BuildingsList.Add(new ProductionBuilding(Guid.NewGuid(), "Sawmill", 1,"~/Content/img/Farm.jpg", BuildingsInfo.SAMWILL_INFO, new Resource(ResourceType.WOOD)));
            return View("Build", buildings);
        }
    }
}