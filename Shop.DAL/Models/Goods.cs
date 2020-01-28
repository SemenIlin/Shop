namespace Shop.DAL.Models
{
    public class Goods
    {

        public int Id { get; set; }

        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal Margin { get;  set; }
        public decimal TotalRevenueFromSales { get; set; }
        public decimal TotalExpensesForPurchase { get; set; }
        public int Count { get; set; }
    }
}
