using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Seminario.Model;
using Seminario.NHibernate.Mapping;
using System.Reflection;
using NHibernate.Linq;
using FluentNHibernate.Utils;
using NHibernate.Cfg;
using System.Collections.Generic;
namespace Seminario.NHibernate
{
    public static class SessionFactoryBuilder
    {
        public static ISessionFactory Build(string connectionString)
        {
            ISessionFactory sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2005
                .ConnectionString(connectionString).ShowSql())
                .Mappings(m =>
                {
                    foreach (var classToMap in Assembly.GetExecutingAssembly().DefinedTypes)
                    {
                        if (classToMap.Name.EndsWith("Map"))
                        {
                            m.FluentMappings.Add(classToMap);
                        }
                    }
                })
                .ExposeConfiguration(l => new SchemaExport(l)
                .Create(false, false))
                .BuildSessionFactory();
            return sessionFactory;
        }
    }
}
