namespace LibraryShop.Goods
{
    public class Toys : IGoods
    {
        public Toys(decimal purchasePrice, decimal salePrice, int count)
        {
            PurchasePrice = purchasePrice;
            SalePrice = salePrice;
            Count = count;            
        }

        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }

        public int Count { get; set; }
    }
}
