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
    public class CraftableRepository : ICraftableRepository
    {
        public async Task<Craftable> CreateAsync(Craftable entity)
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

        public async Task<IEnumerable<Craftable>> GetAsync()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            List<Craftable> craftables = new List<Craftable>();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    craftables = await session
                        .Query<Craftable>()
                        .Select(x => new Craftable
                        {
                            Id = x.Id,
                            TimeToBuild = x.TimeToBuild,
                            TimeUntilCrafted = x.TimeUntilCrafted,
                            CraftableType = x.CraftableType,
                            IsFinishedCrafting = x.IsFinishedCrafting,
                            RequiredResources = x.RequiredResources.Select(y => new ResourceBatch
                            {
                                Id = y.Id
                            }).ToList()
                        })
                        .ToListAsync();

                    await tx.CommitAsync();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
            return craftables;
        }

        public async Task<Craftable> GetAsync(Guid id)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            Craftable craftable = new Craftable();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    craftable = await session
                        .Query<Craftable>()
                        .Where(x => x.Id == id)
                        .Select(x => new Craftable
                        {
                            Id = x.Id,
                            TimeToBuild = x.TimeToBuild,
                            TimeUntilCrafted = x.TimeUntilCrafted,
                            CraftableType = x.CraftableType,
                            IsFinishedCrafting = x.IsFinishedCrafting,
                            RequiredResources = x.RequiredResources.Select(y => new ResourceBatch
                            {
                                Id = y.Id
                            }).ToList()
                        })
                        .SingleOrDefaultAsync();

                    await tx.CommitAsync();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
            return craftable;
        }

        public async Task<Craftable> UpdateAsync(Craftable entity)
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