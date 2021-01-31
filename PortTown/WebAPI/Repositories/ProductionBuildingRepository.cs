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
    public class ProductionBuildingRepository : IProductionBuildingRepository
    {
        public async Task<ProductionBuilding> CreateAsync(ProductionBuilding entity)
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

        public async Task DeleteAsync(ProductionBuilding entity)
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

        public async Task<IEnumerable<ProductionBuilding>> GetAsync()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            List<ProductionBuilding> productionBuildings = new List<ProductionBuilding>();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    productionBuildings = await session
                        .Query<ProductionBuilding>()
                        .Select(x => new ProductionBuilding
                        {
                            Id = x.Id,
                            ResourceProduced = x.ResourceProduced,
                            LastHarvestTime = x.LastHarvestTime,
                            ProductionRate = x.ProductionRate,
                            ParentBuilding = new Building
                            {
                                Id = x.ParentBuilding.Id,
                                Capacity = x.ParentBuilding.Capacity,
                                BuildingType = x.ParentBuilding.BuildingType,
                                Level = x.ParentBuilding.Level,
                                Name = x.ParentBuilding.Name
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
            return productionBuildings;
        }

        public async Task<ProductionBuilding> GetAsync(Guid id)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            ProductionBuilding productionBuilding = new ProductionBuilding();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    productionBuilding = await session
                        .Query<ProductionBuilding>()
                        .Where(x => x.Id == id)
                        .Select(x => new ProductionBuilding
                        {
                            Id = x.Id,
                            ResourceProduced = x.ResourceProduced,
                            LastHarvestTime = x.LastHarvestTime,
                            ProductionRate = x.ProductionRate,
                            ParentBuilding = new Building
                            {
                                Id = x.ParentBuilding.Id,
                                Capacity = x.ParentBuilding.Capacity,
                                BuildingType = x.ParentBuilding.BuildingType,
                                Level = x.ParentBuilding.Level,
                                Name = x.ParentBuilding.Name
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
            return productionBuilding;
        }

        public async Task<ProductionBuilding> GetByBuildingAsync(Guid buildingId)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            ProductionBuilding productionBuilding = new ProductionBuilding();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    productionBuilding = await session
                        .Query<ProductionBuilding>()
                        .Where(x => x.ParentBuilding.Id == buildingId)
                        .Select(x => new ProductionBuilding
                        {
                            Id = x.Id,
                            ResourceProduced = x.ResourceProduced,
                            LastHarvestTime = x.LastHarvestTime,
                            ProductionRate = x.ProductionRate
                        })
                        .SingleOrDefaultAsync();

                    await tx.CommitAsync();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
            return productionBuilding;
        }

        public async Task<ProductionBuilding> GetForDeleteAsync(Guid id)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            ProductionBuilding productionBuilding = new ProductionBuilding();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    productionBuilding = await session
                        .Query<ProductionBuilding>()
                        .Where(x => x.Id == id)
                        .Select(x => new ProductionBuilding
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
            return productionBuilding;
        }

        public async Task<ProductionBuilding> UpdateAsync(ProductionBuilding entity)
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