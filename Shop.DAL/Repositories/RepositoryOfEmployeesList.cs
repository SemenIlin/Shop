using Shop.DAL.Models;
using Shop.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Shop.DAL.Repositories
{
    public class RepositoryOfEmployeesList : IRepository<Employees>        
    {
        private readonly List<Employees> employees;
        
        public RepositoryOfEmployeesList()
        {
            employees = new List<Employees>();
        }

        public void Create(Employees item)
        {
            employees.Add(item);
        }

        public void Delete(int id)
        {
            employees.RemoveAt(id);
        }

        public Employees Get(int id)
        {
            return employees[id];
        }

        public IEnumerable<Employees> GetAll()
        {
            return employees.ToList();
        }

        public void Update(Employees item, int id)
        {
            employees[id] = item;
        }

        public void Clear()
        {
            employees.Clear();
        }
    }
}
