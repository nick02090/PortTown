﻿using Domain;
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
                        .Select(x => new ResourceBatch
                        {
                            Id = x.Id,
                            ResourceType = x.ResourceType,
                            Size = x.Size,
                            Craftable = x.Craftable
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
                        .Select(x => new ResourceBatch
                        {
                            Id = x.Id,
                            ResourceType = x.ResourceType,
                            Size = x.Size,
                            Craftable = x.Craftable
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
    }
}