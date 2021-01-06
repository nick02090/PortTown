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
        // GET api/<controller>
        public async Task<Town> GetAsync()
        {
            await AddingMock();
            var towns = await GetTownsAsync();
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
                    towns = await session
                        .Query<Town>()
                        .Where(c => c.Level >= 2)
                        .ToListAsync();

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
                Name = "SpajicGrad",
                Level = 5
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