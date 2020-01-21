using EndingsOfTheNouns;
using Shop.PL;
using System;
using System.Globalization;
using System.Linq;

namespace Shop.Settings
{
    public class CalculateRevenueDefault
    {
        private readonly NumberStyles style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign;
        private readonly CultureInfo culture = CultureInfo.CreateSpecificCulture("en-GB");
        private readonly CalculaterOfRevenue calculater;

        private decimal budget;
        private decimal bubgetDublicat;

        private decimal salaryForAccountant;
        private decimal salaryForSeller;
        private decimal salaryForPorter;
        private decimal salaryForPurchasingAgent;

        private decimal pricePurchaseToy;
        private decimal priceSalesToy;
        private int numberOfToys;

        private int month;

        private decimal valueRentalSpace;

        public CalculateRevenueDefault(CalculaterOfRevenue calculater)
        {
            this.calculater = calculater;
        }

        public void Calculate()
        {
            SetBudget();
            SetSalaryForImployeesDefault();
            SetRentalSpace();
            SetToyPricePurchace();
            CalculateCountToys();
            SetToyPriceSale();
            SetNumberOfMonths();
            calculater.CalculateRevenue(bubgetDublicat, month);
            OutputDataInTables(month);

            calculater.Clear();
            Console.ReadLine();
        }

        private void SetSalaryForImployeesDefault()
        {
            Console.WriteLine("Введите зарплату бухгалтеру.");
            decimal.TryParse(Console.ReadLine(), style, culture, out salaryForAccountant);
            GetBudget(salaryForAccountant);
            calculater.AddEmployee("Accountant", salaryForAccountant);

            Console.WriteLine("Введите зарплату продавцу.");
            decimal.TryParse(Console.ReadLine(), style, culture, out salaryForSeller);
            GetBudget(salaryForSeller);
            calculater.AddEmployee("Seller", salaryForSeller);

            Console.WriteLine("Введите зарплату грузчику.");
            decimal.TryParse(Console.ReadLine(), style, culture, out salaryForPorter);
            GetBudget(salaryForPorter);
            calculater.AddEmployee("Porter", salaryForPorter);

            Console.WriteLine("Введите зарплату закупщику.");
            decimal.TryParse(Console.ReadLine(), style, culture, out salaryForPurchasingAgent);
            GetBudget(salaryForPurchasingAgent);
            calculater.AddEmployee("PurchasingAgent", salaryForPurchasingAgent);
        }

        private void SetBudget()
        {
            Console.WriteLine("Введите свой бюджет.");
            decimal.TryParse(Console.ReadLine(), style, culture, out budget);
            bubgetDublicat = budget;
        }

        private void SetRentalSpace()
        {
            Console.WriteLine("Введите арендную плату.");
            decimal.TryParse(Console.ReadLine(), style, culture, out valueRentalSpace);
            GetBudget(valueRentalSpace);
            calculater.CreateRentalSpace("Shop", valueRentalSpace);
        }

        private void GetBudget(decimal moneyValue)
        {
            budget -= moneyValue;
            if (budget <= 0)
            {
                 throw new Exception("Закончились деньги");
            }
        }

        private void SetToyPricePurchace()
        {
            Console.WriteLine("Введите закупочную цену игрушки.");
            decimal.TryParse(Console.ReadLine(), style, culture, out pricePurchaseToy);
        }

        private void CalculateCountToys()
        {
            if (budget >= pricePurchaseToy)
            {
                int numberOfToys = (int)Math.Floor(budget / pricePurchaseToy);
                Console.WriteLine("На оставшиеся деньги можно купить {0} игрушек.", numberOfToys);

                SetNumberOfToys(numberOfToys);
            }
        }

        private void SetNumberOfToys(int maxValueOfToys)
        {
            Console.WriteLine("Введите количество игрушек.");
            int.TryParse(Console.ReadLine(), out numberOfToys);
            if (maxValueOfToys < numberOfToys)
            {
                throw new Exception("Невозможно закупить столько товара");
            }
        }

        private void SetToyPriceSale()
        {
            Console.WriteLine("Введите цену при продажи игрушки. Минимальная цена для получения прибыли {0}", pricePurchaseToy + 1 + Math.Round((bubgetDublicat - budget)/ numberOfToys, 2));
            decimal.TryParse(Console.ReadLine(), style, culture, out priceSalesToy);
            calculater.CreateGood(pricePurchaseToy, priceSalesToy, numberOfToys);
        }

        private void SetNumberOfMonths()
        {
            Console.WriteLine("Введите количество месяцев для расчёта.");
            int.TryParse(Console.ReadLine(), out month);
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
