using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    public class StorageController : ApiController
    {
        private readonly IStorageRepository _repository;

        public StorageController(IStorageRepository repository)
        {
            _repository = repository;
        }

        // GET api/<controller>
        public async Task<IEnumerable<Storage>> GetAsync()
        {
            return await _repository.GetAsync();
        }

        // GET api/<controller>/5
        public async Task<Storage> GetAsync(Guid id)
        {
            return await _repository.GetAsync(id);
        }

        // POST api/<controller>
        public async Task<Storage> CreateAsync([FromBody] Storage entity)
        {
            return await _repository.CreateAsync(entity);
        }

        // PUT api/<controller>/5
        public async Task<Storage> UpdateAsync(Guid id, [FromBody] Storage entity)
        {
            var entitydb = await _repository.GetAsync(id);

            entitydb.StoredResources = entity.StoredResources;

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