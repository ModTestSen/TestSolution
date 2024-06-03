using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace TestSolution.DataPersistence
{
    /// <summary>
    /// 
    /// </summary>
    public class SessionFactory
    {
        private static ISessionFactory? _sessionFactory;

        public static ISession OpenSession()
        {
            return _sessionFactory?.OpenSession() ?? throw new InvalidOperationException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public static void Init(string connectionString)
        {
            _sessionFactory = BuildSessionFactory(connectionString);
        }

        private static ISessionFactory? BuildSessionFactory(string connectionString)
        {
            var sessionFactory = Fluently
                .Configure()
                .Database(PostgreSQLConfiguration.PostgreSQL81
                    .ConnectionString(c => c.Is(connectionString))
                    .ShowSql())
                .Mappings(m =>
                {
                    foreach (var classType in GetClassesFromNamespace())
                    {
                        m.FluentMappings.Add(classType);
                    }
                })
                .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true))
                .BuildSessionFactory();

            return sessionFactory;
        }

        private static List<Type> GetClassesFromNamespace()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            var types = asm.GetTypes()
                .Where(type => type.Namespace == "TestSolution.Models.Mappings" && type.FullName.EndsWith("Map"))
                .Select(type => type).ToList();

            return types;
        }
    }
}