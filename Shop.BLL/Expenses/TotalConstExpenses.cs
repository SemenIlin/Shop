using Shop.DAL.Storages;

namespace Shop.BLL.Expenses
{
    public class TotalConstExpenses
    {
        private readonly Storage storage;

        public TotalConstExpenses()
        {
            storage = Storage.GetStorages();


            RentalSpaseExpenses = GetTotalExpensesForRentalSpace();
            EmployeesExpenses = GetTotalExpensesForEmployees();
            ConstExpenses = RentalSpaseExpenses + EmployeesExpenses;
        }

        public decimal ConstExpenses { get; }
        public decimal RentalSpaseExpenses { get; }
        public decimal EmployeesExpenses { get; }

        private decimal GetTotalExpensesForEmployees()
        {
            decimal expensesForEmployee = 0;
            foreach (var employee in storage.Employees)
            {
                expensesForEmployee += employee.Salary;
            }

            return expensesForEmployee;
        }

        private decimal GetTotalExpensesForRentalSpace()
        {
            decimal expensesForRentalSpace = 0;
            foreach (var space in storage.RentalSpaces)
            {
                expensesForRentalSpace += space.Rental;
            }

            return expensesForRentalSpace;
        }
    }
}
