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
                    Session["townLevel"] = townLevel;
                    town = new Town(townID, townName, townLevel);
                    town.CanUpgrade = (bool)dict["CanUpgrade"];

                    var upgradeDict = ((JObject)dict["Upgradeable"]).ToObject<Dictionary<string, object>>();
                    if(upgradeDict["TimeUntilUpgraded"] != null)
                    {
                        town.IsUpgrading = true;
                        town.TimeToUpgrade = (DateTime)upgradeDict["TimeUntilUpgraded"];
                    }
                    town.IsFinishUpgrade = (bool)upgradeDict["IsFinishedUpgrading"];

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
                            Building b = null;
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
                                b = new StorageBuilding(buildingID, buildingName, buildingLevel, "INFO", imgPath, false, StoredResources);
                            } else
                            {
                                Resource r = new Resource((ResourceType)((int)((long)childDict["ResourceProduced"])), 69);
                                b = new ProductionBuilding(buildingID, buildingName, buildingLevel, "INFO", imgPath, false, r);
                            }
                            Dictionary<string, object> craftDict = ((JObject)buildingDict["ParentCraftable"]).ToObject<Dictionary<string, object>>();
                            bool isFinishedCrafting = (bool)craftDict["IsFinishedCrafting"];
                            Dictionary<string, object> upgDict = ((JObject)buildingDict["Upgradeable"]).ToObject<Dictionary<string, object>>();
                            bool isFinishedUpg = (bool)upgDict["IsFinishedUpgrading"];
                            if (isFinishedUpg || upgDict["TimeUntilUpgraded"] != null)
                            {
                                DateTime time = new DateTime();
                                if (upgDict["TimeUntilUpgraded"] != null)
                                {
                                    time = (DateTime)upgDict["TimeUntilUpgraded"];
                                }
                                town.UpgradeingBuildings.Add(new CraftingBuilding(b, time, isFinishedUpg));
                            } else if (isFinishedCrafting || craftDict["TimeUntilCrafted"] != null)
                            {
                                DateTime time = new DateTime();
                                if (craftDict["TimeUntilCrafted"] != null)
                                {
                                    time = (DateTime)craftDict["TimeUntilCrafted"];
                                }
                                town.CraftingBuildings.Add(new CraftingBuilding(b, time, isFinishedCrafting));

                            }
                            else
                            {
                                town.Buildings.Add(b);
                            }
                        }
                    }
                }
                //returning the town info to view
                return View("Index", town);
            }
        }

        public async Task<ActionResult> FinishCrafting(Guid id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsync($"api/building/craft/{id}", null);
                return RedirectToAction("Index", new { townID = Session["townId"] });
            }
        }

        public async Task<ActionResult> FinishUpgradeing(Guid id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsync($"api/building/upgrade/{id}", null);
                return RedirectToAction("Index", new { townID = Session["townId"] });
            }
        }

        public async Task<ActionResult> FinishUpgradeTown(Guid id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsync($"api/town/upgrade/{id}", null);
                return RedirectToAction("Index", new { townID = Session["townId"] });
            }
        }

        public async Task<ActionResult> UpgradeTown(Guid id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsync($"api/town/start-upgrade/{id}", null);
                return RedirectToAction("Index", new { townID = Session["townId"] });
            }
        }

        public async Task<ActionResult> UpgradeBuilding(Guid id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsync($"api/building/start-upgrade/{id}", null);
                return RedirectToAction("Index", new { townID = Session["townId"] });
            }
        }

        public async Task<ActionResult> BuildBuilding(Guid id)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsync($"api/building/start-craft/{Session["townId"]}/{id}", null);
                return RedirectToAction("Index", new { townID = Session["townId"] });
            }
        }

        [ChildActionOnly]
        public ActionResult UpgradingBuildings(Town town)
        {
            TownBuildingsViewModel buildings = new TownBuildingsViewModel();

            buildings.UpgradingBuildings = town.UpgradeingBuildings;

            return PartialView("UpgradingBuildings", buildings);
        }

        [ChildActionOnly]
        public ActionResult CraftingBuildings(Town town)
        {
            TownBuildingsViewModel buildings = new TownBuildingsViewModel();

            buildings.CraftingBuildings = town.CraftingBuildings;

            return PartialView("CraftingBuildings", buildings);
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

        public async Task<ActionResult> Harvest(Guid id)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync($"/api/building/production/harvest/{id}");
                return RedirectToAction("Index", new { townID = Session["townId"] });
            }
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

                var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseResult);
                Dictionary<string, object> childDict = ((JObject)dict["ChildProductionBuilding"]).ToObject<Dictionary<string, object>>();
                Resource maxR = new Resource((ResourceType)((int)((long)childDict["ResourceProduced"])), (int)((long)dict["Capacity"]));
                Resource r = new Resource((ResourceType)((int)((long)childDict["ResourceProduced"])), (int)((long)dict["AccumulatedResources"]));
                pb.MaxResource = maxR;
                pb.Resource = r;
                pb.Info = (string)dict["UpgradeMessage"];
                pb.CanUpgrade = (bool)dict["CanUpgrade"];
                pb.CanHarvest = (bool)dict["CanHarvest"];
                pb.HarvestMessage = (string)dict["HarvestMessage"];


                Dictionary<string, object> craftDict = ((JObject)dict["Upgradeable"]).ToObject<Dictionary<string, object>>();
                if (craftDict["TimeUntilUpgraded"] != null)
                {
                    DateTime time = (DateTime)craftDict["TimeUntilCrafted"];
                    pb.TimeUntilUpgraded = time;
                    pb.IsUpgradeing = true;
                }
                return View(pb);
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
                int capacity = (int)(long)dict["Capacity"];
                pb.Capacity = capacity;
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
                pb.Info = (string)dict["UpgradeMessage"];
                pb.CanUpgrade = (bool)dict["CanUpgrade"];

                Dictionary<string, object> craftDict = ((JObject)dict["Upgradeable"]).ToObject<Dictionary<string, object>>();
                if (craftDict["TimeUntilUpgraded"] != null)
                {
                    DateTime time = (DateTime)craftDict["TimeUntilCrafted"];
                    pb.TimeUntilUpgraded = time;
                    pb.IsUpgradeing = true;
                }

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

        public async Task<ActionResult> Build()
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync($"api/building/template/{Session["townId"]}");
                var responseResult = response.Content.ReadAsStringAsync().Result;
                JArray craftablesJSON = JsonConvert.DeserializeObject<JArray>(responseResult);
                List<Craftable> craftables = new List<Craftable>();
                foreach(var craftable in craftablesJSON)
                {
                    var craftDict = craftable.ToObject<Dictionary<string, object>>();
                    bool CanCraft = (bool)craftDict["CanCraft"];
                    string Name = (string)craftDict["Name"];
                    Guid id = Guid.Parse((string)craftDict["TemplateId"]);
                    string imgPath = "~/Content/img/" + Name + ".jpg";
                    int level = 0;
                    int type = ((int)((long)craftDict["BuildingType"]));
                    Building b;
                    if (type == 0) b = new ProductionBuilding(id, Name, level, "INFO", imgPath, CanCraft, null);
                    else b = new StorageBuilding(id, Name, level, "INFO", imgPath, CanCraft, null);
                    Craftable c = new Craftable(CanCraft, b);
                    JArray resourcesJSON = (JArray)craftDict["RequiredResources"];
                    List<Resource> resources = new List<Resource>();
                    foreach(var res in resourcesJSON)
                    {
                        var resDict = res.ToObject<Dictionary<string, object>>();
                        Resource r = new Resource((ResourceType)((int)((long)resDict["ResourceType"])), Convert.ToInt32((long)resDict["Quantity"]));
                        resources.Add(r);
                    }
                    c.RequiredResources = resources;
                    craftables.Add(c);
                }
                BuildViewModel model = new BuildViewModel();
                model.Craftables = craftables;

                return View("Build", model);
            }
        }
    }
}