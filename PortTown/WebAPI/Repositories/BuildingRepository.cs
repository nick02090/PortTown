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
    public class BuildingRepository : IBuildingRepository
    {
        public async Task<Building> CreateAsync(Building entity)
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

        public async Task<IEnumerable<Building>> GetAsync()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            List<Building> buildings = new List<Building>();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    buildings = await session
                        .Query<Building>()
                        .Where(x => x.Town.Id != null)
                        .Select(x => new Building
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Level = x.Level,
                            Capacity = x.Capacity,
                            BuildingType = x.BuildingType,
                            Town = new Town
                            {
                                Id = x.Town.Id
                            },
                            ParentCraftable = new Craftable
                            {
                                Id = x.ParentCraftable.Id,
                                CraftableType = x.ParentCraftable.CraftableType,
                                IsFinishedCrafting = x.ParentCraftable.IsFinishedCrafting,
                                TimeToBuild = x.ParentCraftable.TimeToBuild,
                                TimeUntilCrafted = x.ParentCraftable.TimeUntilCrafted,
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
            return buildings;
        }

        public async Task<Building> GetAsync(Guid id)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            Building building = new Building();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    building = await session
                        .Query<Building>()
                        .Where(x => x.Id == id)
                        .Where(x => x.Town.Id != null)
                        .Select(x => new Building
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Level = x.Level,
                            Capacity = x.Capacity,
                            BuildingType = x.BuildingType,
                            Town = new Town
                            {
                                Id = x.Town.Id
                            },
                            ParentCraftable = new Craftable
                            {
                                Id = x.ParentCraftable.Id,
                                CraftableType = x.ParentCraftable.CraftableType,
                                IsFinishedCrafting = x.ParentCraftable.IsFinishedCrafting,
                                TimeToBuild = x.ParentCraftable.TimeToBuild,
                                TimeUntilCrafted = x.ParentCraftable.TimeUntilCrafted,
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
            return building;
        }

        public async Task<IEnumerable<Building>> GetByTownAsync(Guid townId)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            List<Building> buildings = new List<Building>();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    buildings = await session
                        .Query<Building>()
                        .Where(x => x.Town.Id == townId)
                        .Select(x => new Building
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Level = x.Level,
                            Capacity = x.Capacity,
                            BuildingType = x.BuildingType,
                            Town = new Town
                            {
                                Id = x.Town.Id
                            },
                            ParentCraftable = new Craftable
                            {
                                Id = x.ParentCraftable.Id,
                                CraftableType = x.ParentCraftable.CraftableType,
                                IsFinishedCrafting = x.ParentCraftable.IsFinishedCrafting,
                                TimeToBuild = x.ParentCraftable.TimeToBuild,
                                TimeUntilCrafted = x.ParentCraftable.TimeUntilCrafted,
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
            return buildings;
        }

        public async Task<IEnumerable<Building>> GetTemplateAsync()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            List<Building> buildings = new List<Building>();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    buildings = await session
                        .Query<Building>()
                        .Where(x => x.Town.Id == null)
                        .Select(x => new Building
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Level = x.Level,
                            Capacity = x.Capacity,
                            BuildingType = x.BuildingType,
                            ParentCraftable = new Craftable
                            {
                                Id = x.ParentCraftable.Id,
                                CraftableType = x.ParentCraftable.CraftableType,
                                IsFinishedCrafting = x.ParentCraftable.IsFinishedCrafting,
                                TimeToBuild = x.ParentCraftable.TimeToBuild,
                                TimeUntilCrafted = x.ParentCraftable.TimeUntilCrafted,
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
            return buildings;
        }

        public async Task<Building> GetTemplateAsync(Guid id)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            var building = new Building();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    building = await session
                        .Query<Building>()
                        .Where(x => x.Town.Id == null)
                        .Where(x => x.Id == id)
                        .Select(x => new Building
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Level = x.Level,
                            Capacity = x.Capacity,
                            BuildingType = x.BuildingType,
                            ParentCraftable = new Craftable
                            {
                                Id = x.ParentCraftable.Id,
                                CraftableType = x.ParentCraftable.CraftableType,
                                IsFinishedCrafting = x.ParentCraftable.IsFinishedCrafting,
                                TimeToBuild = x.ParentCraftable.TimeToBuild,
                                TimeUntilCrafted = x.ParentCraftable.TimeUntilCrafted,
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
            return building;
        }

        public async Task<Building> UpdateAsync(Building entity)
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