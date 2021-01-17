﻿using Domain;
using Domain.Helper;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebAPI.Interfaces;

namespace WebAPI.Repositories
{
    public class UserRespository : IUserRespository
    {
        public async Task<User> CreateAsync(User entity)
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

        public async Task<IEnumerable<User>> GetAsync()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            List<User> users = new List<User>();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    users = await session
                        .Query<User>()
                        .Select(x => new User
                        {
                            Id = x.Id,
                            Username = x.Username,
                            Email = x.Email,
                            Password = x.Password,
                            Town = x.Town
                        })
                        .ToListAsync();

                    await tx.CommitAsync();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
            return users;
        }

        public async Task<User> GetAsync(Guid id)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            User user = new User();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    user = await session
                        .Query<User>()
                        .Where(x => x.Id == id)
                        .Select(x => new User
                        {
                            Id = x.Id,
                            Username = x.Username,
                            Email = x.Email,
                            Password = x.Password,
                            Town = x.Town
                        })
                        .SingleOrDefaultAsync();

                    await tx.CommitAsync();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
            return user;
        }

        public async Task<User> UpdateAsync(User entity)
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