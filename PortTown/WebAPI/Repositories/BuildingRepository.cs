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