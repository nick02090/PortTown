using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    public class CraftableController : ApiController
    {
        private readonly ICraftableRepository _repository;

        public CraftableController(ICraftableRepository repository)
        {
            _repository = repository;
        }

        // GET api/<controller>
        public async Task<IEnumerable<Craftable>> GetAsync()
        {
            return await _repository.GetAsync();
        }

        // GET api/<controller>/5
        public async Task<Craftable> GetAsync(Guid id)
        {
            return await _repository.GetAsync(id);
        }

        // POST api/<controller>
        public async Task<Craftable> CreateAsync([FromBody] Craftable entity)
        {
            return await _repository.CreateAsync(entity);
        }

        // PUT api/<controller>/5
        public async Task<Craftable> UpdateAsync(Guid id, [FromBody] Craftable entity)
        {
            var entitydb = await _repository.GetAsync(id);

            entitydb.TimeToBuild = entity.TimeToBuild;
            entitydb.TimeUntilCrafted = entity.TimeUntilCrafted;
            entitydb.CraftableType = entity.CraftableType;
            entitydb.IsFinishedCrafting = entity.IsFinishedCrafting;

            return await _repository.UpdateAsync(entitydb);
        }

        // DELETE api/<controller>/5
        public async Task DeleteAsync(Guid id)
        {
            var entityDel = await _repository.GetForDeleteAsync(id);
            await _repository.DeleteAsync(entityDel);
            return;
        }
    }
}