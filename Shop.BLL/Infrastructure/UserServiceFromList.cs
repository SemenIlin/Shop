using Shop.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using Shop.DAL.Analytics;
using Shop.DAL.Interfaces;

namespace Shop.BLL.Infrastructure
{
    public class UserServiceFromList
    {
        private readonly IUnitOfWork unitOfWorks;
        private AnalyticsOfShop analyticsOfShop;

        private decimal margin;
        private decimal totalRevenueFromSales;
        private decimal totalExpenceForPurchase;

        public UserServiceFromList(IUnitOfWork unitOfWorks)
        {
            this.unitOfWorks = unitOfWorks;
        }

        public Delta Delta { get; private set; }
        public AnalyticsOfShop AnalyticsOfShop
        {
            get
            {
                if(analyticsOfShop == null)
                {
                    decimal totalPurchasePriceOfGood = GetGoods().Sum(g => g.TotalExpensesForPurchase);
                    decimal totalRentalSpace = GetRentalSpaces().Sum(r => r.Rental);
                    decimal totalSalary = GetEmployees().Sum(e => e.Salary);
                    decimal revenue = GetGoods().Sum(g => g.TotalRevenueFromSales);
                    decimal totalExpenses = totalSalary + totalRentalSpace + totalPurchasePriceOfGood;

                    analyticsOfShop = new AnalyticsOfShop
                    {
                        Revenue = revenue,
                        TotalPurchasePriceOfGood = totalPurchasePriceOfGood,
                        TotalRentalSpace = totalRentalSpace,
                        TotalSalary = totalSalary,
                        TotalExpenses = totalExpenses,
                        DeltaFromSales = revenue - totalExpenses
                    };
                }

                return analyticsOfShop;
            }
        }

        public void AddEmployee(string position, decimal salary)
        {
            unitOfWorks.Employees.Create(new Employees { Position = position, Salary = salary });
        }

        public void UpdateEmployee(string position, decimal salary, int id)
        {
            unitOfWorks.Employees.Update(new Employees { Position = position, Salary = salary }, id);
        }

        public void DeleteEmployee(int id)
        {
            unitOfWorks.Employees.Delete(id);
        }

        public Employees GetEmployee(int id)
        {
            return unitOfWorks.Employees.Get(id);
        }

        public List<Employees> GetEmployees()
        {
            return unitOfWorks.Employees.GetAll().ToList();
        }

        public void CreateGood(decimal purchacePrice, decimal salePrice, int count)
        {
            margin = salePrice - purchacePrice;
            totalRevenueFromSales = salePrice * count;
            totalExpenceForPurchase = purchacePrice * count;

            unitOfWorks.Goods.Create(new Goods
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

            unitOfWorks.Goods.Update(new Goods
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
            unitOfWorks.Goods.Delete(id);
        }

        public Goods Good(int id)
        {
            return unitOfWorks.Goods.Get(id);
        }

        public List<Goods> GetGoods()
        {
            return unitOfWorks.Goods.GetAll().ToList();
        }

        public void CreateRentalSpace(string title, decimal rental)
        {
            unitOfWorks.RentalSpaces.Create(new RentalSpaces { Title = title, Rental = rental });
        }

        public void UpdateRentalSpace(string title, decimal rental, int id)
        {
            unitOfWorks.RentalSpaces.Update(new RentalSpaces { Rental = rental, Title = title }, id);
        }

        public void DeleteRentalSpace(int id)
        {
            unitOfWorks.RentalSpaces.Delete(id);
        }

        public RentalSpaces RentalSpace(int id)
        {
            return unitOfWorks.RentalSpaces.Get(id);
        }

        public List<RentalSpaces> GetRentalSpaces()
        {
            return unitOfWorks.RentalSpaces.GetAll().ToList();
        }

        public void CalculateRevenue(int month = 1)
        {            
            if (month == 1)
            {
                Delta = new Delta(AnalyticsOfShop);
            }
            else
            {
                Delta = new Delta(AnalyticsOfShop, month);
            }
        }

        public void Clear()
        {
            unitOfWorks.Employees.Clear();
            unitOfWorks.Goods.Clear();
            unitOfWorks.RentalSpaces.Clear();
        }
    }
}