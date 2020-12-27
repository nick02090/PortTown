using Domain;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class TownController : ApiController
    {
        // GET api/<controller>
        public Town Get()
        {
            return new Town()
            {
                Name = "Spajicgrad",
                Level = 5
            };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}