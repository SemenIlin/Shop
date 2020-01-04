namespace Shop.DAL.Expenses
{
    public class TotalExpenses
    {
        private readonly TotalConstExpenses totalConstExpenses;
        private readonly TotalVariableExpenses totalVariableExpenses;

        public TotalExpenses(TotalConstExpenses totalConstExpenses, TotalVariableExpenses totalVariableExpenses)
        {
            this.totalConstExpenses = totalConstExpenses;
            this.totalVariableExpenses = totalVariableExpenses;      
        }

        public decimal GetExpenses()
        {
            return totalConstExpenses.GetTotalExpensesForEmployees() +
                   totalConstExpenses.GetTotalExpensesForRentalSpace() +
                   totalVariableExpenses.GetTotalExpensesForGoods();
        } 
    }
}
