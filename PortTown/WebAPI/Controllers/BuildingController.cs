using Domain;
using Domain.Enums;
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
    public class BuildingController : ApiController
    {

        private readonly IBuildingRepository _repository;
        private readonly IBuildingService _service;
        private readonly IProductionBuildingService _productionBuildingService;
        private readonly ITownService _townService;

        public BuildingController(IBuildingRepository repository, IBuildingService service,
            IProductionBuildingService productionBuildingService, ITownService townService)
        {
            _repository = repository;
            _service = service;

            _productionBuildingService = productionBuildingService;
            _townService = townService;
        }


        // GET api/<controller>
        [HttpGet]
        public async Task<HttpResponseMessage> GetAsync()
        {
            var entities = await _repository.GetAsync();
            return Request.CreateResponse(HttpStatusCode.OK, entities);
        }

        // GET api/<controller>/5
        [HttpGet]
        public async Task<HttpResponseMessage> GetAsync(Guid id)
        {
            var building = await _service.GetBuilding(id);
            var canUpgrade = new JSONFormatter();
            canUpgrade.AddField("CanUpgrade", true);
            var upgradeMessage = new JSONFormatter();
            upgradeMessage.AddField("UpgradeMessage", $"Building can be upgraded to level {building.Level + 1}.");
            var town = await _townService.GetTown(building.Town.Id);
            if (!_townService.DoesTownAllowUpgrade(town, building.Level + 1))
            {
                canUpgrade["CanUpgrade"] = false;
                upgradeMessage["UpgradeMessage"] = "Town level doesn't allow this upgrade!";
            }
            else
            {
                canUpgrade = await _service.CanUpgradeLevel(building);
                if (!(bool)canUpgrade["CanUpgrade"])
                {
                    upgradeMessage["UpgradeMessage"] = "Unsufficient funds to upgrade this building!";
                }
            }
            var buildingResult = new JSONFormatter();
            buildingResult.AddField("Id", building.Id);
            buildingResult.AddField("Name", building.Name);
            buildingResult.AddField("Level", building.Level);
            buildingResult.AddField("Capacity", building.Capacity);
            buildingResult.AddField("BuildingType", building.BuildingType);
            buildingResult.AddField("Town", building.Town);
            buildingResult.AddField("ParentCraftable", building.ParentCraftable);
            buildingResult.AddField("Upgradeable", building.Upgradeable);
            buildingResult.AddField("ChildProductionBuilding", building.ChildProductionBuilding);
            buildingResult.AddField("ChildStorage", building.ChildStorage);
            buildingResult.AddField("CanUpgrade", canUpgrade["CanUpgrade"]);
            buildingResult.AddField("UpgradeMessage", upgradeMessage["UpgradeMessage"]);

            if (building.BuildingType == BuildingType.Production)
            {
                var accumulatedResources = await _productionBuildingService.GetCurrentResources(building.ChildProductionBuilding.Id);
                buildingResult.AddField("AccumulatedResources", accumulatedResources["AccumulatedResources"]);
                buildingResult.AddField("HarvestMessage", "Accumulated resources can be harvested!");
                if (building.ParentCraftable.IsFinishedCrafting)
                {
                    buildingResult["AccumulatedResources"] = 0;
                    accumulatedResources["AccumulatedResources"] = 0;
                    buildingResult["HarvestMessage"] = "This building is still under construction!";
                }
                buildingResult.AddField("CanHarvest", true);
                if ((int)accumulatedResources["AccumulatedResources"] <= 0)
                {
                    buildingResult["CanHarvest"] = false;
                    buildingResult["HarvestMessage"] = "There is nothing to harvest!";
                }
                var townBuildings = await _service.GetBuildingsByTown(building.Town.Id);
                var canHarvest = await _productionBuildingService.CanHarvest(building.ChildProductionBuilding.Id, townBuildings);
                if (!(bool)canHarvest["CanHarvest"])
                {
                    buildingResult["CanHarvest"] = false;
                    buildingResult["HarvestMessage"] = "There is no storage room to harvest the accumulated resources!";
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, buildingResult.Result);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<HttpResponseMessage> CreateAsync([FromBody] Building entity)
        {
            var entitydb = await _repository.CreateAsync(entity);
            return Request.CreateResponse(HttpStatusCode.Created, entitydb);
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<HttpResponseMessage> UpdateAsync(Guid id, [FromBody] Building entity)
        {
            var entitydb = await _repository.GetAsync(id);

            entitydb.Name = entity.Name;
            entitydb.Level = entity.Level;
            entitydb.Capacity = entity.Capacity;
            entitydb.BuildingType = entity.BuildingType;

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

        [Route("api/building/town/{id}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetByTownAsync([FromUri] Guid id)
        {
            var buildings = await _service.GetBuildingsByTown(id);
            var result = new List<object>();
            foreach (var building in buildings)
            {
                var canUpgrade = new JSONFormatter();
                canUpgrade.AddField("CanUpgrade", true);
                var upgradeMessage = new JSONFormatter();
                upgradeMessage.AddField("UpgradeMessage", $"Building can be upgraded to level {building.Level + 1}.");
                var town = await _townService.GetTown(building.Town.Id);
                if (!_townService.DoesTownAllowUpgrade(town, building.Level + 1))
                {
                    canUpgrade["CanUpgrade"] = false;
                    upgradeMessage["UpgradeMessage"] = "Town level doesn't allow this upgrade!";
                }
                else
                {
                    canUpgrade = await _service.CanUpgradeLevel(building);
                    if (!(bool)canUpgrade["CanUpgrade"])
                    {
                        upgradeMessage["UpgradeMessage"] = "Unsufficient funds to upgrade this building!";
                    }
                }
                var buildingResult = new JSONFormatter();
                buildingResult.AddField("Id", building.Id);
                buildingResult.AddField("Name", building.Name);
                buildingResult.AddField("Level", building.Level);
                buildingResult.AddField("Capacity", building.Capacity);
                buildingResult.AddField("BuildingType", building.BuildingType);
                buildingResult.AddField("Town", building.Town);
                buildingResult.AddField("ParentCraftable", building.ParentCraftable);
                buildingResult.AddField("Upgradeable", building.Upgradeable);
                buildingResult.AddField("ChildProductionBuilding", building.ChildProductionBuilding);
                buildingResult.AddField("ChildStorage", building.ChildStorage);
                buildingResult.AddField("CanUpgrade", canUpgrade["CanUpgrade"]);
                buildingResult.AddField("UpgradeMessage", upgradeMessage["UpgradeMessage"]);
                result.Add(buildingResult.Result);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("api/building/production-info/{id}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetWithProductionInfo([FromUri] Guid id)
        {
            var building = await _service.GetBuilding(id);
            if (building.BuildingType != BuildingType.Production)
            {
                var error = new JSONErrorFormatter("The building isn't of type production building!", building.BuildingType,
                    "BuildingType", "GET", $"api/building/production-info/{id}",
                    "BuildingController.GetWithProductionInfo");
                return Request.CreateResponse(HttpStatusCode.BadRequest, error);
            }
            if (building.ParentCraftable.TimeUntilCrafted != null)
            {
                var error = new JSONErrorFormatter("The building isn't finished crafting yet!", building.ParentCraftable.TimeUntilCrafted,
                    "ParentCraftable.TimeUntilCrafted", "GET", $"api/building/production-info/{id}",
                    "BuildingController.GetWithProductionInfo");
                return Request.CreateResponse(HttpStatusCode.BadRequest, error);
            }
            var accumulatedResources = await _productionBuildingService.GetCurrentResources(building.ChildProductionBuilding.Id);
            var result = new JSONFormatter();
            result.AddField("Id", building.Id);
            result.AddField("Name", building.Name);
            result.AddField("Level", building.Level);
            result.AddField("Capacity", building.Capacity);
            result.AddField("BuildingType", building.BuildingType);
            result.AddField("Town", building.Town);
            result.AddField("ParentCraftable", building.ParentCraftable);
            result.AddField("Upgradeable", building.Upgradeable);
            result.AddField("ChildProductionBuilding", building.ChildProductionBuilding);
            result.AddField("AccumulatedResources", accumulatedResources["AccumulatedResources"]);
            result.AddField("CanHarvest", true);
            if ((int)accumulatedResources["AccumulatedResources"] <= 0)
            {
                result["CanHarvest"] = false;
            }
            var townBuildings = await _service.GetBuildingsByTown(building.Town.Id);
            var canHarvest = await _productionBuildingService.CanHarvest(building.ChildProductionBuilding.Id, townBuildings);
            if (!(bool)canHarvest["CanHarvest"])
            {
                result["CanHarvest"] = false;
            }
            return Request.CreateResponse(HttpStatusCode.OK, result.Result);
        }

        [Route("api/building/production/harvest/{id}")]
        [HttpGet]
        public async Task<HttpResponseMessage> HarvestProductionBuildingAsync([FromUri] Guid id)
        {
            var building = await _service.GetBuilding(id);
            if (building.BuildingType != BuildingType.Production)
            {
                var error = new JSONErrorFormatter("The building isn't of type production building!", building.BuildingType,
                    "BuildingType", "GET", $"api/building/production-info/{id}",
                    "BuildingController.HarvestProductionBuildingAsync");
                return Request.CreateResponse(HttpStatusCode.BadRequest, error);
            }
            if (building.ParentCraftable.TimeUntilCrafted != null)
            {
                var error = new JSONErrorFormatter("The building isn't finished crafting yet!", building.ParentCraftable.TimeUntilCrafted,
                    "ParentCraftable.TimeUntilCrafted", "GET", $"api/building/production-info/{id}",
                    "BuildingController.HarvestProductionBuildingAsync");
                return Request.CreateResponse(HttpStatusCode.BadRequest, error);
            }
            var accumulatedResources = await _productionBuildingService.GetCurrentResources(building.ChildProductionBuilding.Id);
            if ((int)accumulatedResources["AccumulatedResources"] <= 0)
            {
                var error = new JSONErrorFormatter("There is nothing to harvest!", building.ChildProductionBuilding.LastHarvestTime,
                    "ChildProductionBuildings.LastHarvestTime", "GET", $"api/building/production/harvest/{id}",
                    "BuildingController.HarvestProductionBuildingAsync");
                return Request.CreateResponse(HttpStatusCode.BadRequest, error);
            }
            var townBuildings = await _service.GetBuildingsByTown(building.Town.Id);
            var canHarvest = await _productionBuildingService.CanHarvest(building.ChildProductionBuilding.Id, townBuildings);
            if (!(bool)canHarvest["CanHarvest"])
            {
                var error = new JSONErrorFormatter("There is not enough room to harvest!", building.ChildProductionBuilding.ResourceProduced,
                    "ChildProductionBuildings.ResourceProduces", "GET", $"api/building/production/harvest/{id}",
                    "BuildingController.HarvestProductionBuildingAsync");
                return Request.CreateResponse(HttpStatusCode.BadRequest, error);
            }
            townBuildings = await _service.GetBuildingsByTown(building.Town.Id);
            await _productionBuildingService.Harvest(building.ChildProductionBuilding.Id, townBuildings);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        #region Crafting
        [Route("api/building/craft/{id}")]
        [HttpPost]
        public async Task<HttpResponseMessage> CraftAsync([FromUri] Guid id)
        {
            var building = await _service.GetBuilding(id);
            if (!building.ParentCraftable.IsFinishedCrafting)
            {
                var error = new JSONErrorFormatter("The building hasn't finished crafting!", building.ParentCraftable.IsFinishedCrafting,
                    "ParentCraftable.IsFinishedCrafting", "POST", $"api/building/craft/{id}",
                    "BuildingController.CraftAsync");
                return Request.CreateResponse(HttpStatusCode.BadRequest, error);
            }
            building = await _service.CraftBuilding(building);
            return Request.CreateResponse(HttpStatusCode.OK, building);
        }

        [Route("api/building/start-craft/{townId}/{templateId}")]
        [HttpPost]
        public async Task<HttpResponseMessage> StartCraftAsync([FromUri] Guid townId, [FromUri] Guid templateId)
        {
            var building = await _repository.GetTemplateAsync(templateId);
            var canCraft = await _service.CanCraftBuilding(building, townId);
            if (!(bool)canCraft["CanCraft"])
            {
                var error = new JSONErrorFormatter("The building cannot be crafted due to unsufficient funds!",
                    building.Name, "Name", "POST", $"api/building/start-craft/{townId}/{templateId}",
                    "BuildingController.StartCraftAsync");
                return Request.CreateResponse(HttpStatusCode.BadRequest, error);
            }
            building = await _service.StartCraftBuilding(building, townId);
            return Request.CreateResponse(HttpStatusCode.OK, building);
        }

        [Route("api/building/can-craft/{townId}/{templateId}")]
        [HttpGet]
        public async Task<HttpResponseMessage> CanCraftAsync([FromUri] Guid townId, [FromUri] Guid templateId)
        {
            var building = await _repository.GetTemplateAsync(templateId);
            var canCraft = await _service.CanCraftBuilding(building, townId);
            return Request.CreateResponse(HttpStatusCode.OK, canCraft.Result);
        }
        #endregion

        #region Upgrades
        [Route("api/building/upgrade/{id}")]
        [HttpPost]
        public async Task<HttpResponseMessage> UpgradeAsync([FromUri] Guid id)
        {
            var building = await _service.GetBuilding(id);
            if (!building.Upgradeable.IsFinishedUpgrading)
            {
                var error = new JSONErrorFormatter("The building hasn't finished upgrading!", building.Upgradeable.IsFinishedUpgrading,
                    "Upgradeable.IsFinishedUpgrading", "POST", $"api/building/upgrade/{id}",
                    "BuildingController.UpgradeAsync");
                return Request.CreateResponse(HttpStatusCode.BadRequest, error);
            }
            building = await _service.UpgradeLevel(building);
            return Request.CreateResponse(HttpStatusCode.OK, building);
        }

        [Route("api/building/start-upgrade/{id}")]
        [HttpPost]
        public async Task<HttpResponseMessage> StartUpgradeAsync([FromUri] Guid id)
        {
            var building = await _service.GetBuilding(id);
            var canUpgradeLevel = await _service.CanUpgradeLevel(building);
            if (!(bool)canUpgradeLevel["CanUpgrade"])
            {
                var error = new JSONErrorFormatter("The building level cannot be upgraded due to unsufficient funds!",
                    building.Level, "Level", "POST", $"api/building/start-upgrade/{id}",
                    "BuildingController.StartUpgradeAsync");
                return Request.CreateResponse(HttpStatusCode.BadRequest, error);
            }
            building = await _service.StartUpgradeLevel(building);
            return Request.CreateResponse(HttpStatusCode.OK, building);
        }

        [Route("api/building/can-upgrade/{id}")]
        [HttpGet]
        public async Task<HttpResponseMessage> CanUpgradeAsync([FromUri] Guid id)
        {
            var building = await _service.GetBuilding(id);
            var canUpgrade = await _service.CanUpgradeLevel(building);
            return Request.CreateResponse(HttpStatusCode.OK, canUpgrade.Result);
        }
        #endregion

        #region Template
        [Route("api/building/check-initial-template-data")]
        [HttpGet]
        public async Task<HttpResponseMessage> CheckInitialDataAsync()
        {
            var hasData = await _service.CheckInitialTemplateData();
            return Request.CreateResponse(HttpStatusCode.OK, hasData.Result);
        }

        [Route("api/building/add-initial-template-data")]
        [HttpPost]
        public async Task<HttpResponseMessage> AddInitialTemplateDataAsync()
        {
            await _service.AddInitialTemplateData();
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [Route("api/building/template")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetTemplateDataAsync()
        {
            var template = await _service.GetTemplateBuildings();
            return Request.CreateResponse(HttpStatusCode.OK, template);
        }

        [Route("api/building/template/{id}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetTemplateForTown([FromUri] Guid id)
        {
            var townTemplate = await _service.FilterTemplateForTown(id);
            var result = new List<Dictionary<string, object>>();
            foreach (var template in townTemplate)
            {
                result.Add(template.Result);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("api/building/template")]
        [HttpPost]
        public async Task<HttpResponseMessage> AddDataToTemplate([FromBody] Building building)
        {
            if (building.ParentCraftable == null)
            {
                var error = new JSONErrorFormatter("Building doesn't contain craftable information!", 
                    building.ParentCraftable, "ParentCraftable", "POST", $"api/building/template", 
                    "BuildingController.AddDataToTemplate");
                return Request.CreateResponse(HttpStatusCode.BadRequest, error);
            }
            if (building.ChildProductionBuilding == null && building.ChildStorage == null)
            {
                var error = new JSONErrorFormatter("Building doesn't contain child information! It has to be a production or a storage!",
                    building.ChildProductionBuilding, "Child{ProductionBuilding}/{Storage}", "POST", $"api/building/template",
                    "BuildingController.AddDataToTemplate");
                return Request.CreateResponse(HttpStatusCode.BadRequest, error);
            }
            await _service.AddDataToTemplate(building);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
        #endregion
    }
}