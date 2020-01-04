namespace Shop.DAL.Analytics
{
    public class Expenses
    {
        public Expenses(decimal totalRentalSpace, decimal totalSalary, decimal totalPurchasePriceOfGood)
        {
            TotalRentalSpace = totalRentalSpace;
            TotalSalary = totalSalary;
            TotalPurchasePriceOfGood = totalPurchasePriceOfGood;
            TotalExpenses = TotalPurchasePriceOfGood + TotalRentalSpace + TotalSalary;
        }

        public decimal TotalRentalSpace { get; private set; }
        public decimal TotalSalary { get; private set; }
        public decimal TotalPurchasePriceOfGood { get; private set; }
        public decimal TotalExpenses { get; private set; }

    }
}

