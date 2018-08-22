using System.Linq;
using Domain.Entities;

namespace Domain
{
    public interface IDataStore<T> where T : PersistingObject
    {
        IQueryable<T> GetAll();

        T Get(long id);

        void Save(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}