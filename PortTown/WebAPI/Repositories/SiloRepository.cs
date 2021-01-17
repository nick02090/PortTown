using Domain;
using Domain.Helper;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebAPI.Interfaces;

namespace WebAPI.Repositories
{
    public class SiloRepository : ISiloRepository
    {
        public async Task<Silo> CreateAsync(Silo entity)
        {
            ISession session = NHibernateHelper.GetCurrentSession();

            try
            {
                using (var tx = session.BeginTransaction())
                {

                    await session.SaveAsync(entity);


                    await tx.CommitAsync();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }

            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetAsync(id);
            ISession session = NHibernateHelper.GetCurrentSession();

            try
            {
                using (var tx = session.BeginTransaction())
                {

                    await session.DeleteAsync(entity);


                    await tx.CommitAsync();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }

            return;
        }

        public async Task<IEnumerable<Silo>> GetAsync()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            List<Silo> silos = new List<Silo>();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    silos = await session
                        .Query<Silo>()
                        .Select(x => new Silo
                        {
                            Id = x.Id,
                            StoredFood = x.StoredFood
                        })
                        .ToListAsync();

                    await tx.CommitAsync();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
            return silos;
        }

        public async Task<Silo> GetAsync(Guid id)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            Silo silo = new Silo();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    silo = await session
                        .Query<Silo>()
                        .Where(x => x.Id == id)
                        .Select(x => new Silo   // todo: vjj fali capacity
                        {
                            Id = x.Id,
                            StoredFood = x.StoredFood
                        })
                        .SingleOrDefaultAsync();

                    await tx.CommitAsync();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
            return silo;
        }

        public async Task<Silo> UpdateAsync(Silo entity)
        {
            ISession session = NHibernateHelper.GetCurrentSession();

            try
            {
                using (var tx = session.BeginTransaction())
                {

                    await session.UpdateAsync(entity);


                    await tx.CommitAsync();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }

            return entity;
        }
    }
}