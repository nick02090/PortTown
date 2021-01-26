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
    public class UpgradeableRepository : IUpgradeableRepository
    {
        public async Task<Upgradeable> CreateAsync(Upgradeable entity)
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

        public async Task<IEnumerable<Upgradeable>> GetAsync()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            List<Upgradeable> upgradeables = new List<Upgradeable>();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    upgradeables = await session
                        .Query<Upgradeable>()
                        .Select(x => new Upgradeable
                        {
                            Id = x.Id,
                            TimeToUpgrade = x.TimeToUpgrade,
                            TimeUntilUpgraded = x.TimeUntilUpgraded,
                            IsFinishedUpgrading = x.IsFinishedUpgrading,
                            UpgradeMultiplier = x.UpgradeMultiplier,
                            RequiredResources = x.RequiredResources.Select(y => new ResourceBatch
                            {
                                Id = y.Id,
                                ResourceType = y.ResourceType,
                                Quantity = y.Quantity
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
            return upgradeables;
        }

        public async Task<Upgradeable> GetAsync(Guid id)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            Upgradeable upgradeable = new Upgradeable();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    upgradeable = await session
                        .Query<Upgradeable>()
                        .Where(x => x.Id == id)
                        .Select(x => new Upgradeable
                        {
                            Id = x.Id,
                            TimeToUpgrade = x.TimeToUpgrade,
                            TimeUntilUpgraded = x.TimeUntilUpgraded,
                            IsFinishedUpgrading = x.IsFinishedUpgrading,
                            UpgradeMultiplier = x.UpgradeMultiplier,
                            RequiredResources = x.RequiredResources.Select(y => new ResourceBatch
                            {
                                Id = y.Id,
                                ResourceType = y.ResourceType,
                                Quantity = y.Quantity
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
            return upgradeable;
        }

        public async Task<Upgradeable> UpdateAsync(Upgradeable entity)
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