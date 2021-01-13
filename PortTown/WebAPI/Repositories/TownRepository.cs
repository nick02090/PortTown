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
        public Task<Town> CreateAsync(Town entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Town>> GetAsync()
        {
            throw new NotImplementedException();
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
            return town;
        }

        public Task<Town> UpdateAsync(Town entity)
        {
            throw new NotImplementedException();
        }


        //private async Task<List<Town>> GetTownsAsync()
        //{
        //    ISession session = NHibernateHelper.GetCurrentSession();
        //    List<Town> towns = new List<Town>();
        //    try
        //    {
        //        using (var tx = session.BeginTransaction())
        //        {
        //            towns = await session
        //                .Query<Town>()
        //                .Select(x => new Town
        //                {
        //                    Id = x.Id,
        //                    Name = x.Name,
        //                    Level = x.Level,
        //                    Buildings = x.Buildings.Select(y => new Building
        //                    {
        //                        Id = y.Id
        //                    }).ToList()
        //                })
        //                .ToListAsync();

        //            await tx.CommitAsync();
        //        }
        //    }
        //    finally
        //    {
        //        NHibernateHelper.CloseSession();
        //    }
        //    return towns;
        //}

        //private async Task<List<ProductionBuilding>> GetProdsAsync(Guid townId)
        //{
        //    ISession session = NHibernateHelper.GetCurrentSession();
        //    List<ProductionBuilding> prods = new List<ProductionBuilding>();
        //    try
        //    {
        //        using (var tx = session.BeginTransaction())
        //        {
        //            prods = await session
        //                .Query<ProductionBuilding>()
        //                .Where(x => x.ParentBuilding.Town.Id.Equals(townId) && x.ParentBuilding.BuildingType == BuildingType.Production)
        //                .Select(x => new ProductionBuilding(x.ParentBuilding)
        //                {
        //                    Id = x.Id,
        //                    ResourceProduced = x.ResourceProduced,
        //                    ProductionRate = x.ProductionRate,
        //                    LastHarvestTime = x.LastHarvestTime
        //                    //RequiredResources = x.ParentBuilding.ParentCraftable.RequiredResources.Select(y => new ResourceBatch
        //                    //{
        //                    //    Id = y.Id
        //                    //}).ToList()
        //                })
        //                .ToListAsync();

        //            await tx.CommitAsync();
        //        }
        //    }
        //    finally
        //    {
        //        NHibernateHelper.CloseSession();
        //    }
        //    return prods;
        //}

        //private async Task<Town> AddingTownMock()
        //{
        //    ISession session = NHibernateHelper.GetCurrentSession();
        //    var spajicGrad = new Town
        //    {
        //        Name = "SpajicGrad"
        //    };
        //    try
        //    {
        //        using (ITransaction tx = session.BeginTransaction())
        //        {
        //            await session.SaveAsync(spajicGrad);
        //            await tx.CommitAsync();
        //        }
        //    }
        //    finally
        //    {
        //        NHibernateHelper.CloseSession();
        //    }
        //    return spajicGrad;
        //}

        //private async Task<ProductionBuilding> AddingProdMock(Town town)
        //{
        //    ISession session = NHibernateHelper.GetCurrentSession();
        //    var craftable = new Craftable();
        //    var building = new Building
        //    {
        //        Name = "Zlatara",
        //        Town = town
        //    };
        //    ProductionBuilding res;
        //    try
        //    {
        //        using (ITransaction tx = session.BeginTransaction())
        //        {
        //            await session.SaveAsync(craftable);
        //            building.SetParentCraftable(craftable);
        //            await session.SaveAsync(building);
        //            var prod = new ProductionBuilding(building);
        //            await session.SaveAsync(prod);
        //            res = prod;
        //            await tx.CommitAsync();
        //        }
        //    }
        //    finally
        //    {
        //        NHibernateHelper.CloseSession();
        //    }
        //    return res;
        //}
    }
}