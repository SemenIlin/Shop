using Shop.DAL.Models;
using Shop.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Shop.DAL.Repositories
{
    public class RepositoryOfRentalSpaceList : IRepository<RentalSpaces>
    {
        private readonly List<RentalSpaces> rentalSpaces;

        public RepositoryOfRentalSpaceList()
        {
            rentalSpaces = new List<RentalSpaces>();        
        }

        public void Create(RentalSpaces item)
        {
            rentalSpaces.Add(item);
        }

        public void Delete(int id)
        {
            rentalSpaces.RemoveAt(id);
        }

        public RentalSpaces Get(int id)
        {
            return rentalSpaces[id];
        }

        public IEnumerable<RentalSpaces> GetAll()
        {
            return rentalSpaces.ToList();
        }

        public void Update(RentalSpaces item, int id)
        {
            rentalSpaces[id] = item;
        }

        public void Clear()
        {
            rentalSpaces.Clear();
        }
    }
}
