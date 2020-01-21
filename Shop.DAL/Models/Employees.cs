namespace Shop.DAL.Models
{
    public class Employees
    {
        private static int id = 1;
        public Employees()
        {
            Id = id;
            ++id;
        }

        public int Id { get; set; }
        public string Position { get;  set; }
        public decimal Salary { get;  set; }
    }
}
