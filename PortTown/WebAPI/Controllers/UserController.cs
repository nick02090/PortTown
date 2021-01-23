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
        [Route("api/user/register/{townName}")]
        [HttpPost]
        public async Task<object> CreateAsync([FromUri] string townName, [FromBody] User entity)
        {
            var originalPassword = entity.Password;
            // Check email availability
            var isAvailable = await _service.CheckAvailability(entity.Email);
            if (!isAvailable.Availability)
            {
                return new JSONErrorFormatter("User with that email already exists!", 
                    entity.Email, $"api/user/register/{townName}", "UserService.CheckAvailability");
            }
            // Hash the password and create a new user (entitydb)
            entity.Password = _passwordHasher.HashPassword(entity.Password);
            var entitydb = await _service.CreateUserWithTown(entity, townName);
            // Authenticate user with original password
            // NOTE: authentication should always pass here, so no need to check the cast
            entitydb.Password = originalPassword;
            var entityauth = await AuthenticateAsync(entitydb) as User;
            // Remove password from the result 
            entitydb.Password = null;
            // Add the token to the result
            entitydb.Token = entityauth.Token;
            return entitydb;
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<User> UpdateAsync([FromUri]Guid id, [FromBody] User entity)
        {
            var entitydb = await _repository.GetAsync(id);

            entitydb.Username = entity.Username;
            entitydb.Email = entity.Email;
            // TODO: Check email availability
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
        public async Task<object> AuthenticateAsync([FromBody] User entity)
        {
            var entitydb = await _repository.GetByEmailAsync(entity.Email);

            if (entitydb == null)
            {
                // invalid email
                return new JSONErrorFormatter("Invalid email!", 
                    entity.Email, "api/user/authenticate", "UserRepository.GetByEmailAsync");
            }

            var entityauth = await _service.Authenticate(entitydb, entity.Password);

            if (entityauth == null)
            {
                // invalid password
                return new JSONErrorFormatter("Invalid password!",
                    entity.Password, "api/user/authenticate", "UserService.Authenticate");
            }

            entityauth.Password = null;
            return entityauth;
        }

        [Route("api/user/check-availability")]
        [HttpPost]
        public async Task<dynamic> CheckAvailabilityAsync([FromBody] User entity)
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