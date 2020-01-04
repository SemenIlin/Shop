using Shop.DAL.Storages;

namespace Shop.DAL.Expenses
{
    public class TotalVariableExpenses
    {
        private readonly Storage storages;

        public TotalVariableExpenses()
        {
            storages = Storage.GetStorages();
        }

        public decimal GetTotalExpensesForGoods()
        {
            decimal expensesForGood = 0;
            foreach (var good in storages.Goods)
            {
                expensesForGood += good.TotalExpensesForPurchase;
            }

            return expensesForGood;
        }
    }
}