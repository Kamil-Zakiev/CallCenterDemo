using System;
using System.Linq;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using NHibernateConfigs.Maps;

namespace NHibernateConfigs
{
    public static class SessionFactoryProvider
    {
        private static readonly object LockObject = new object();
        
        public static ISessionFactory SessionFactory { get; private set; }

        public static void InitConfig(string connectionString)
        {
            if (SessionFactory != null)
            {
                return;
            }

            lock (LockObject)
            {
                if (SessionFactory == null)
                {
                    InnerInit(connectionString);
                }
            }
        }

        private static void InnerInit(string connectionString)
        {
            var cfg = new Configuration();

            cfg.DataBaseIntegration(d =>
            {
                d.Dialect<PostgreSQLDialect>();
                d.ConnectionString = connectionString;
                d.LogSqlInConsole = true;
            });

            var modelMapper = new ModelMapper();
            var mapClasses = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assem => assem.GetExportedTypes())
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType)
                .Where(type => typeof(PersistingObjectMap<>) == type.BaseType.GetGenericTypeDefinition())
                .ToArray();

            foreach (var mapClass in mapClasses)
            {
                modelMapper.AddMapping(mapClass);
            }

            cfg.AddMapping(modelMapper.CompileMappingForAllExplicitlyAddedEntities());
            SessionFactory = cfg.BuildSessionFactory();
        }
    }
}