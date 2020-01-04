using Shop.DAL.Storages;

namespace Shop.DAL.Expenses
{
    public class TotalConstExpenses
    {
        private readonly Storage storages;

        public TotalConstExpenses()
        {
            storages = Storage.GetStorages();
        }

        public decimal GetTotalExpensesForEmployees()
        {
            decimal expensesForEmployee = 0;
            foreach (var employee in storages.Employees)
            {
                expensesForEmployee += employee.Salary;
            }

            return expensesForEmployee;
        }

        public decimal GetTotalExpensesForRentalSpace()
        {
            decimal expensesForRentalSpace = 0;
            foreach (var space in storages.RentalSpaces)
            {
                expensesForRentalSpace += space.Rental;
            }
            return expensesForRentalSpace;
        }
    }
}
