using Shop.DAL.Models;
using Shop.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Shop.DAL.Repositories
{
    public class RepositoryOfGoodsList : IRepository<Goods>
    {
        private readonly List<Goods> goods;

        public RepositoryOfGoodsList()
        {
            goods = new List<Goods>();
        }

        public void Create(Goods item)
        {
            goods.Add(item);
        }

        public void Delete(int id)
        {
            goods.RemoveAt(id);
        }

        public Goods Get(int id)
        {
            return goods[id];
        }

        public IEnumerable<Goods> GetAll()
        {
            return goods.ToList();
        }

        public void Update(Goods item, int id)
        {
            goods[id] = item;
        }

        public void Clear()
        {
            goods.Clear();
        }
    }
}
