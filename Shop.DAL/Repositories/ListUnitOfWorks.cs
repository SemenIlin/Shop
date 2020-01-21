using Shop.DAL.Models;
using Shop.DAL.Interfaces;

namespace Shop.DAL.Repositories
{
    public class ListUnitOfWorks : IUnitOfWork
    {
        private IRepository<Employees> employees;
        private IRepository<Goods> goods;
        private IRepository<RentalSpaces> rentalSpace;

        
        public IRepository<Employees> Employees
        {
            get
            {
                if(employees == null)
                {
                    employees = new RepositoryOfEmployees();
                }

                return employees;
            }
        }

        public IRepository<Goods> Goods
        {
            get
            {
                if(goods == null)
                {
                    goods = new RepositoryOfGoods();
                }

                return goods;
            } 
        }

        public IRepository<RentalSpaces> RentalSpaces
        {
            get
            {
                if (rentalSpace == null)
                {
                    rentalSpace = new RepositoryOfRentalSpace();                
                }

                return rentalSpace;
            }
        }
    }
}
