using Domain;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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
        public async Task<HttpResponseMessage> CreateAsync([FromUri] string townName, [FromBody] User entity)
        {
            var originalPassword = entity.Password;
            // Check email availability
            var isAvailable = await _service.CheckAvailability(entity.Email);
            if (!(bool)isAvailable["Availability"])
            {
                var error = new JSONErrorFormatter("User with that email already exists!",
                    entity.Email, $"api/user/register/{townName}", "UserService.CheckAvailability");
                return Request.CreateResponse(HttpStatusCode.BadRequest, error);
            }
            // Hash the password and create a new user (entitydb)
            entity.Password = _passwordHasher.HashPassword(entity.Password);
            var entitydb = await _service.CreateUserWithTown(entity, townName);
            // Authenticate user with original password
            // NOTE: authentication should always pass here, so no need to check the cast
            entitydb.Password = originalPassword;
            var entityauths = await AuthenticateAsync(entitydb);
            var entityauth = await entityauths.Content.ReadAsAsync<User>();
            // Remove password from the result 
            entitydb.Password = null;
            // Add the token to the result
            entitydb.Token = entityauth.Token;
            return Request.CreateResponse(HttpStatusCode.Created, entitydb);
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
        public async Task<HttpResponseMessage> AuthenticateAsync([FromBody] User entity)
        {
            var entitydb = await _repository.GetByEmailAsync(entity.Email);

            if (entitydb == null)
            {
                // invalid email
                var error = new JSONErrorFormatter("Invalid email!",
                    entity.Email, "api/user/authenticate", "UserRepository.GetByEmailAsync");
                return Request.CreateResponse(HttpStatusCode.BadRequest, error);
            }

            var entityauth = await _service.Authenticate(entitydb, entity.Password);

            if (entityauth == null)
            {
                // invalid password
                var error = new JSONErrorFormatter("Invalid password!",
                    entity.Password, "api/user/authenticate", "UserService.Authenticate");
                return Request.CreateResponse(HttpStatusCode.BadRequest, error);
            }

            entityauth.Password = null;
            return Request.CreateResponse(HttpStatusCode.Accepted, entityauth);
        }

        [Route("api/user/check-availability")]
        [HttpPost]
        public async Task<HttpResponseMessage> CheckAvailabilityAsync([FromBody] User entity)
        {
            var availability = await _service.CheckAvailability(entity.Email);
            return Request.CreateResponse(HttpStatusCode.OK, availability.Result);
        }

        [Route("api/user/logout/{token}")]
        [HttpGet]
        public async Task LogoutAsync([FromUri] string token)
        {
            await _service.Logout(token);
        }
    }
}