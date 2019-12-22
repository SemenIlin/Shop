using System;
using LibraryShop.Goods;
using LibraryShop.Staff;
using System.Globalization;
using LibraryShop.RentalSpace;
using LibraryShop.Expenses;
using LibraryShop.Revenue;
using LibraryShop;

namespace Shop
{
    class MyShop
    {
        private readonly NumberStyles style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign;
        private readonly CultureInfo culture = CultureInfo.CreateSpecificCulture("en-GB");

        private IGoods goods;
        private IRentalSpace rentalSpace;
        private Delta delta;
        private TotalExpenses totalExpenses;
        private TotalConstExpenses totalConst;
        private TotalVariableExpenses totalVariable;
        private readonly IEmployee[] employees = new IEmployee[4];

        public decimal VerificationOfData()
        {
            decimal data = decimal.TryParse(Console.ReadLine().Replace(',', '.'), style, culture, out data) ? (data > 0 ? data : 0) : 0;
            return data;
        }

        public decimal Budget { get; set; }

        public decimal SalaryForSeller { get; set; }
        public decimal SalaryForPorter { get; set; }
        public decimal SalaryForAccountant { get; set; }
        public decimal SalaryForPurchasingAgent { get; set; }

        public decimal PricePurchaseToy { get; set; }
        public decimal PriceSalesToy { get; set; }
        public int NumberOfToys { get; set; }

        public decimal ValueRentalSpace { get; set; }

        public void AddStaff()
        {
            employees[0] = CreateSeller(SalaryForSeller);
            employees[1] = CreatePorter(SalaryForPorter);
            employees[2] = CreateAccountant(SalaryForAccountant);
            employees[3] = CreatePurchasingAgent(SalaryForPurchasingAgent);
        }

        public IEmployee[] GetEmployees()
        {
            return employees;
        }

        public void CreateDelta(int numberOfMonth)
        {
            delta = new Delta(GetTotalExpenses(), GetRevenueFromSales(), numberOfMonth);
        }

        public Delta GetDelta()
        {
            return delta;
        }

        public IGoods GetGoods()
        {
            return goods;
        }

        public void CreateGoods()
        {
            goods = new Toys(PricePurchaseToy, PriceSalesToy, NumberOfToys);
        }

        public void CreateRentalSpace()
        {
            rentalSpace = new LibraryShop.RentalSpace.Shop(ValueRentalSpace);
        }

        public IRentalSpace GetRentalSpace()
        {
            return rentalSpace;
        }

        public TotalExpenses GetTotalExpenses()
        {
            return totalExpenses;
        }

        public TotalConstExpenses GetTotalConstExpenses()
        {
            return totalConst;
        }

        public TotalVariableExpenses GetTotalVariableExpenses()
        {
            return totalVariable;        
        }

        public void CreateExpenses()
        {
            CreateTotalConstExpenses();
            CreateTotalVariableExpenses();
            CreateTotalExpenses();

        }

        private void CreateTotalConstExpenses()
        {
            totalConst = new TotalConstExpenses(GetEmployees(), GetRentalSpace());
        }

        private void CreateTotalVariableExpenses()
        {
            totalVariable = new TotalVariableExpenses(GetGoods());
        }

        private void CreateTotalExpenses()
        {
            totalExpenses = new TotalExpenses(GetTotalConstExpenses(), GetTotalVariableExpenses());
        }

        private RevenueFromSales GetRevenueFromSales()
        {            
            return new RevenueFromSales(GetGoods());        
        }

        private Seller CreateSeller(decimal salary)
        {
            return new Seller("Seller", salary);
        }

        private Porter CreatePorter(decimal salary)
        {
            return new Porter("Porter", salary);
        }

        private Accountant CreateAccountant(decimal salary)
        {
            return new Accountant("Accountant", salary);
        }

        private PurchasingAgent CreatePurchasingAgent(decimal salary)
        {
            return new PurchasingAgent("PurchasingAgent ", salary);
        }
    }
}