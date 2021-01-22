using Domain;
using System;
using System.Collections.Generic;
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
        public async Task<IEnumerable<Town>> GetAsync()
        {
            return await _repository.GetAsync();
        }

        // GET api/<controller>/5
        [HttpGet]
        public async Task<Town> GetAsync(Guid id)
        {
            return await _repository.GetAsync(id);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<Town> CreateAsync([FromBody] Town entity)
        {
            return await _repository.CreateAsync(entity);
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<Town> UpdateAsync(Guid id, [FromBody] Town entity)
        {
            var entitydb = await _repository.GetAsync(id);

            entitydb.Name = entity.Name;
            entitydb.Level = entity.Level;

            return await _repository.UpdateAsync(entitydb);
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
            return;
        }

        [Route("api/town/reset/{id}")]
        [HttpPost]
        public async Task ResetAsync([FromUri] Guid id)
        {
            await _service.ResetAsync(id);
            return;
        }
    }
}