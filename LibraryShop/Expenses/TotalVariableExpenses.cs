using LibraryShop.Goods;

namespace LibraryShop.Expenses
{
    public class TotalVariableExpenses
    {
        private readonly IGoods[] goods;

        public TotalVariableExpenses( params IGoods[] goods)
        {
            this.goods = goods;

            VariableExpenses = GetTotalExpensesForGoods();
        }

        public decimal VariableExpenses { get; }

      

        private decimal GetTotalExpensesForGoods()
        {
            decimal expensesForGood = 0;
            foreach (var good in goods)
            {
                expensesForGood += good.PurchasePrice * good.Count;
            }

            return expensesForGood;
        }
    }
}
