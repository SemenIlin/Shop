using Shop.DAL.Models;
using Shop.DAL.Interfaces;
using Shop.DAL.Storages;
using System.Collections.Generic;
using System.Linq;

namespace Shop.DAL.Repositories
{
    public class RepositoryOfRentalSpace : IRepository<RentalSpaces>
    {
        private readonly Storage storages;

        public RepositoryOfRentalSpace()
        {
            storages = Storage.GetStorages();        
        }

        public void Create(RentalSpaces item)
        {
            storages.RentalSpaces.Add(item);
        }

        public void Delete(int id)
        {
            storages.RentalSpaces.RemoveAt(id);
        }

        public RentalSpaces Get(int id)
        {
            return storages.RentalSpaces[id];
        }

        public IEnumerable<RentalSpaces> GetAll()
        {
            return storages.RentalSpaces.ToList();
        }

        public void Update(RentalSpaces item, int id)
        {
            storages.RentalSpaces[id] = item;
        }

        public void Clear()
        {
            storages.ClearRentalSpace();
        }
    }
}
