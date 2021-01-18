using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    public class ItemController : ApiController
    {
        private readonly IItemRepository _repository;

        public ItemController(IItemRepository repository)
        {
            _repository = repository;
        }


        // GET api/<controller>
        public async Task<IEnumerable<Item>> GetAsync()
        {
            return await _repository.GetAsync();
        }

        // GET api/<controller>/5
        public async Task<Item> GetAsync(Guid id)
        {
            return await _repository.GetAsync(id);
        }

        // POST api/<controller>
        public async Task<Item> CreateAsync([FromBody] Item entity)
        {
            return await _repository.CreateAsync(entity);
        }

        // PUT api/<controller>/5
        public async Task<Item> UpdateAsync(Guid id, [FromBody] Item entity)
        {
            var entitydb = await _repository.GetAsync(id);

            entitydb.Value = entity.Value;

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