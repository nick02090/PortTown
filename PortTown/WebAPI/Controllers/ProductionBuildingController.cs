﻿using Domain;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI.Helpers;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    public class ProductionBuildingController : ApiController
    {
        private readonly IProductionBuildingRepository _repository;
        private readonly IProductionBuildingService _service;

        public ProductionBuildingController(IProductionBuildingRepository repository,
            IProductionBuildingService service)
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
        public async Task<HttpResponseMessage> CreateAsync([FromBody] ProductionBuilding entity)
        {
            var entitydb = await _repository.CreateAsync(entity);
            return Request.CreateResponse(HttpStatusCode.Created, entitydb);
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<HttpResponseMessage> UpdateAsync(Guid id, [FromBody] ProductionBuilding entity)
        {
            var entitydb = await _repository.GetAsync(id);

            entitydb.ResourceProduced = entity.ResourceProduced;
            entitydb.LastHarvestTime = entity.LastHarvestTime;

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
    }
}