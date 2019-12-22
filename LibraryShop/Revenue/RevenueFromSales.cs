using LibraryShop.Goods;

namespace LibraryShop.Revenue
{
    public class RevenueFromSales
    {
        public RevenueFromSales(params IGoods[] goods)
        {
            foreach (var good in goods)
            {
                Revenue += good.SalePrice * good.Count;
            }            
        }

        public decimal Revenue { get; }
    }
}