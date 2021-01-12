using Domain;
using Domain.Helper;
using NHibernate;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class TownController : ApiController
    {
        // GET api/<controller>
        public async Task<Town> GetAsync()
        {
            await AddingMock();
            var towns = await GetTownsAsync();
            var town = towns.FirstOrDefault();
            await AddingProdsMock(town);
            towns = await GetTownsAsync();
            return towns.FirstOrDefault();
        }

        private async Task<List<Town>> GetTownsAsync()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            List<Town> towns = new List<Town>();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    var result = await session
                        .Query<Town>()
                        .Select(x => new Town
                        { 
                            Id = x.Id,
                            Name = x.Name,
                            Level = x.Level,
                            ProductionBuildings = x.ProductionBuildings
                        })
                        .ToListAsync();

                    towns = result.ToList();

                    await tx.CommitAsync();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
            return towns;
        }

        private async Task AddingMock()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            var spajicGrad = new Town
            {
                Name = "SpajicGrad"
            };
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    await session.SaveAsync(spajicGrad);
                    await tx.CommitAsync();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
            return;
        }

        private async Task AddingProdsMock(Town town)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            var prod = new ProductionBuilding
            {
                Name = "Zlatara",
                Town = town
            };
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    await session.SaveAsync(prod);
                    await tx.CommitAsync();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
            return;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
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