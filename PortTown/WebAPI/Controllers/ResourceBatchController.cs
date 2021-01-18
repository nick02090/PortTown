using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    public class ResourceBatchController : ApiController
    {
        private readonly IResourceBatchRepository _repository;

        public ResourceBatchController(IResourceBatchRepository repository)
        {
            _repository = repository;
        }

        // GET api/<controller>
        public async Task<IEnumerable<ResourceBatch>> GetAsync()
        {
            return await _repository.GetAsync();
        }

        // GET api/<controller>/5
        public async Task<ResourceBatch> GetAsync(Guid id)
        {
            return await _repository.GetAsync(id);
        }

        // POST api/<controller>
        public async Task<ResourceBatch> CreateAsync([FromBody] ResourceBatch entity)
        {
            return await _repository.CreateAsync(entity);
        }

        // PUT api/<controller>/5
        public async Task<ResourceBatch> UpdateAsync(Guid id, [FromBody] ResourceBatch entity)
        {
            var entitydb = await _repository.GetAsync(id);

            entitydb.ResourceType = entity.ResourceType;
            entitydb.Size = entity.Size;

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