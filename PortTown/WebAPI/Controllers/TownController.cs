using Domain;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI.Helpers;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    public class TownController : ApiController
    {
        private readonly ITownRepository _repository;
        private readonly ITownService _service;

        public TownController(ITownRepository repository, ITownService service)
        {
            _repository = repository;
            _service = service;
        }

        // GET api/<controller>
        [HttpGet]
        public async Task<HttpResponseMessage> GetAsync()
        {
            var towns = await _service.GetTowns();
            return Request.CreateResponse(HttpStatusCode.OK, towns);
        }

        // GET api/<controller>/5
        [HttpGet]
        public async Task<HttpResponseMessage> GetAsync(Guid id)
        {
            var town = await _service.GetTown(id);
            return Request.CreateResponse(HttpStatusCode.OK, town);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<HttpResponseMessage> CreateAsync([FromBody] Town entity)
        {
            var town = await _repository.CreateAsync(entity);
            return Request.CreateResponse(HttpStatusCode.Created, town);
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<HttpResponseMessage> UpdateAsync(Guid id, [FromBody] Town entity)
        {
            var entitydb = await _service.GetTown(id);

            entitydb.Name = entity.Name;
            entitydb.Level = entity.Level;

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

        [Route("api/town/reset/{id}")]
        [HttpPost]
        public async Task<HttpResponseMessage> ResetAsync([FromUri] Guid id)
        {
            await _service.ResetAsync(id);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [Route("api/town/upgrade/{id}")]
        [HttpPost]
        public async Task<HttpResponseMessage> UpgradeAsync([FromUri] Guid id)
        {
            var town = await _service.GetTown(id);
            if (!town.Upgradeable.IsFinishedUpgrading)
            {
                var error = new JSONErrorFormatter("The town hasn't finished upgrading!", town.Upgradeable.IsFinishedUpgrading, 
                    "Upgradeable.IsFinishedUpgrading", "POST", $"api/town/upgrade/{id}",
                    "TownController.UpgradeAsync");
                return Request.CreateResponse(HttpStatusCode.BadRequest, error);
            }
            town = await _service.UpgradeLevel(town);
            return Request.CreateResponse(HttpStatusCode.OK, town);
        }

        [Route("api/town/start-upgrade/{id}")]
        [HttpPost]
        public async Task<HttpResponseMessage> StartUpgradeAsync([FromUri] Guid id)
        {
            var town = await _service.GetTown(id);
            var canUpgradeLevel = await _service.CanUpgradeLevel(town);
            if (!(bool)canUpgradeLevel["CanUpgrade"])
            {
                var error = new JSONErrorFormatter("The town level cannot be upgraded due to unsufficient funds!", 
                    town.Level, "Level", "POST", $"api/town/start-upgrade/{id}",
                    "TownController.StartUpgradeAsync");
                return Request.CreateResponse(HttpStatusCode.BadRequest, error);
            }
            town = await _service.StartUpgradeLevel(town);
            return Request.CreateResponse(HttpStatusCode.OK, town);
        }

        [Route("api/town/can-upgrade/{id}")]
        [HttpGet]
        public async Task<HttpResponseMessage> CanUpgradeAsync([FromUri] Guid id)
        {
            var town = await _service.GetTown(id);
            var canUpgrade = await _service.CanUpgradeLevel(town);
            return Request.CreateResponse(HttpStatusCode.OK, canUpgrade);
        }
    }
}