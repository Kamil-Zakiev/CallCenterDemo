using System.Linq;
using Domain;
using Domain.Entities;
using NHibernate;
using NHibernate.Linq;

namespace NHibernateConfigs
{
    public class DataStore<T> : IDataStore<T> where T : PersistingObject
    {
        public ISession CurrentSession { get; set; }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> queryable;
            using (var tr = CurrentSession.BeginTransaction())
            {
                queryable = CurrentSession.Query<T>();
                tr.Commit();
            }

            return queryable;
        }

        public T Get(long id)
        {
            T entity = null;
            using (var tr = CurrentSession.BeginTransaction())
            {
                entity = CurrentSession.Get<T>(id);
                tr.Commit();
            }

            return entity;
        }

        public void Save(T entity)
        {
            using (var tr = CurrentSession.BeginTransaction())
            {
                CurrentSession.Save(entity);
                tr.Commit();
            }
        }

        public void Update(T entity)
        {
            using (var tr = CurrentSession.BeginTransaction())
            {
                CurrentSession.Update(entity);
                tr.Commit();
            }
        }

        public void Delete(T entity)
        {
            using (var tr = CurrentSession.BeginTransaction())
            {
                CurrentSession.Delete(entity);
                tr.Commit();
            }
        }
    }
}