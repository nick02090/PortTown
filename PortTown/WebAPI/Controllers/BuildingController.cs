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
    public class BuildingController : ApiController
    {

        private readonly IBuildingRepository _repository;
        private readonly IBuildingService _service;

        public BuildingController(IBuildingRepository repository, IBuildingService service)
        {
            _repository = repository;
            _service = service;
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
            var entity = await _repository.GetAsync(id);
            return Request.CreateResponse(HttpStatusCode.OK, entity);
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
            await _repository.DeleteAsync(id);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

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
            var template = await _repository.GetTemplateAsync();
            return Request.CreateResponse(HttpStatusCode.OK, template);
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