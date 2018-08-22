using System.Linq;
using Domain;
using Domain.Entities;
using NHibernate;
using NHibernate.Linq;

namespace NHibernateConfigs
{
    public class DataStore<T> : IDataStore<T> where T : PersistingObject
    {
        private ISession OpenSession()
        {
            return SessionFactoryProvider.SessionFactory.OpenSession();
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> queryable;
            var session = OpenSession();
            using (var tr = session.BeginTransaction())
            {
                queryable = session.Query<T>();
                tr.Commit();
            }

            return queryable;
        }

        public T Get(long id)
        {
            T entity = null;
            var session = OpenSession();
            using (var tr = session.BeginTransaction())
            {
                entity = session.Get<T>(id);
                tr.Commit();
            }

            return entity;
        }

        public void Save(T entity)
        {
            var session = OpenSession();
            using (var tr = session.BeginTransaction())
            {
                session.Save(entity);
                tr.Commit();
            }
        }

        public void Update(T entity)
        {
            var session = OpenSession();
            using (var tr = session.BeginTransaction())
            {
                session.Update(entity);
                tr.Commit();
            }
        }

        public void Delete(T entity)
        {
            var session = OpenSession();
            using (var tr = session.BeginTransaction())
            {
                session.Delete(entity);
                tr.Commit();
            }
        }
    }
}