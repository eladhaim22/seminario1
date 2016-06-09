using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Seminario.Model;
using Seminario.NHibernate.Mapping;
namespace Seminario.NHibernate
{
    public static class FluentNHibernateHelper
    {
        public static ISession OpenSession()
        {
            string connectionString = "Data Source=ELAD\\SQLEXPRESS;Initial Catalog=Seminario;Integrated Security=SSPI;";
            ISessionFactory sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                  .ConnectionString(connectionString).ShowSql()
                   )
                .Mappings(m =>
                          m.FluentMappings
                              .AddFromAssemblyOf<ChequeClassMapping>())
                .ExposeConfiguration(cfg => new SchemaExport(cfg)
                 .Create(false, false))
                .BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}