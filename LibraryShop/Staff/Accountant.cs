namespace LibraryShop.Staff
{
    public class Accountant:IEmployee
    {
        public Accountant(string position, decimal salary)
        {
            Position = position;
            Salary = salary;
        }

        public string Position { get; }
        public decimal Salary { get;  }
    }
}
