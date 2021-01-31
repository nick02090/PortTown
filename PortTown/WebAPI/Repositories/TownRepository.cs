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
    public class TownRepository : ITownRepository
    {
        public async Task<Town> CreateAsync(Town entity)
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

        public async Task DeleteAsync(Town entity)
        {
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

        public async Task<IEnumerable<Town>> GetAsync()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            List<Town> towns = new List<Town>();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    towns = await session
                        .Query<Town>()
                        .Select(x => new Town
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Level = x.Level,
                            Buildings = x.Buildings.Select(y => new Building
                            {
                                Id = y.Id,
                                Name = y.Name,
                                BuildingType = y.BuildingType,
                                Capacity = y.Capacity,
                                Level = y.Level
                            }).ToList(),
                            User = new User
                            {
                                Id = x.User.Id,
                                Username = x.User.Username,
                                Email = x.User.Email
                            },
                            Upgradeable = new Upgradeable
                            {
                                Id = x.Upgradeable.Id,
                                UpgradeMultiplier = x.Upgradeable.UpgradeMultiplier,
                                IsFinishedUpgrading = x.Upgradeable.IsFinishedUpgrading,
                                TimeToUpgrade = x.Upgradeable.TimeToUpgrade,
                                TimeUntilUpgraded = x.Upgradeable.TimeUntilUpgraded
                            }
                        })
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

        public async Task<Town> GetAsync(Guid id)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            Town town = new Town();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    town = await session
                        .Query<Town>()
                        .Where(x => x.Id == id)
                        .Select(x => new Town
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Level = x.Level,
                            Buildings = x.Buildings.Select(y => new Building
                            {
                                Id = y.Id,
                                Name = y.Name,
                                BuildingType = y.BuildingType,
                                Capacity = y.Capacity,
                                Level = y.Level
                            }).ToList(),
                            User = new User
                            {
                                Id = x.User.Id,
                                Username = x.User.Username,
                                Email = x.User.Email
                            },
                            Upgradeable = new Upgradeable
                            {
                                Id = x.Upgradeable.Id,
                                UpgradeMultiplier = x.Upgradeable.UpgradeMultiplier,
                                IsFinishedUpgrading = x.Upgradeable.IsFinishedUpgrading,
                                TimeToUpgrade = x.Upgradeable.TimeToUpgrade,
                                TimeUntilUpgraded = x.Upgradeable.TimeUntilUpgraded
                            }
                        })
                        .SingleOrDefaultAsync();

                    await tx.CommitAsync();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
            return town;
        }

        public async Task<Town> GetForDeleteAsync(Guid id)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            Town town = new Town();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    town = await session
                        .Query<Town>()
                        .Where(x => x.Id == id)
                        .Select(x => new Town
                        {
                            Id = x.Id,
                        })
                        .SingleOrDefaultAsync();

                    await tx.CommitAsync();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
            return town;
        }

        public async Task<Town> UpdateAsync(Town entity)
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