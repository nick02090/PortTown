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
    public class ItemRepository : IItemRepository
    {
        public async Task<Item> CreateAsync(Item entity)
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

        public async Task<IEnumerable<Item>> GetAsync()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            List<Item> items = new List<Item>();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    items = await session
                        .Query<Item>()
                        .Select(x => new Item
                        {
                            Id = x.Id,
                            Value = x.Value,
                            Name = x.Name,
                            ParentCraftable = new Craftable
                            {
                                Id = x.ParentCraftable.Id,
                                IsFinishedCrafting = x.ParentCraftable.IsFinishedCrafting,
                                TimeToBuild = x.ParentCraftable.TimeToBuild,
                                TimeUntilCrafted = x.ParentCraftable.TimeUntilCrafted,
                                CraftableType = x.ParentCraftable.CraftableType,
                                RequiredResources = x.ParentCraftable.RequiredResources.Select(y => new ResourceBatch
                                {
                                    Id = y.Id,
                                    ResourceType = y.ResourceType,
                                    Quantity = y.Quantity
                                }).ToList()
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
            return items;
        }

        public async Task<Item> GetAsync(Guid id)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            Item item = new Item();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    item = await session
                        .Query<Item>()
                        .Where(x => x.Id == id)
                        .Select(x => new Item
                        {
                            Id = x.Id,
                            Value = x.Value,
                            Name = x.Name,
                            ParentCraftable = new Craftable
                            {
                                Id = x.ParentCraftable.Id,
                                IsFinishedCrafting = x.ParentCraftable.IsFinishedCrafting,
                                TimeToBuild = x.ParentCraftable.TimeToBuild,
                                TimeUntilCrafted = x.ParentCraftable.TimeUntilCrafted,
                                CraftableType = x.ParentCraftable.CraftableType,
                                RequiredResources = x.ParentCraftable.RequiredResources.Select(y => new ResourceBatch
                                {
                                    Id = y.Id,
                                    ResourceType = y.ResourceType,
                                    Quantity = y.Quantity
                                }).ToList()
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
            return item;
        }

        public async Task<IEnumerable<Item>> GetTemplateAsync()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            List<Item> items = new List<Item>();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    items = await session
                        .Query<Item>()
                        .Where(x => x.Town.Id == null)
                        .Select(x => new Item
                        {
                            Id = x.Id,
                            Value = x.Value,
                            Name = x.Name,
                            ParentCraftable = new Craftable
                            {
                                Id = x.ParentCraftable.Id,
                                IsFinishedCrafting = x.ParentCraftable.IsFinishedCrafting,
                                TimeToBuild = x.ParentCraftable.TimeToBuild,
                                TimeUntilCrafted = x.ParentCraftable.TimeUntilCrafted,
                                CraftableType = x.ParentCraftable.CraftableType,
                                RequiredResources = x.ParentCraftable.RequiredResources.Select(y => new ResourceBatch
                                {
                                    Id = y.Id,
                                    ResourceType = y.ResourceType,
                                    Quantity = y.Quantity
                                }).ToList()
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
            return items;
        }

        public async Task<Item> UpdateAsync(Item entity)
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