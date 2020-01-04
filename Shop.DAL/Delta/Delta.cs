using Shop.DAL.Expenses;
using Shop.DAL.Models;
using Shop.DAL.Repositories;
using Shop.DAL.Revenue;
using System.Collections.Generic;

namespace Shop.DAL
{
    public class Delta
    {
        private readonly ListUnitOfWorks storages;
        private readonly List<Goods> goods;
        private readonly List<Analytics.Expenses> expenses;

        private decimal delta = 0;
        private int numbersOfToys = 0;

        public Delta(TotalExpenses total, RevenueFromSales revenueFromSales)//const number of toys every month
        { 
            DeltaFromShop = revenueFromSales.GetRevenue() - total.GetExpenses();                    
        }

        public Delta(decimal budget, TotalConstExpenses totalConst, Goods good, int numberOfMonth, List<Goods> goods, List<Analytics.Expenses> expenses)
        {
            this.goods = goods;
            this.expenses = expenses;
            storages = new ListUnitOfWorks();

            decimal totalConstExpenses = totalConst.GetTotalExpensesForEmployees() + totalConst.GetTotalExpensesForRentalSpace();

            DeltaFromShop = GetDelta(budget, totalConst, good, numberOfMonth) - goods[0].TotalExpensesForPurchase - totalConstExpenses;                  
        }       

        public decimal DeltaFromShop { get; }

        private decimal GetDelta(decimal budget, TotalConstExpenses totalConst, Goods good, int numberOfMonth)//variable number of toys every month
        {
            numbersOfToys = (int)((budget - totalConst.GetTotalExpensesForEmployees() - totalConst.GetTotalExpensesForRentalSpace()) / good.PurchasePrice);
            if (numberOfMonth >= 1)
            {
                delta = numbersOfToys * good.SalePrice;

                var newGood = new Goods(good.PurchasePrice, good.SalePrice, numbersOfToys);
                goods.Add(newGood);
                storages.Goods.Update(newGood, 0);

                expenses.Add(new Analytics.Expenses(
                    totalConst.GetTotalExpensesForRentalSpace(),
                    totalConst.GetTotalExpensesForEmployees(),
                    newGood.PurchasePrice * newGood.Count));

                return GetDelta(delta, totalConst, good, numberOfMonth - 1);
            }
            else
            {
                return delta;  
            } 
        }
    }
}
