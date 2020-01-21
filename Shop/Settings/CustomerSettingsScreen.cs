using System;
using System.Collections.Generic;
using System.Linq;
using EndingsOfTheNouns;
using Shop.CursoreConsole;
using Shop.PL;

namespace Shop.Settings
{
    public class CustomerSettingsScreen
    {
        private readonly CalculaterOfRevenue calculater; 

        private readonly Dictionary<string, Action> customerSettings;

        public CustomerSettingsScreen(CalculaterOfRevenue calculater)
        {
            this.calculater = calculater;

            OperationOverEmployees = new OperationOverEmployees(calculater);
            OperationOverGoods = new OperationOverGoods(calculater);
            OperationOverRentalSpace = new OperationOverRentalSpace(calculater);

            customerSettings = new Dictionary<string, Action>()
            {
                {"Работник.", GetOperationOverEmployees },
                { "Товар.", GetOperationOverGoods },
                { "Арендуемое помещение.", GetOperationOverRentalSpace },
                { "Расчитать прибыль", CalculateRevenue},
                { "Назад", ToMainScreen }
            };

            CursorForSelect = new CursorForSelect(customerSettings);
        }

        public OperationOverEmployees OperationOverEmployees { get; private set; }
        public OperationOverGoods OperationOverGoods { get; private set; }
        public OperationOverRentalSpace OperationOverRentalSpace { get; private set; }  
        
        public CursorForSelect CursorForSelect { get; private set; }
        
        public bool IsSettings { get; set; }

        private void ToMainScreen()
        {
            IsSettings = false;
        }

        private void GetOperationOverEmployees()
        {
            OperationOverEmployees.IsToBack = false;
        }

        private void GetOperationOverGoods()
        {
            OperationOverGoods.IsToBack = false;
        }

        private void GetOperationOverRentalSpace() 
        {
            OperationOverRentalSpace.IsToBack = false;        
        }

        private void CalculateRevenue()
        {
            Console.WriteLine("Введите бюджет.");
            decimal.TryParse(Console.ReadLine(), out decimal budget);

            if (budget < calculater.TotalExpenses.GetExpenses())
            {
                Console.WriteLine("Недостаточно денег.");
                return;            
            }

            Console.WriteLine("Введите количество месяцев");
            int month = int.TryParse(Console.ReadLine(), out month) ? (month > 0 ? month : 1) : 1;

            calculater.CalculateRevenue(budget, month);

            OutputDataInTables(month);
            Console.ReadLine();
        }

        private void OutputDataInTables(int month)
        {
            var tableExpenses = new ConsoleTable("№", "Аренда", "Зарплата", "Закупка", "Итого");
            var tableRevenueFromSales = new ConsoleTable("№", "Цена продажи", "Наценка", "Количество", "Итого с продаж");

            int counter = 1;

            if (month == 1)
            {
                tableExpenses.AddRow("1", calculater.TotalConst.GetTotalExpensesForRentalSpace().ToString(),
                                     calculater.TotalConst.GetTotalExpensesForEmployees().ToString(),
                                     calculater.TotalVariable.GetTotalExpensesForGoods().ToString(),
                                     calculater.TotalExpenses.GetExpenses().ToString());
            }
            else
            {
                foreach (var expense in calculater.Expenses)
                {
                    tableExpenses.AddRow(counter.ToString(),
                                         expense.TotalRentalSpace.ToString(),
                                         expense.TotalSalary.ToString(),
                                         expense.TotalPurchasePriceOfGood.ToString(),
                                         expense.TotalExpenses.ToString());
                    ++counter;
                }
            }
            

            counter = 1;
            foreach (var good in (month == 1 ? calculater.GetGoods() : calculater.Toys))
            {
                tableRevenueFromSales.AddRow(counter.ToString(), good.SalePrice.ToString(), good.Margin.ToString(), good.Count.ToString(), good.TotalRevenueFromSales.ToString());
                ++counter;
            }
            calculater.GetGoods().Clear();         

            Console.WriteLine("Затраты");
            tableExpenses.Print();
            Console.WriteLine();
            Console.WriteLine("Доход от продажи товара");

            tableRevenueFromSales.Print();
            Console.WriteLine($"Доход {calculater.Toys.Sum(t => t.TotalRevenueFromSales)} Расход {calculater.Expenses.Sum(e => e.TotalExpenses)}");
            Console.WriteLine($"Прибыль за {month} {Endings.GetNewWord("месяц", month)} составила:{calculater.Delta.DeltaFromShop}");

            calculater.Clear();
            calculater.Toys.Clear();
            calculater.Expenses.Clear();
            Console.ReadLine();
        }
    }
}
