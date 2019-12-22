namespace LibraryShop.Goods
{
    public interface IGoods
    { 
        decimal PurchasePrice { get; set; }
        decimal SalePrice { get; set; }
        int Count { get; set; }
    }
}
