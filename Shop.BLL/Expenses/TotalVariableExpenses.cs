using Shop.DAL.Storages;

namespace Shop.BLL.Expenses
{
    public class TotalVariableExpenses
    {
        private readonly Storage storage;

        public TotalVariableExpenses( )
        {
            storage = Storage.GetStorages();

            VariableExpenses = GetTotalExpensesForGoods();
        }

        public decimal VariableExpenses { get; }

      

        private decimal GetTotalExpensesForGoods()
        {
            decimal expensesForGood = 0;
            foreach (var good in storage.Goods)
            {
                expensesForGood += good.PurchasePrice * good.Count;
            }

            return expensesForGood;
        }
    }
}
