using Shop.DAL.Storages;

namespace LibraryShop.Revenue
{
    public class RevenueFromSales
    {
        private readonly Storage storages;

        public RevenueFromSales()
        {
            storages = Storage.GetStorages();
            Revenue = GetRevenue();
        }

        public decimal Revenue { get; }

        private decimal GetRevenue()
        {
            decimal revenue = 0;
            foreach (var good in storages.Goods)
            {
                revenue += good.TotalRevenueFromSales;
            }

            return revenue;
        }
    }
}