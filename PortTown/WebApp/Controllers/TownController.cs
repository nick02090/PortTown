using Domain;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApp.Models.Buildings;
using WebApp.Utils;

namespace WebApp.Controllers
{
    public class TownController : Controller
    {
        //Hosted web API REST Service base url
        readonly string Baseurl = "https://localhost:44366/";

        public async Task<ActionResult> Index()
        {
            Town town = new Town();

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
                    town = JsonConvert.DeserializeObject<Town>(responseResult);
                }
                //returning the town info to view  
                return View(town);
            }
        }

        [ChildActionOnly]
        public ActionResult Buildings()
        {
            Buildings buildings = new Buildings();
            buildings.BuildingsList.Add(new Building("Farm", 1, BuildingsInfo.FARM_INFO));
            buildings.BuildingsList.Add(new Building("Workshop", 1, BuildingsInfo.WORKSHOP_INFO));
            buildings.BuildingsList.Add(new Building("Gold mine", 1, BuildingsInfo.GOLD_MINE_INFO));
            buildings.BuildingsList.Add(new Building("Silo", 1, BuildingsInfo.SILO_INFO));
            buildings.BuildingsList.Add(new Building("Warehouse", 1, BuildingsInfo.WAREHOUSE_INFO));
            buildings.BuildingsList.Add(new Building("Quarry", 1, BuildingsInfo.QUARRY_INFO));
            buildings.BuildingsList.Add(new Building("Coal mine", 1, BuildingsInfo.COAL_MINE));
            buildings.BuildingsList.Add(new Building("Iron mine", 1, BuildingsInfo.IRON_MINE));
            buildings.BuildingsList.Add(new Building("Sawmill", 1, BuildingsInfo.SAMWILL_INFO));
            buildings.BuildingsList.Add(new Building("Palace", 1, BuildingsInfo.PALACE_INFO));
            return PartialView("Buildings", buildings);
        }

        public ActionResult Build()
        {
            Buildings buildings = new Buildings();
            buildings.BuildingsList.Add(new Building("Farm", 1, BuildingsInfo.FARM_INFO));
            buildings.BuildingsList.Add(new Building("Silo", 1, BuildingsInfo.SILO_INFO));
            buildings.BuildingsList.Add(new Building("Sawmill", 1, BuildingsInfo.SAMWILL_INFO));
            return View("Build", buildings);
        }
    }
}