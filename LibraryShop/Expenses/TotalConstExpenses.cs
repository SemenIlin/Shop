using LibraryShop.RentalSpace;
using LibraryShop.Staff;

namespace LibraryShop.Expenses
{
    public class TotalConstExpenses
    {
        private readonly IEmployee[] employees;
        private readonly IRentalSpace[] rentalSpaces;

        public TotalConstExpenses(IEmployee[] employees, params IRentalSpace[] rentalSpaces)
        {
            this.employees = employees;
            this.rentalSpaces = rentalSpaces;


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
            foreach (var employee in employees)
            {
                expensesForEmployee += employee.Salary;
            }

            return expensesForEmployee;
        }

        private decimal GetTotalExpensesForRentalSpace()
        {
            decimal expensesForRentalSpace = 0;
            foreach (var space in rentalSpaces)
            {
                expensesForRentalSpace += space.Rental;
            }
            return expensesForRentalSpace;
        }
    }
}
