using Shop.DAL.Infrastructure;
using System;
using System.Globalization;

namespace Shop
{
    public class MyShopInputData : IInputData
    {
        private readonly NumberStyles style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign;
        private readonly CultureInfo culture = CultureInfo.CreateSpecificCulture("en-GB");
        private decimal totalConstExpenses = 0;
        private decimal budget = 0;

        public MyShopInputData()
        {
            InitData();
        }

        public decimal Budget { get; private set; }

        public decimal SalaryForSeller { get; private set; }
        public decimal SalaryForPorter { get; private set; }
        public decimal SalaryForAccountant { get; private set; }
        public decimal SalaryForPurchasingAgent { get; private set; }

        public decimal PricePurchaseToy { get; private set; }
        public decimal PriceSalesToy { get; private set; }
        public int NumberOfToys { get; private set; }

        public decimal ValueRentalSpace { get; private set; } 

        public int NumberOfMonths { get; private set; }

        private void InitData()
        {
            SetBudget();
            SetRentalSpace();
            SetSalaryForImployees();
            GetBudget();
            SetToyPricePurchace();
            CalculateCountToys();
            SetToyriceSale();
            SetNumberOfMonths();
        }

        private void SetBudget()
        {
            Console.WriteLine("Введите свой бюджет.");
            decimal.TryParse(Console.ReadLine(), style, culture, out decimal budget);
            Budget = budget;
            this.budget = Budget;
        }

        private void SetRentalSpace()
        {
            Console.WriteLine("Введите арендную плату.");
            decimal.TryParse(Console.ReadLine(), style, culture, out decimal rentalSpace);
            ValueRentalSpace = rentalSpace;
        }

        private void SetSalaryForImployees()
        {
            Console.WriteLine("Введите зарплату бухгалтеру.");
            decimal.TryParse(Console.ReadLine(), style, culture, out decimal salaryForAccountant);
            SalaryForAccountant = salaryForAccountant;

            Console.WriteLine("Введите зарплату продавцу.");
            decimal.TryParse(Console.ReadLine(), style, culture, out decimal salaryForSeller);
            SalaryForSeller = salaryForSeller;

            Console.WriteLine("Введите зарплату грузчику.");
            decimal.TryParse(Console.ReadLine(), style, culture, out decimal salaryForPorter);
            SalaryForPorter = salaryForPorter;

            Console.WriteLine("Введите зарплату закупщику.");
            decimal.TryParse(Console.ReadLine(), style, culture, out decimal salaryForPurchasingAgent);
            SalaryForPurchasingAgent = salaryForPurchasingAgent;
        }

        private void GetBudget()
        {
            totalConstExpenses = SalaryForPurchasingAgent + SalaryForPorter + SalaryForSeller + SalaryForAccountant + ValueRentalSpace;
            budget -= totalConstExpenses;
            if (budget <= 0)
            {
                throw new MoneyException("Закончились деньги", "");
            }
        }

        private void SetToyPricePurchace()
        {
            Console.WriteLine("Введите закупочную цену игрушки.");
            decimal.TryParse(Console.ReadLine(), style, culture, out decimal pricePurchaseToy);
            PricePurchaseToy = pricePurchaseToy;
        }

        private void CalculateCountToys()
        {
            if (budget >= PricePurchaseToy)
            {
                int numberOfToys = (int)Math.Floor(budget / PricePurchaseToy);
                Console.WriteLine("На оставшиеся деньги можно купить {0} игрушек.", numberOfToys);

                SetNumberOfToys(numberOfToys);
            }
        }

        private void SetNumberOfToys(int maxValueOfToys)
        {
            Console.WriteLine("Введите количество игрушек.");
            int.TryParse(Console.ReadLine(), out int numberOfToys);
            if (maxValueOfToys >= numberOfToys)
            {
                NumberOfToys = numberOfToys;
            }
            else 
            {
                throw new MoneyException("Невозможно закупить столько товара", "");            
            }
        }

        private void SetToyriceSale()
        {
            Console.WriteLine("Введите цену при продажи игрушки. Минимальная цена для получения прибыли {0}", PricePurchaseToy + 1 + Math.Round(totalConstExpenses / NumberOfToys,2));
            decimal.TryParse(Console.ReadLine(), style, culture, out decimal priceSalesToy);
            PriceSalesToy = priceSalesToy;
        }

        private void SetNumberOfMonths()
        {
            Console.WriteLine("Введите количество месяцев для расчёта.");
            int.TryParse(Console.ReadLine(), out int numberOfMonths);
            NumberOfMonths = numberOfMonths;
        }
    }
}
