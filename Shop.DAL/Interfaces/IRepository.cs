using System.Collections.Generic;

namespace Shop.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Create(T item);
        void Update(T item, int id);
        void Delete(int id);
        void Clear();
    }
}
