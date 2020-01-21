using Shop.CursoreConsole;
using Shop.PL;
using System;
using Exceptions;
using System.Collections.Generic;

namespace Shop.Settings
{
    public class ChoiseOperationForCalculateRevevenue
    {
        private readonly SignInOut signInOut;
        private readonly CalculaterOfRevenue calculater;
        public ChoiseOperationForCalculateRevevenue(CalculaterOfRevenue calculater, SignInOut signInOut)
        {
            this.calculater = calculater; 
            this.signInOut = signInOut;

            CalculateRevenueDefault = new CalculateRevenueDefault(calculater);
            CustomerSettingsScreen = new CustomerSettingsScreen(calculater);
            
            CursorForSelect = new CursorForSelect(new Dictionary<string, Action>()
            {
                {"Посчитать с параметрами по умолчанию.", CalculateRevenueDefault.Calculate},
                {"Настроить.", ToSettings},
                {"Посмотреть прошлую зпись.", ToPrevRecords },
                {"Выйти", ToExit }
            });
        }

        public CursorForSelect CursorForSelect { get; private set; }

        public CalculateRevenueDefault CalculateRevenueDefault { get; private set; }
        public CustomerSettingsScreen CustomerSettingsScreen { get; private set; }
       

        private void ToPrevRecords()
        
        {
            if((signInOut == null) || 
               (calculater.Account == null) ||
               (calculater.Account.GetRecords() == null))
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
    }
}
