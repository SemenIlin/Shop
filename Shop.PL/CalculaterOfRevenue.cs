using Shop.DAL;
using Shop.DAL.Expenses;
using Shop.DAL.Models;
using Shop.DAL.Revenue;
using Shop.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;
using Shop.DAL.Analytics;

namespace Shop.PL
{
    public class CalculaterOfRevenue
    {
        private readonly ListUnitOfWorks listUnitOfWorks = new ListUnitOfWorks();

        decimal margin;
        decimal totalRevenueFromSales;
        decimal totalExpenceForPurchase;

        public CalculaterOfRevenue(Account account)
        {
             Account = account;

            Toys = new List<Goods>();
            Expenses = new List<Expenses>();

            TotalConst = new TotalConstExpenses();
            TotalVariable = new TotalVariableExpenses();
            TotalExpenses = new TotalExpenses(TotalConst, TotalVariable);

            Revenue = new RevenueFromSales();
        }

        public Account Account { get; private set; }

        public Delta Delta { get; private set; }
        public TotalExpenses TotalExpenses { get; private set; }
        public TotalConstExpenses TotalConst { get; private set; }
        public TotalVariableExpenses TotalVariable { get; private set; }
        public RevenueFromSales Revenue { get; private set; }
        public List<Goods> Toys { get; private set; }
        public List<Expenses> Expenses { get; private set; }

        public void AddEmployee(string position, decimal salary)
        {
            listUnitOfWorks.Employees.Create(new Employees { Position = position, Salary = salary });
        }

        public void UpdateEmployee(string position, decimal salary, int id)
        {
            listUnitOfWorks.Employees.Update(new Employees { Position = position, Salary = salary }, id);
        }

        public void DeleteEmployee(int id)
        {
            listUnitOfWorks.Employees.Delete(id);
        }

        public Employees GetEmployee(int id)
        {
            return listUnitOfWorks.Employees.Get(id);
        }

        public List<Employees> GetEmployees()
        {
            return listUnitOfWorks.Employees.GetAll().ToList();
        }

        public void CreateGood(decimal purchacePrice, decimal salePrice, int count)
        {
            margin = salePrice - purchacePrice;
            totalRevenueFromSales = salePrice * count;
            totalExpenceForPurchase = purchacePrice * count;

            listUnitOfWorks.Goods.Create(new Goods
            {
                PurchasePrice = purchacePrice,
                SalePrice = salePrice,
                Count = count,
                Margin = margin,
                TotalExpensesForPurchase = totalExpenceForPurchase,
                TotalRevenueFromSales = totalRevenueFromSales
            });
        }

        public void UpdateGood(decimal purchasePrice, decimal salePrice, int count, int id)
        {
            margin = salePrice - purchasePrice;
            totalRevenueFromSales = salePrice * count;
            totalExpenceForPurchase = purchasePrice * count;

            listUnitOfWorks.Goods.Update(new Goods
            {
                PurchasePrice = purchasePrice,
                SalePrice = salePrice,
                Count = count,
                Margin = margin,
                TotalExpensesForPurchase = totalExpenceForPurchase,
                TotalRevenueFromSales = totalRevenueFromSales
            }, id);
        }

        public void DeleteGood(int id)
        {
            listUnitOfWorks.Goods.Delete(id);
        }

        public Goods Good(int id)
        {
            return listUnitOfWorks.Goods.Get(id);
        }

        public List<Goods> GetGoods()
        {
            return listUnitOfWorks.Goods.GetAll().ToList();
        }

        public void CreateRentalSpace(string title, decimal rental)
        {
            listUnitOfWorks.RentalSpaces.Create(new RentalSpaces { Title = title, Rental = rental });
        }

        public void UpdateRentalSpace(string title, decimal rental, int id)
        {
            listUnitOfWorks.RentalSpaces.Update(new RentalSpaces { Rental = rental, Title = title }, id);
        }

        public void DeleteRentalSpace(int id)
        {
            listUnitOfWorks.RentalSpaces.Delete(id);
        }

        public RentalSpaces RentalSpace(int id)
        {
            return listUnitOfWorks.RentalSpaces.Get(id);
        }

        public List<RentalSpaces> GetRentalSpace()
        {
            return listUnitOfWorks.RentalSpaces.GetAll().ToList();
        }

        public void CalculateRevenue(decimal budget, int month = 1)
        {            
            if (month == 1)
            {
                Delta = new Delta(TotalExpenses, Revenue);
            }
            else
            {
                Delta = new Delta(budget, TotalConst, GetGoods()[0], month, Toys, Expenses);
            }

            Account.UserSignInDTO.Employees = GetEmployees();
            Account.UserSignInDTO.Goods = GetGoods();
            Account.UserSignInDTO.RentalSpaces = GetRentalSpace();
            Account.UserSignInDTO.TotalRevenue = Toys.Sum(t=>t.TotalRevenueFromSales);
            Account.UserSignInDTO.TotalExpenses = Expenses.Sum(u => u.TotalExpenses); 
            Account.UserSignInDTO.Delta = Delta.DeltaFromShop;
            Account.UserSignInDTO.Budget = budget;
            Account.UserSignInDTO.Month = month;
            Account.CreateRecord();
        }

        public void Clear()
        {
            listUnitOfWorks.Employees.Clear();
            listUnitOfWorks.Goods.Clear();
            listUnitOfWorks.RentalSpaces.Clear();
        }
    }
}