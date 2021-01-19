using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI.Helpers;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserRepository _repository;
        private readonly IUserService _service;
        private readonly PasswordHasher _passwordHasher;

        public UserController(IUserRepository repository, IUserService service)
        {
            _repository = repository;
            _service = service;
            _passwordHasher = new PasswordHasher();
        }

        // GET api/<controller>
        [HttpGet]
        public async Task<IEnumerable<User>> GetAsync()
        {
            return await _repository.GetAsync();
        }

        // GET api/<controller>/5
        [HttpGet]
        public async Task<User> GetAsync([FromUri] Guid id)
        {
            var entitydb = await _repository.GetAsync(id);
            entitydb.Password = null;
            return entitydb;
        }

        // POST api/<controller>
        [Route("api/user/register")]
        [HttpPost]
        public async Task<User> CreateAsync([FromBody] User entity)
        {
            if (!await _service.CheckAvailability(entity.Email))
            {
                return null;
            }

            entity.Password = _passwordHasher.HashPassword(entity.Password);
            var entitydb = await _repository.CreateAsync(entity);
            entitydb.Password = null;
            return entitydb;
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<User> UpdateAsync([FromUri]Guid id, [FromBody] User entity)
        {
            var entitydb = await _repository.GetAsync(id);

            entitydb.Username = entity.Username;
            entitydb.Email = entity.Email;
            entitydb.Password = _passwordHasher.HashPassword(entity.Password);

            var updatedEntitydb = await _repository.UpdateAsync(entitydb);
            updatedEntitydb.Password = null;
            return updatedEntitydb;
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        [Route("api/user/authenticate")]
        [HttpPost]
        public async Task<User> AuthenticateAsync([FromBody] User entity)
        {
            var entitydb = await _service.Authenticate(entity.Email, entity.Password);

            if (entitydb == null)
            {
                return null;
            }

            entitydb.Password = null;

            return entitydb;
        }

        [Route("api/user/check-availability")]
        [HttpPost]
        public async Task<bool> CheckAvailabilityAsync([FromBody] User entity)
        {
            return await _service.CheckAvailability(entity.Email);
        }

        [Route("api/user/logout/{token}")]
        [HttpGet]
        public async Task LogoutAsync([FromUri] string token)
        {
            await _service.Logout(token);
        }
    }
}