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
            Town town = null;
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
                    town = new Town(townID, townName, townLevel);

                    HttpResponseMessage buildingsHttpResponse = await client.GetAsync($"api/building/town/{townID}");
                    if(buildingsHttpResponse.IsSuccessStatusCode)
                    {
                        string builingsResponseResult = buildingsHttpResponse.Content.ReadAsStringAsync().Result;
                        JArray Buildings = JsonConvert.DeserializeObject<JArray>(builingsResponseResult);
                        foreach (var building in Buildings)
                        {
                            Dictionary<string, object> buildingDict = building.ToObject<Dictionary<string, object>>();
                            int buildingType = Convert.ToInt32((long)buildingDict["BuildingType"]);
                            Guid buildingID = Guid.Parse((string)buildingDict["Id"]);
                            string buildingName = (string)buildingDict["Name"];
                            int buildingLevel = Convert.ToInt32((long)buildingDict["Level"]);
                            string imgPath = "~/Content/img/" + buildingName + ".jpg";
                            string childBuilding = buildingType == 0 ? "ChildProductionBuilding" : "ChildStorage";
                            Dictionary<string, object> childDict = ((JObject)buildingDict[childBuilding]).ToObject<Dictionary<string, object>>();
                            if(buildingType == 1)
                            {
                                JArray resources = (JArray)childDict["StoredResources"];
                                List<Resource> StoredResources = new List<Resource>();
                                foreach(var res in resources)
                                {
                                    var resDict = res.ToObject<Dictionary<string, object>>();
                                    Resource r = new Resource((ResourceType)((int)((long)resDict["ResourceType"])), Convert.ToInt32((long)resDict["Quantity"]));
                                    StoredResources.Add(r);
                                }
                                StorageBuilding sb = new StorageBuilding(buildingID, buildingName, buildingLevel, "INFO", imgPath, StoredResources);
                                town.Buildings.Add(sb);
                            } else
                            {

                            }

                        }
                    }
                    
                }
                //returning the town info to view
                return View("Index", town);
            }
        }

        [ChildActionOnly]
        public ActionResult ProductionBuildings(Town town)
        {
            TownBuildingsViewModel buildings = new TownBuildingsViewModel();

            buildings.BuildingsList = town.Buildings;
            
            return PartialView("ProductionBuildings", buildings);
        }

        [ChildActionOnly]
        public ActionResult StorageBuildings(Town town)
        {
            TownBuildingsViewModel buildings = new TownBuildingsViewModel();

            buildings.BuildingsList = town.Buildings;

            return PartialView("StorageBuildings", buildings);
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

        public async Task<ActionResult> StorageBuildingDetails(Guid id)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync($"api/building/{id}");
                var responseResult = response.Content.ReadAsStringAsync().Result;
                StorageBuilding pb = JsonConvert.DeserializeObject<StorageBuilding>(responseResult);

                var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseResult);
                Dictionary<string, object> childDict = ((JObject)dict["ChildStorage"]).ToObject<Dictionary<string, object>>();
                JArray resources = (JArray)childDict["StoredResources"];
                List<Resource> StoredResources = new List<Resource>();
                foreach (var res in resources)
                {
                    var resDict = res.ToObject<Dictionary<string, object>>();
                    Resource r = new Resource((ResourceType)((int)((long)resDict["ResourceType"])), Convert.ToInt32((long)resDict["Quantity"]));
                    StoredResources.Add(r);
                }
                pb.StoredResources = StoredResources;

                return View(pb);
            }
        }

        [ChildActionOnly]
        public ActionResult Items()
        {
            List<Resource> FoodStored = new List<Resource>
            {
                new Resource(ResourceType.Food, 420),
                new Resource(ResourceType.Coal, 69)
            };
            TownItemsViewModel items = new TownItemsViewModel();
            items.Items.Add(new Models.Items.Item(Models.Items.ItemType.JEWELRY, 5, FoodStored));
            items.Items.Add(new Models.Items.Item(Models.Items.ItemType.POTTERY, 10, FoodStored));
            return PartialView("Items", items);
        }

        public ActionResult Build()
        {
            TownBuildingsViewModel buildings = new TownBuildingsViewModel();
            buildings.BuildingsList.Add(new ProductionBuilding(Guid.NewGuid(), "Farm", 1, "~/Content/img/Farm.jpg", BuildingsInfo.FARM_INFO, new Resource(ResourceType.Food)));
            // buildings.BuildingsList.Add(new ProductionBuilding("Silo", 1, BuildingsInfo.SILO_INFO));
            buildings.BuildingsList.Add(new ProductionBuilding(Guid.NewGuid(), "Sawmill", 1,"~/Content/img/Farm.jpg", BuildingsInfo.SAMWILL_INFO, new Resource(ResourceType.Wood)));
            return View("Build", buildings);
        }
    }
}