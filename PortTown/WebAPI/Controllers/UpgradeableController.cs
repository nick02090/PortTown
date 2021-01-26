using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI.Helpers;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    public class UpgradeableController : ApiController
    {
        private readonly IUpgradeableRepository _repository;

        public UpgradeableController(IUpgradeableRepository repository)
        {
            _repository = repository;
        }


        // GET api/<controller>
        [HttpGet]
        public async Task<HttpResponseMessage> GetAsync()
        {
            var entites = await _repository.GetAsync();
            return Request.CreateResponse(HttpStatusCode.OK, entites);
        }

        // GET api/<controller>/5
        [HttpGet]
        public async Task<HttpResponseMessage> GetAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            return Request.CreateResponse(HttpStatusCode.OK, entity);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<HttpResponseMessage> CreateAsync([FromBody] Upgradeable entity)
        {
            var entitydb = await _repository.CreateAsync(entity);
            return Request.CreateResponse(HttpStatusCode.Created, entitydb);
        }

        // PUT api/<controller>/5
        [HttpPost]
        public async Task<HttpResponseMessage> UpdateAsync(Guid id, [FromBody] Upgradeable entity)
        {
            var entitydb = await _repository.GetAsync(id);

            entitydb.RequiredResources = entity.RequiredResources;
            entitydb.TimeToUpgrade = entity.TimeToUpgrade;
            entitydb.TimeUntilUpgraded = entity.TimeUntilUpgraded;
            entitydb.IsFinishedUpgrading = entity.IsFinishedUpgrading;
            entitydb.UpgradeMultiplier = entity.UpgradeMultiplier;

            entitydb = await _repository.UpdateAsync(entitydb);
            return Request.CreateResponse(HttpStatusCode.OK, entitydb);
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}