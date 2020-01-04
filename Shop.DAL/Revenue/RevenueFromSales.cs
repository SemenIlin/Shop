using Shop.DAL.Storages;

namespace Shop.DAL.Revenue
{
    public class RevenueFromSales
    {
        private readonly Storage storages;

        public RevenueFromSales()
        {
            storages = Storage.GetStorages();                       
        }

        public decimal GetRevenue()
        {
            decimal revenue = 0;
            foreach (var good in storages.Goods)
            {
                revenue += good.SalePrice * good.Count;
            }

            return revenue;
        } 
    }
}