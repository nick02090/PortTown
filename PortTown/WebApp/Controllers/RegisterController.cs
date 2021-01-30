using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.Utilities;

namespace WebApp.Controllers
{
    public class RegisterController : Controller
    {
        readonly string Baseurl = "https://localhost:44366/api/user/";

        // GET: Register
        public ActionResult Index()
        {
            User user = new User();
            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> RegisterUser(User user)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var emailJSON = JsonConvert.SerializeObject(user);
                var stringContent = new StringContent(emailJSON, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("check-availability", stringContent);
                string responseResult = response.Content.ReadAsStringAsync().Result;
                var isAvailable = JObject.Parse(responseResult);
                if (isAvailable.Property("Availability").Value.ToObject<bool>())
                {
                    var userJSON = JsonConvert.SerializeObject(user);
                    var userStringContent = new StringContent(userJSON, Encoding.UTF8, "application/json");
                    var registerResponse = await client.PostAsync("register/" + user.Town.Name, userStringContent);
                    if(registerResponse.IsSuccessStatusCode)
                    {
                        var resultResponse = registerResponse.Content.ReadAsStringAsync().Result;
                        var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(resultResponse);
                        Dictionary<string, object> townDict = ((JObject)dict["Town"]).ToObject<Dictionary<string, object>>();
                        Guid townId = Guid.Parse((string)townDict["Id"]);
                        return RedirectToAction("Index", "Town", new { townID = townId });
                    }
                }
            }
            return View("Index");
        }
    }
}
