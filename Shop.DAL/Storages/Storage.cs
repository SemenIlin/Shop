using Shop.DAL.Models;
using System.Collections.Generic;

namespace Shop.DAL.Storages
{
    public class Storage
    {
        private static Storage storages;

        private Storage()
        {
              Employees = new List<Employees>(4);
              Goods = new List<Goods>(1);
              RentalSpaces = new List<RentalSpaces>(1);
        }

        public static Storage GetStorages()
        {
            if (storages == null)
            {
                storages = new Storage();                
            }

            return storages;                    
        }

        public List<Employees> Employees { get; }
        public List<Goods> Goods { get; }
        public List<RentalSpaces> RentalSpaces { get; }
    }
}
