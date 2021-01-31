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
    public class ResourceBatchRepository : IResourceBatchRepository
    {
        public async Task<ResourceBatch> CreateAsync(ResourceBatch entity)
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

        public async Task DeleteAsync(ResourceBatch entity)
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

        public async Task<IEnumerable<ResourceBatch>> GetAsync()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            List<ResourceBatch> resourceBatches = new List<ResourceBatch>();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    resourceBatches = await session
                        .Query<ResourceBatch>()
                        .Where(x => x.Sellable == null)
                        .Select(x => new ResourceBatch
                        {
                            Id = x.Id,
                            ResourceType = x.ResourceType,
                            Quantity = x.Quantity
                        })
                        .ToListAsync();

                    await tx.CommitAsync();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
            return resourceBatches;
        }

        public async Task<ResourceBatch> GetAsync(Guid id)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            ResourceBatch resourceBatch = new ResourceBatch();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    resourceBatch = await session
                        .Query<ResourceBatch>()
                        .Where(x => x.Id == id)
                        .Where(x => x.Sellable == null)
                        .Select(x => new ResourceBatch
                        {
                            Id = x.Id,
                            ResourceType = x.ResourceType,
                            Quantity = x.Quantity
                        })
                        .SingleOrDefaultAsync();

                    await tx.CommitAsync();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
            return resourceBatch;
        }

        public async Task<IEnumerable<ResourceBatch>> GetByCraftableAsync(Guid craftableId)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            List<ResourceBatch> resourceBatches = new List<ResourceBatch>();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    resourceBatches = await session
                        .Query<ResourceBatch>()
                        .Where(x => x.Craftable.Id == craftableId)
                        .Select(x => new ResourceBatch
                        {
                            Id = x.Id,
                            ResourceType = x.ResourceType,
                            Quantity = x.Quantity,
                            Craftable = new Craftable
                            {
                                Id = x.Craftable.Id
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
            return resourceBatches;
        }

        public async Task<IEnumerable<ResourceBatch>> GetByUpgradeableAsync(Guid upgradeableId)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            List<ResourceBatch> resourceBatches = new List<ResourceBatch>();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    resourceBatches = await session
                        .Query<ResourceBatch>()
                        .Where(x => x.Upgradeable.Id == upgradeableId)
                        .Select(x => new ResourceBatch
                        {
                            Id = x.Id,
                            ResourceType = x.ResourceType,
                            Quantity = x.Quantity
                        })
                        .ToListAsync();

                    await tx.CommitAsync();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
            return resourceBatches;
        }

        public async Task<IEnumerable<ResourceBatch>> GetByStorageAsync(Guid storageId)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            List<ResourceBatch> resourceBatches = new List<ResourceBatch>();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    resourceBatches = await session
                        .Query<ResourceBatch>()
                        .Where(x => x.Storage.Id == storageId)
                        .Select(x => new ResourceBatch
                        {
                            Id = x.Id,
                            ResourceType = x.ResourceType,
                            Quantity = x.Quantity
                        })
                        .ToListAsync();

                    await tx.CommitAsync();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
            return resourceBatches;
        }

        public async Task<ResourceBatch> GetTemplateAsync(Guid id)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            var resourceBatch = new ResourceBatch();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    resourceBatch = await session
                        .Query<ResourceBatch>()
                        .Where(x => x.Id == id)
                        .Where(x => x.Sellable.Id != null)
                        .Select(x => new ResourceBatch
                        {
                            Id = x.Id,
                            ResourceType = x.ResourceType,
                            Quantity = x.Quantity,
                            Sellable = new Sellable
                            {
                                Id = x.Sellable.Id,
                                Price = x.Sellable.Price
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
            return resourceBatch;
        }

        public async Task<IEnumerable<ResourceBatch>> GetTemplateAsync()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            List<ResourceBatch> resourceBatches = new List<ResourceBatch>();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    resourceBatches = await session
                        .Query<ResourceBatch>()
                        .Where(x => x.Sellable.Id != null)
                        .Select(x => new ResourceBatch
                        {
                            Id = x.Id,
                            ResourceType = x.ResourceType,
                            Quantity = x.Quantity,
                            Sellable = new Sellable
                            {
                                Id = x.Sellable.Id,
                                Price = x.Sellable.Price
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
            return resourceBatches;
        }

        public async Task<ResourceBatch> UpdateAsync(ResourceBatch entity)
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

        public async Task<ResourceBatch> GetForDeleteAsync(Guid id)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            ResourceBatch resourceBatch = new ResourceBatch();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    resourceBatch = await session
                        .Query<ResourceBatch>()
                        .Where(x => x.Id == id)
                        .Select(x => new ResourceBatch
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
            return resourceBatch;
        }
    }
}