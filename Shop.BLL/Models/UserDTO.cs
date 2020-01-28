using Shop.DAL.Models;
using System.Collections.Generic;

namespace Shop.BLL.Models
{
    public class UserDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public IEnumerable<Employees> Employees { get; set; }
        public IEnumerable<Goods> Goods { get; set; }
        public IEnumerable<RentalSpaces> RentalSpaces { get; set; }

        public decimal TotalExpenses { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal Delta { get; set; }

        public decimal Budget { get; set; }
        public int Month { get; set; }
    }
}
