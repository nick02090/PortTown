using Domain;
using Domain.Helper;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Interfaces;

namespace WebAPI.Repositories
{
    public class SellableRepository : ISellableRepository
    {
        public async Task<Sellable> CreateAsync(Sellable entity)
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

        public async Task<IEnumerable<Sellable>> GetAsync()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            List<Sellable> sellables = new List<Sellable>();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    sellables = await session
                        .Query<Sellable>()
                        .Select(x => new Sellable
                        {
                            Id = x.Id,
                            Price = x.Price
                        })
                        .ToListAsync();

                    await tx.CommitAsync();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
            return sellables;
        }

        public async Task<Sellable> GetAsync(Guid id)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            var sellable = new Sellable();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    sellable = await session
                        .Query<Sellable>()
                        .Where(x => x.Id == id)
                        .Select(x => new Sellable
                        {
                            Id = x.Id,
                            Price = x.Price
                        })
                        .SingleOrDefaultAsync();

                    await tx.CommitAsync();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
            return sellable;
        }

        public async Task<Sellable> UpdateAsync(Sellable entity)
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