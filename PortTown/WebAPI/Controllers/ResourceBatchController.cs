using Domain;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    public class ResourceBatchController : ApiController
    {
        private readonly IResourceBatchRepository _repository;
        private readonly IResourceBatchService _service;

        public ResourceBatchController(IResourceBatchRepository repository, IResourceBatchService service)
        {
            _repository = repository;
            _service = service;
        }

        [Route("api/resourcebatch/check-initial-template-data")]
        [HttpGet]
        public async Task<HttpResponseMessage> CheckInitialDataAsync()
        {
            var hasData = await _service.CheckInitialTemplateData();
            return Request.CreateResponse(HttpStatusCode.OK, hasData.Result);
        }

        [Route("api/resourcebatch/add-initial-template-data")]
        [HttpPost]
        public async Task<HttpResponseMessage> AddInitialTemplateDataAsync()
        {
            await _service.AddInitialTemplateData();
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [Route("api/marketplace/template")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetTemplateDataAsync()
        {
            var template = await _repository.GetTemplateAsync();
            return Request.CreateResponse(HttpStatusCode.OK, template);
        }

        // PUT api/<controller>/5
        public async Task<ResourceBatch> UpdateAsync(Guid id, [FromBody] ResourceBatch entity)
        {
            var entitydb = await _repository.GetAsync(id);

            entitydb.ResourceType = entity.ResourceType;
            entitydb.Quantity = entity.Quantity;

            return await _repository.UpdateAsync(entitydb);
        }

        // DELETE api/<controller>/5
        public async Task DeleteAsync(Guid id)
        {
            var entityDel = await _repository.GetForDeleteAsync(id);
            await _repository.DeleteAsync(entityDel);
            return;
        }
    }
}