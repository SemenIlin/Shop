namespace LibraryShop.Staff
{
    public class Porter : IEmployee
    {
        public Porter(string position, decimal salary)
        {
            Position = position;
            Salary = salary;
        }

        public string Position { get;  }
        public decimal Salary { get;  }
    }
}
