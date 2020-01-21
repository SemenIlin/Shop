namespace Shop.DAL.Models
{
    public class Goods
    {
        private static int id = 1;
        public Goods()
        {
            Id = id;
            ++id;
        }

        public int Id { get; private set; }

        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal Margin { get;  set; }
        public decimal TotalRevenueFromSales { get; set; }
        public decimal TotalExpensesForPurchase { get; set; }
        public int Count { get; set; }
    }
}
