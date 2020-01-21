using Shop.DAL.Models;
using Shop.DAL.Interfaces;
using Shop.DAL.Storages;
using System.Collections.Generic;
using System.Linq;

namespace Shop.DAL.Repositories
{
    public class RepositoryOfGoods : IRepository<Goods>
    {
        private readonly Storage storages;

        public RepositoryOfGoods()
        {
            storages = Storage.GetStorages();
        }

        public void Create(Goods item)
        {
            storages.Goods.Add(item);
        }

        public void Delete(int id)
        {
            storages.Goods.RemoveAt(id);
        }

        public Goods Get(int id)
        {
            return storages.Goods[id];
        }

        public IEnumerable<Goods> GetAll()
        {
            return storages.Goods.ToList();
        }

        public void Update(Goods item, int id)
        { 
            storages.Goods[id] = item;
        }

        public void Clear()
        {
            storages.ClearGood();
        }
    }
}
