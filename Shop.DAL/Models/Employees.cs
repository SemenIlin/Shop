namespace Shop.DAL.Models
{
    public class Employees
    {
        private static int id = 1;
        public Employees(string position, decimal salary)
        {
            Id = id;
            Position = position;
            Salary = salary;
            ++id;
        }

        public int Id { get; private set; }
        public string Position { get; private set; }
        public decimal Salary { get; private set; }
    }
}
