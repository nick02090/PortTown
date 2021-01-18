using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    public class BuildingController : ApiController
    {

        private readonly IBuildingRepository _repository;

        public BuildingController(IBuildingRepository repository)
        {
            _repository = repository;
        }


        // GET api/<controller>
        public async Task<IEnumerable<Building>> GetAsync()
        {
            return await _repository.GetAsync();
        }

        // GET api/<controller>/5
        public async Task<Building> GetAsync(Guid id)
        {
            return await _repository.GetAsync(id);
        }

        // POST api/<controller>
        public async Task<Building> CreateAsync([FromBody] Building entity)
        {
            return await _repository.CreateAsync(entity);
        }

        // PUT api/<controller>/5
        public async Task<Building> UpdateAsync(Guid id, [FromBody] Building entity)
        {
            var entitydb = await _repository.GetAsync(id);

            entitydb.Name = entity.Name;
            entitydb.Level = entity.Level;
            entitydb.Capacity = entity.Capacity;
            entitydb.BuildingType = entity.BuildingType;

            return await _repository.UpdateAsync(entitydb);
        }

        // DELETE api/<controller>/5
        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
            return;
        }
    }
}