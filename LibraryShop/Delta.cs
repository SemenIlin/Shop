using LibraryShop.Expenses;
using LibraryShop.Revenue;

namespace LibraryShop
{
    public class Delta
    {
        public Delta(TotalExpenses totalExpenses, RevenueFromSales revenueFromSales, int numberOfMonths = 1)
        {
            DeltaFromShop = (revenueFromSales.Revenue - totalExpenses.Expenses) * numberOfMonths;                    
        }

        public decimal DeltaFromShop { get; }
    }
}
