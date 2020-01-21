using Shop.DAL.Models;
using Shop.DAL.Interfaces;
using Shop.DAL.Storages;
using System.Collections.Generic;
using System.Linq;

namespace Shop.DAL.Repositories
{
    public class RepositoryOfEmployees : IRepository<Employees>
    {
        private readonly Storage storages;

        public RepositoryOfEmployees()
        {
            storages = Storage.GetStorages();
        }

        public void Create(Employees item)
        {
            storages.Employees.Add(item);
        }

        public void Delete(int id)
        {
            storages.Employees.RemoveAt(id);
        }

        public Employees Get(int id)
        {
            return storages.Employees[id];
        }

        public IEnumerable<Employees> GetAll()
        {
            return storages.Employees.ToList();
        }

        public void Update(Employees item, int id)
        {
            storages.Employees[id] = item;
        }

        public void Clear()
        {
            storages.ClearEmployee();
        }
    }
}
