using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    public class TownController : ApiController
    {
        private readonly ITownRepository _repository;

        public TownController(ITownRepository repository)
        {
            _repository = repository;
        }

        // GET api/<controller>
        public async Task<IEnumerable<Town>> GetAsync()
        {
            return await _repository.GetAsync();
            //var town = await AddingTownMock();
            //await AddingProdMock(town);
            //var prods = await GetProdsAsync(town.Id);
            //town.ProductionBuildings = prods;
            //return town;
        }

        // GET api/<controller>/5
        public async Task<Town> GetAsync(Guid id)
        {
            return await _repository.GetAsync(id);
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}