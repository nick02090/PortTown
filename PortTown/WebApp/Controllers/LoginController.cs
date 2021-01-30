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

namespace WebApp.Controllers
{
    public class LoginController : Controller
    {
        readonly string Baseurl = "https://localhost:44366/api/user/";

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> LoginUser(User user)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var userJSON = JsonConvert.SerializeObject(user);
                var userStringContent = new StringContent(userJSON, Encoding.UTF8, "application/json");
                var loginResponse = await client.PostAsync("authenticate/", userStringContent);
                if (loginResponse.IsSuccessStatusCode)
                {
                    var resultResponse = loginResponse.Content.ReadAsStringAsync().Result;
                    var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(resultResponse);
                    Dictionary<string, object> townDict = ((JObject)dict["Town"]).ToObject<Dictionary<string, object>>();
                    Guid townId = Guid.Parse((string)townDict["Id"]);
                    Session["userId"] = Guid.Parse((string)dict["Id"]);
                    Session["townId"] = townId;
                    return RedirectToAction("Index", "Town", new { townID = townId });
                }
            }
            return View("Index");
        }
    }
}