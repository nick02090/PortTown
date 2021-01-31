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
    public class StorageRespository : IStorageRepository
    {
        public async Task<Storage> CreateAsync(Storage entity)
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

        public async Task DeleteAsync(Storage entity)
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

        public async Task<IEnumerable<Storage>> GetAsync()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            List<Storage> storages = new List<Storage>();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    storages = await session
                        .Query<Storage>()
                        .Select(x => new Storage
                        {
                            Id = x.Id,
                            StoredResources = x.StoredResources.Select(y => new ResourceBatch
                            {
                                Id = y.Id,
                                Quantity = y.Quantity,
                                ResourceType = y.ResourceType
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
            return storages;
        }

        public async Task<Storage> GetAsync(Guid id)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            Storage storage = new Storage();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    storage = await session
                        .Query<Storage>()
                        .Where(x => x.Id == id)
                        .Select(x => new Storage
                        {
                            Id = x.Id,
                            StoredResources = x.StoredResources.Select(y => new ResourceBatch
                            {
                                Id = y.Id,
                                Quantity = y.Quantity,
                                ResourceType = y.ResourceType
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
            return storage;
        }

        public async Task<Storage> GetByBuildingAsync(Guid buildingId)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            Storage storage = new Storage();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    storage = await session
                        .Query<Storage>()
                        .Where(x => x.ParentBuilding.Id == buildingId)
                        .Select(x => new Storage
                        {
                            Id = x.Id,
                            StoredResources = x.StoredResources.Select(y => new ResourceBatch
                            {
                                Id = y.Id,
                                Quantity = y.Quantity,
                                ResourceType = y.ResourceType
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
            return storage;
        }

        public async Task<Storage> GetForDeleteAsync(Guid id)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            Storage storage = new Storage();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    storage = await session
                        .Query<Storage>()
                        .Where(x => x.Id == id)
                        .Select(x => new Storage
                        {
                            Id = x.Id
                        })
                        .SingleOrDefaultAsync();

                    await tx.CommitAsync();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
            return storage;
        }

        public async Task<Storage> UpdateAsync(Storage entity)
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