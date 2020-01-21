using Shop.DAL.Expenses;
using Shop.DAL.Models;
using Shop.DAL.Repositories;
using Shop.DAL.Revenue;
using System.Collections.Generic;
using System.Linq;

namespace Shop.DAL
{
    public class Delta
    {
        private readonly ListUnitOfWorks storages;
        private readonly List<Goods> goods;
        private readonly List<Analytics.Expenses> expenses;
        private readonly List<decimal> result = new List<decimal>();

        private readonly decimal totalConstExpenses;
        
        private decimal balance;        
        private decimal surplus;

        private decimal margin;
        private decimal totalRevenueFromSales;
        private decimal totalExpenceForPurchase;
        private int numbersOfToys;

        public Delta(TotalExpenses total, RevenueFromSales revenueFromSales)//const number of toys every month
        { 
            DeltaFromShop = revenueFromSales.GetRevenue() - total.GetExpenses();                    
        }

        public Delta(decimal budget, TotalConstExpenses totalConst, Goods good, int numberOfMonth, List<Goods> goods, List<Analytics.Expenses> expenses)
        {
            this.goods = goods;
            this.expenses = expenses;
            storages = new ListUnitOfWorks();
            
            totalConstExpenses = totalConst.GetTotalExpensesForEmployees() + totalConst.GetTotalExpensesForRentalSpace();

            DeltaFromShop =GetDelta(budget, totalConst, good, numberOfMonth) - goods.Sum(g=>g.TotalExpensesForPurchase) - totalConstExpenses * numberOfMonth;                  
        }       

        public decimal DeltaFromShop { get; }

        private decimal GetDelta(decimal budget, TotalConstExpenses totalConst, Goods good, int numberOfMonth)//variable number of toys every month
        {
           if (numberOfMonth >= 1)
            {
                balance = budget - totalConst.GetTotalExpensesForEmployees() - totalConst.GetTotalExpensesForRentalSpace();
                numbersOfToys = (int)(balance / good.PurchasePrice);
                surplus = balance - numbersOfToys * good.PurchasePrice;
               
                margin = good.SalePrice - good.PurchasePrice;
                totalRevenueFromSales = good.SalePrice * numbersOfToys;
                totalExpenceForPurchase = good.PurchasePrice * numbersOfToys;
                result.Add(totalRevenueFromSales);

                var newGood = new Goods {PurchasePrice = good.PurchasePrice,
                                         SalePrice = good.SalePrice, 
                                         Count = numbersOfToys,
                                         Margin = margin,
                                         TotalExpensesForPurchase = totalExpenceForPurchase,
                                         TotalRevenueFromSales = totalRevenueFromSales};                

                goods.Add(newGood);
                storages.Goods.Update(newGood, 0);

                expenses.Add(new Analytics.Expenses(
                    totalConst.GetTotalExpensesForRentalSpace(),
                    totalConst.GetTotalExpensesForEmployees(),
                    newGood.PurchasePrice * newGood.Count));

                return GetDelta(totalRevenueFromSales + surplus, totalConst, good, numberOfMonth - 1);
            }
            else
            {
                return result.Sum();  
            } 
        }
    }
}
