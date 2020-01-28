using System;
using System.Collections.Generic;
using Shop.CursoreConsole;
using Shop.BLL.Infrastructure;
using Shop.Settings;

namespace Shop.PL.Settings
{
    public class CustomerSettingsScreen
    {
        private readonly UserServiceFromList calculater; 

        private readonly Dictionary<string, Action> customerSettings;

        private DrawTable draw;
        private ToFillUser toFill;

        public CustomerSettingsScreen(UserServiceFromList calculater)
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

        public delegate void DrawTable(int month);
        public delegate void ToFillUser(int month, decimal budget);

        public void RegisterHandlerDrawTable(DrawTable draw)
        {
            this.draw = draw;
        }

        public void RegisterHandlerToFillUser(ToFillUser toFill)
        {
            this.toFill = toFill;
        }

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

            if (budget < calculater.AnalyticsOfShop.TotalExpenses)
            {
                Console.WriteLine("Недостаточно денег.");
                return;            
            }

            Console.WriteLine("Введите количество месяцев");
            int month = int.TryParse(Console.ReadLine(), out month) ? (month > 0 ? month : 1) : 1;

            calculater.CalculateRevenue(month);
            toFill?.Invoke(month, budget);
            draw?.Invoke(month);
            Console.ReadLine();
        }
    }
}
