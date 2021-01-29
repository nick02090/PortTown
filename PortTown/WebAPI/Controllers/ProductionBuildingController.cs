using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    public class ProductionBuildingController : ApiController
    {
        private readonly IProductionBuildingRepository _repository;

        public ProductionBuildingController(IProductionBuildingRepository repository)
        {
            _repository = repository;
        }

        // GET api/<controller>
        public async Task<IEnumerable<ProductionBuilding>> GetAsync()
        {
            return await _repository.GetAsync();
        }

        // GET api/<controller>/5
        public async Task<ProductionBuilding> GetAsync(Guid id)
        {
            return await _repository.GetAsync(id);
        }

        // POST api/<controller>
        public async Task<ProductionBuilding> CreateAsync([FromBody] ProductionBuilding entity)
        {
            return await _repository.CreateAsync(entity);
        }

        // PUT api/<controller>/5
        public async Task<ProductionBuilding> UpdateAsync(Guid id, [FromBody] ProductionBuilding entity)
        {
            var entitydb = await _repository.GetAsync(id);

            entitydb.ResourceProduced = entity.ResourceProduced;
            entitydb.LastHarvestTime = entity.LastHarvestTime;

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