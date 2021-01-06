﻿using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System.Web;

namespace Domain.Helper
{
    public sealed class NHibernateHelper
    {
        private static readonly string pathToDB = "D:\\FER\\dipl\\3.semestar\\OO\\baza\\PortTownDb.mdf";
        private static readonly bool deleteShemaOnStart = true;

        private const string CurrentSessionKey = "nhibernate.current_session";
        private static readonly string connectionString = $"Server=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={pathToDB};Integrated Security=True";
        private static readonly ISessionFactory _sessionFactory;

        static NHibernateHelper()
        {
            if (deleteShemaOnStart)
            {
                _sessionFactory = Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2008.ConnectionString(connectionString).ShowSql)
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Town>())
                    .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(false, false))
                    .BuildSessionFactory();
            }
            else
            {
                _sessionFactory = Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2008.ConnectionString(connectionString).ShowSql)
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Town>())
                    .ExposeConfiguration(cfg => new SchemaUpdate(cfg))
                    .BuildSessionFactory();
            }
        }

        public static ISession GetCurrentSession()
        {
            var context = HttpContext.Current;

            if (!(context.Items[CurrentSessionKey] is ISession currentSession))
            {
                currentSession = _sessionFactory.OpenSession();
                context.Items[CurrentSessionKey] = currentSession;
            }

            return currentSession;
        }

        public static void CloseSession()
        {
            var context = HttpContext.Current;

            if (!(context.Items[CurrentSessionKey] is ISession currentSession))
            {
                // No current session
                return;
            }

            currentSession.Close();
            context.Items.Remove(CurrentSessionKey);
        }

        public static void CloseSessionFactory()
        {
            if (_sessionFactory != null)
            {
                _sessionFactory.Close();
            }
        }
    }
}
