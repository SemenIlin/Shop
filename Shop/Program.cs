using System;
using Shop.DAL.Infrastructure;
using EndingsOfTheNouns;

namespace Shop
{
    class Program
    {
        static void Main()
        {
            decimal delta;
            decimal revenue;
            int counter = 1;
            var tableExpenses = new ConsoleTable("№","Аренда", "Зарплата", "Закупка", "Итого");
            var tableRevenueFromSales = new ConsoleTable("№", "Цена продажи","Наценка", "Количество", "Итого с продаж");

            try
            {
                var input = new MyShopInputData();
                var myShop = new MyShopOutpurResult(input);

                delta = myShop.Delta.DeltaFromShop;
                revenue = myShop.Revenue.GetRevenue();

                if (input.NumberOfMonths == 1)
                {
                    tableExpenses.AddRow("1",myShop.TotalConst.GetTotalExpensesForRentalSpace().ToString(),
                                         myShop.TotalConst.GetTotalExpensesForEmployees().ToString(),
                                         myShop.TotalVariable.GetTotalExpensesForGoods().ToString(),
                                         myShop.TotalExpenses.GetExpenses().ToString()); 
                }
                else 
                { 
                    foreach (var expense in myShop.Expenses) 
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
                foreach (var good in (input.NumberOfMonths == 1 ? myShop.GetGoods() : myShop.Toys))
                {
                    tableRevenueFromSales.AddRow(counter.ToString(), good.SalePrice.ToString(), good.Margin.ToString(), good.Count.ToString(), good.TotalRevenueFromSales.ToString());
                    ++counter;
                }              

                Console.WriteLine("Затраты");
                tableExpenses.Print();
                Console.WriteLine();
                Console.WriteLine("Доход от продажи товара");
                tableRevenueFromSales.Print();

                Console.WriteLine($"Доход за {input.NumberOfMonths} {Endings.GetNewWord("месяц", input.NumberOfMonths)}: {revenue}.");
                Console.WriteLine("Прибыль за это время составила:{0}", delta);
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (MoneyException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}
