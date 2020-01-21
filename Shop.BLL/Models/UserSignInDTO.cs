using Shop.DAL.Models;
using System.Collections.Generic;

namespace ShopBLL.Models
{
    public class UserSignInDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public List<Employees> Employees { get; set; }
        public List<Goods> Goods { get; set; }
        public List<RentalSpaces> RentalSpaces { get; set; }

        public decimal TotalExpenses { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal Delta { get; set; }

        public decimal Budget { get; set; }
        public int Month { get; set; }
    }
}
