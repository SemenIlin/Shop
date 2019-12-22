﻿namespace LibraryShop.Expenses
{
    public class TotalExpenses
    {
        public TotalExpenses(TotalConstExpenses totalConstExpenses, TotalVariableExpenses totalVariableExpenses)
        {
            Expenses = totalConstExpenses.ConstExpenses + totalVariableExpenses.VariableExpenses;        
        }

        public decimal Expenses { get; }
    }
}