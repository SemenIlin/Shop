namespace LibraryShop.Staff
{
    public class Seller:IEmployee
    {
        public Seller(string position, decimal salary)
        {
            Position = position;
            Salary = salary;
        }

        public string Position { get; }
        public decimal Salary { get;  }
    }
}
