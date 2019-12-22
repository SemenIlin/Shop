namespace LibraryShop.Staff
{
    public class PurchasingAgent:IEmployee
    {
        public PurchasingAgent(string position, decimal salary)
        {
            Position = position;
            Salary = salary;
        }

        public string Position { get; }
        public decimal Salary { get; }
    }
}
