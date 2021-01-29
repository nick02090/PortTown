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
    public class ItemController : ApiController
    {
        private readonly IItemRepository _repository;
        private readonly IItemService _service;

        public ItemController(IItemRepository repository, IItemService service)
        {
            _repository = repository;
            _service = service;
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
        public async Task<HttpResponseMessage> CreateAsync([FromBody] Item entity)
        {
            var entitydb = await _repository.CreateAsync(entity);
            return Request.CreateResponse(HttpStatusCode.Created, entitydb);
        }

        // PUT api/<controller>/5
        [HttpPost]
        public async Task<HttpResponseMessage> UpdateAsync(Guid id, [FromBody] Item entity)
        {
            var entitydb = await _repository.GetAsync(id);

            entitydb.Name = entity.Name;
            entitydb.Quality = entity.Quality;

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
        [Route("api/item/check-initial-template-data")]
        [HttpGet]
        public async Task<HttpResponseMessage> CheckInitialDataAsync()
        {
            var hasData = await _service.CheckInitialTemplateData();
            return Request.CreateResponse(HttpStatusCode.OK, hasData.Result);
        }

        [Route("api/item/add-initial-template-data")]
        [HttpPost]
        public async Task<HttpResponseMessage> AddInitialTemplateDataAsync()
        {
            await _service.AddInitialTemplateData();
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [Route("api/item/template")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetTemplateDataAsync()
        {
            var template = await _repository.GetTemplateAsync();
            return Request.CreateResponse(HttpStatusCode.OK, template);
        }

        [Route("api/item/template")]
        [HttpPost]
        public async Task<HttpResponseMessage> AddDataToTemplate([FromBody] Item item)
        {
            if (item.ParentCraftable == null)
            {
                var error = new JSONErrorFormatter("Item doesn't contain craftable information!",
                    item.ParentCraftable, "ParentCraftable", "POST", $"api/item/template",
                    "ItemController.AddDataToTemplate");
                return Request.CreateResponse(HttpStatusCode.BadRequest, error);
            }
            await _service.AddDataToTemplate(item);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
        #endregion
    }
}