using Domain;
using Domain.Helper;
using NHibernate;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public async Task<Upgradeable> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Upgradeable> UpdateAsync(Upgradeable entity)
        {
            throw new NotImplementedException();
        }
    }
}