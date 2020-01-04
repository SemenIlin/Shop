using Shop.DAL.Models;

namespace Shop.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Employees> Employees{ get; }
        IRepository<Goods> Goods { get; }
        IRepository<RentalSpaces> RentalSpaces { get; }
    }
}
