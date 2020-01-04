using Shop.DAL;
using Shop.DAL.Expenses;
using Shop.DAL.Models;
using Shop.DAL.Revenue;
using Shop.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;
using Shop.DAL.Analytics;

namespace Shop
{
    class MyShopOutpurResult
    {
        private readonly ListUnitOfWorks listUnitOfWorks = new ListUnitOfWorks();

        private readonly MyShopInputData myShopInput;


        public MyShopOutpurResult(IInputData inputData)
        {
            Toys = new List<Goods>();
            Expenses = new List<Expenses>();
            myShopInput = (MyShopInputData)inputData;            
            listUnitOfWorks.Employees.Create(new Employees("Seller", myShopInput.SalaryForSeller));
            listUnitOfWorks.Employees.Create (new Employees("Porter", myShopInput.SalaryForPorter));
            listUnitOfWorks.Employees.Create(new Employees("Accountant", myShopInput.SalaryForAccountant));
            listUnitOfWorks.Employees.Create (new Employees("PurchasingAgent ", myShopInput.SalaryForPurchasingAgent));
            Create();

        }

        public Delta Delta { get; private set; }
        public TotalExpenses TotalExpenses { get; private set; }
        public TotalConstExpenses TotalConst { get; private set; }
        public TotalVariableExpenses TotalVariable { get; private set; }
        public RevenueFromSales Revenue { get; private set; }
        public List<Goods> Toys { get; private set; } 
        public List<Expenses> Expenses { get; private set; }

        public List<Goods> GetGoods()
        {
            return listUnitOfWorks.Goods.GetAll().ToList();
        }

        public List<RentalSpaces> GetRentalSpace()
        {
            return listUnitOfWorks.RentalSpaces.GetAll().ToList();
        }

        private void Create()
        {
            CreateRentalSpace();
            CreateGoods();
            CreateTotalConstExpenses();
            CreateTotalVariableExpenses();
            CreateTotalExpenses();
            CreateRevenueFromSales();
            if (myShopInput.NumberOfMonths == 1)
            {
                CreateDelta();
            }
            else
            {
                CreateDelta(myShopInput.NumberOfMonths);
            }
        }

        private void CreateTotalConstExpenses()
        {
            TotalConst = new TotalConstExpenses();
        }

        private void CreateTotalVariableExpenses()
        {
            TotalVariable = new TotalVariableExpenses();
        }

        private void CreateTotalExpenses()
        {
            TotalExpenses = new TotalExpenses(TotalConst, TotalVariable);
        }         

        private void CreateRevenueFromSales()
        {
           Revenue = new RevenueFromSales();
        }

        private void CreateDelta()
        {
            Delta = new Delta(TotalExpenses, Revenue);
        }

        private void CreateDelta(int month)
        {
            Delta = new Delta(myShopInput.Budget, TotalConst, GetGoods()[0], month, Toys, Expenses);
        }

        private void CreateGoods()
        {
            listUnitOfWorks.Goods.Create(new Goods(myShopInput.PricePurchaseToy, myShopInput.PriceSalesToy, myShopInput.NumberOfToys));
        }

        private void CreateRentalSpace()
        {
            listUnitOfWorks.RentalSpaces.Create(new RentalSpaces("Склад", myShopInput.ValueRentalSpace));
        } 
    }
}