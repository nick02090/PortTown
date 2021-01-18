using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    public class UserController : ApiController
    {

        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        // GET api/<controller>
        public async Task<IEnumerable<User>> GetAsync()
        {
            return await _repository.GetAsync();
        }

        // GET api/<controller>/5
        public async Task<User> GetAsync(Guid id)
        {
            return await _repository.GetAsync(id);
        }

        // POST api/<controller>
        public async Task<User> CreateAsync([FromBody] User entity)
        {
            return await _repository.CreateAsync(entity);
        }

        // PUT api/<controller>/5
        public async Task<User> UpdateAsync(Guid id, [FromBody] User entity)
        {
            var entitydb = await _repository.GetAsync(id);

            entitydb.Username = entity.Username;
            entitydb.Email = entity.Email;
            entitydb.Password = entity.Password;

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