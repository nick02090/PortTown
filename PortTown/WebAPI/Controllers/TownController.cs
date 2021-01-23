using Domain;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    public class TownController : ApiController
    {
        private readonly ITownRepository _repository;
        private readonly ITownService _service;

        public TownController(ITownRepository repository, ITownService service)
        {
            _repository = repository;
            _service = service;
        }

        // GET api/<controller>
        [HttpGet]
        public async Task<HttpResponseMessage> GetAsync()
        {
            var towns = await _repository.GetAsync();
            return Request.CreateResponse(HttpStatusCode.OK, towns);
        }

        // GET api/<controller>/5
        [HttpGet]
        public async Task<HttpResponseMessage> GetAsync(Guid id)
        {
            var town = await _repository.GetAsync(id);
            return Request.CreateResponse(HttpStatusCode.OK, town);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<HttpResponseMessage> CreateAsync([FromBody] Town entity)
        {
            var town = await _repository.CreateAsync(entity);
            return Request.CreateResponse(HttpStatusCode.Created, town);
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<HttpResponseMessage> UpdateAsync(Guid id, [FromBody] Town entity)
        {
            var entitydb = await _repository.GetAsync(id);

            entitydb.Name = entity.Name;
            entitydb.Level = entity.Level;

            entitydb = await _repository.UpdateAsync(entitydb);
            return Request.CreateResponse(HttpStatusCode.OK, entitydb);
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [Route("api/town/reset/{id}")]
        [HttpPost]
        public async Task<HttpResponseMessage> ResetAsync([FromUri] Guid id)
        {
            await _service.ResetAsync(id);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}