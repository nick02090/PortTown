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
        private readonly IBuildingService _buildingService;

        public TownController(ITownRepository repository, ITownService service,
            IBuildingService buildingService)
        {
            _repository = repository;
            _service = service;

            _buildingService = buildingService;
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
            var entityDel = await _repository.GetForDeleteAsync(id);
            await _repository.DeleteAsync(entityDel);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [Route("api/town/reset/{id}")]
        [HttpPost]
        public async Task<HttpResponseMessage> ResetAsync([FromUri] Guid id)
        {
            var townBuildings = await _buildingService.GetBuildingsByTown(id);
            await _service.ResetAsync(id, townBuildings);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        #region Upgrade
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
            var townBuildings = await _buildingService.GetBuildingsByTown(id);
            town = await _service.UpgradeLevel(town, townBuildings);
            return Request.CreateResponse(HttpStatusCode.OK, town);
        }

        [Route("api/town/start-upgrade/{id}")]
        [HttpPost]
        public async Task<HttpResponseMessage> StartUpgradeAsync([FromUri] Guid id)
        {
            var town = await _service.GetTown(id);
            var townBuildings = await _buildingService.GetBuildingsByTown(id);
            var canUpgradeLevel = await _service.CanUpgradeLevel(town, townBuildings);
            if (!(bool)canUpgradeLevel["CanUpgrade"])
            {
                var error = new JSONErrorFormatter("The town level cannot be upgraded due to unsufficient funds!", 
                    town.Level, "Level", "POST", $"api/town/start-upgrade/{id}",
                    "TownController.StartUpgradeAsync");
                return Request.CreateResponse(HttpStatusCode.BadRequest, error);
            }
            townBuildings = await _buildingService.GetBuildingsByTown(id);
            town = await _service.StartUpgradeLevel(town, townBuildings);
            return Request.CreateResponse(HttpStatusCode.OK, town);
        }

        [Route("api/town/can-upgrade/{id}")]
        [HttpGet]
        public async Task<HttpResponseMessage> CanUpgradeAsync([FromUri] Guid id)
        {
            var town = await _service.GetTown(id);
            var townBuildings = await _buildingService.GetBuildingsByTown(id);
            var canUpgrade = await _service.CanUpgradeLevel(town, townBuildings);
            return Request.CreateResponse(HttpStatusCode.OK, canUpgrade);
        }

        [Route("api/town/craft-info/{id}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetTownWithCraftInfo([FromUri] Guid id)
        {
            var townBuildings = await _buildingService.GetBuildingsByTown(id);
            var result = await _service.GetTownWithCraftInfo(id, townBuildings);
            return Request.CreateResponse(HttpStatusCode.OK, result.Result);
        }
        #endregion
    }
}