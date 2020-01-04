namespace Shop.DAL.Models
{
    public class Goods
    {
        private static int id = 1;
        public Goods(decimal purchasePrice, decimal salePrice, int count)
        {
            PurchasePrice = purchasePrice;
            SalePrice = salePrice;
            Count = count;
            Margin = salePrice - purchasePrice;
            TotalRevenueFromSales = salePrice * count;
            TotalExpensesForPurchase = purchasePrice * count;

            Id = id;
            ++id;
        }

        public int Id { get; private set; }

        public decimal PurchasePrice { get; private set; }
        public decimal SalePrice { get; private set; }
        public decimal Margin { get; private set; }
        public decimal TotalRevenueFromSales { get; private set; }
        public decimal TotalExpensesForPurchase { get; private set; }
        public int Count { get; private set; }
    }
}
