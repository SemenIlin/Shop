using Shop.CursoreConsole;
using Shop.PL;
using System;
using Exceptions;
using System.Collections.Generic;
using Shop.BLL.Infrastructure;
using Shop.PL.Settings;
using EndingsOfTheNouns;

namespace Shop.Settings
{
    public class ChoiseOperationForCalculateRevevenue
    {
        private readonly SignInOut signInOut;
        private readonly UserServiceFromList calculater;

        public ChoiseOperationForCalculateRevevenue(UserServiceFromList calculater, SignInOut signInOut)
        {
            this.signInOut = signInOut;
            this.calculater = calculater;

            CalculateRevenueDefault = new CalculateRevenueDefault(calculater);
            CustomerSettingsScreen = new CustomerSettingsScreen(calculater);

            CalculateRevenueDefault.RegisterHandlerDrawTable(OutputDataInTables);
            CalculateRevenueDefault.RegisterHandlerToFillUser(signInOut.ToFillUser);

            CustomerSettingsScreen.RegisterHandlerDrawTable(OutputDataInTables);
            CustomerSettingsScreen.RegisterHandlerToFillUser(signInOut.ToFillUser);

            CursorForSelect = new CursorForSelect(new Dictionary<string, Action>()
            {
                {"Посчитать с параметрами по умолчанию.", CalculateRevenueDefault.Calculate},
                {"Настроить.", ToSettings},
                {"Посмотреть прошлую зaпись.", ToPrevRecords },
                {"Выйти", ToExit }
            });
        }

        public CursorForSelect CursorForSelect { get; private set; }

        public CalculateRevenueDefault CalculateRevenueDefault { get; private set; }
        public CustomerSettingsScreen CustomerSettingsScreen { get; private set; }
       

        private void ToPrevRecords()
        
        {
            if(signInOut == null)
            {
                throw new ValidationException("У Вас нету записей.", "");
            }
            else
            {
                signInOut.ViewPreviousEntry();
                Console.ReadLine();
            }
        }

        private void ToSettings()
        {
            CustomerSettingsScreen.IsSettings = true;
        }

        private void ToExit()
        {
            signInOut.ExitFromAccount();
        }

        private void OutputDataInTables(int month)
        {
            var tableExpenses = new ConsoleTable($"Количество месяцев", "Аренда", "Зарплата", "Закупка", "Итого");
            var tableRevenueFromSales = new ConsoleTable("№", "Цена продажи", "Наценка", "Количество", "Итого с продаж");

            tableExpenses.AddRow(month.ToString(), (calculater.AnalyticsOfShop.TotalRentalSpace * month).ToString(),
                                 (calculater.AnalyticsOfShop.TotalSalary * month).ToString(),
                                 (calculater.AnalyticsOfShop.TotalPurchasePriceOfGood* month).ToString(),
                                 (calculater.AnalyticsOfShop.TotalExpenses* month).ToString());


            foreach (var good in calculater.GetGoods())
            {
                tableRevenueFromSales.AddRow(1.ToString(), 
                    (good.SalePrice * month).ToString(),
                    (good.Margin * month).ToString(),
                    (good.Count * month).ToString(),
                    (good.TotalRevenueFromSales * month).ToString());
            }

            Console.WriteLine("Затраты");
            tableExpenses.Print();
            Console.WriteLine();
            Console.WriteLine("Доход от продажи товара");
            tableRevenueFromSales.Print();

            Console.WriteLine($"Доход {calculater.AnalyticsOfShop.Revenue * month} Расход {calculater.AnalyticsOfShop.TotalExpenses * month}");
            Console.WriteLine($"Прибыль за {month} {Endings.GetNewWord("месяц", month)} составила:{calculater.Delta.DeltaFromShop}");

            calculater.Clear();
            Console.ReadLine();
        }
    }
}
