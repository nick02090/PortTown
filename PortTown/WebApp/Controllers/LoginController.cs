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

                var emailJSON = JsonConvert.SerializeObject(user);
                var stringContent = new StringContent(emailJSON, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("check-availability", stringContent);
                string responseResult = response.Content.ReadAsStringAsync().Result;
                var isAvailable = JObject.Parse(responseResult);
                if (isAvailable.Property("Availability").Value.ToObject<bool>())
                {
                    return View("~/Views/Town/Index.cshtml");
                }
            }
            return View("Index");
        }
    }
}