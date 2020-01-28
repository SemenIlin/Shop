using Shop.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using Shop.DAL.Analytics;
using Shop.DAL.Interfaces;

namespace Shop.BLL.Infrastructure
{
    public class Delta
    {
        private readonly List<decimal> result = new List<decimal>();        

        private readonly decimal totalConstExpenses;
        
        private decimal balance;        
        private decimal surplus;

        private decimal margin;
        private decimal totalRevenueFromSales;
        private decimal totalExpenceForPurchase;
        private int numbersOfToys;

        public Delta(AnalyticsOfShop analytics)//const number of toys every month
        {
            DeltaFromShop = analytics.Revenue - analytics.TotalExpenses;                 
        }

        public Delta(AnalyticsOfShop analytics, int month)
        {
            DeltaFromShop = (analytics.Revenue - analytics.TotalExpenses) * month;
        }

        public Delta(IUnitOfWork storages,  AnalyticsOfShop analytics, decimal budget, int numberOfMonth)
        {
            Goods good = storages.Goods.Get(0);

            totalConstExpenses = analytics.TotalRentalSpace + analytics.TotalSalary;

            DeltaFromShop = GetDelta(budget, good, analytics, numberOfMonth) - storages.Goods.GetAll().Sum(g => g.TotalExpensesForPurchase) - totalConstExpenses * numberOfMonth;                  
        }       

        public decimal DeltaFromShop { get; }

        private decimal GetDelta(decimal budget, Goods good, AnalyticsOfShop analytics, int numberOfMonth)//variable number of toys every month
        {
           if (numberOfMonth >= 1)
            {
                balance = budget - analytics.TotalSalary - analytics.TotalRentalSpace;

                if (good.SalePrice * numbersOfToys > decimal.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(balance), "Поздравляю. У Вас невероятно БОЛЬШАЯ прибыль!");
                }
                else if (balance / good.PurchasePrice > int.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(numbersOfToys), "Поздравляю. У Вас невероятно огромное число товара!");
                }

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
                
                return GetDelta(totalRevenueFromSales + surplus, newGood, analytics, numberOfMonth - 1);
            }
            else
            {
                return result.Sum();  
            } 
        }
    }
}
