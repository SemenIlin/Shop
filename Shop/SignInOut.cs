using System;
using Shop.PL.Models;
using Shop.PL;
using System.Collections.Generic;
using Shop.CursoreConsole;

namespace Shop
{
    public class SignInOut
    {        
        private readonly Dictionary<string, Action> action;
        private readonly Account account;
        private string login;
        private string password;
        private string confirmPassword;

        public SignInOut(Account account)
        {
            this.account = account;

            action = new Dictionary<string, Action>(){
                { "Войти в систему", SignIn},
                { "Зарегистрироваться", RegistrationOfUser }
            };

            CursorForSelect = new CursorForSelect(action);             
        }       

        public CursorForSelect CursorForSelect { get; private set; }
        public SignInModel SignInModel { get; private set; }
        public RegistrationModel RegistrationModel { get; private set; }

        public void SignIn()
        {
            SetLogin();
            SetPassword();

            SignInModel = new SignInModel(login, password);
            account.SignInUser(SignInModel);           
        }

        public bool IsLogin()
        {
            return account.IsLogin;
        }

        public void RegistrationOfUser()
        {
            SetLogin();
            SetPassword();
            SetConfirmPassword();

            RegistrationModel = new RegistrationModel(login, password, confirmPassword);
            account.RegistrationUser(RegistrationModel);
            account.CreateRecord();
        }

        public void ExitFromAccount()
        {
            account.ExitFromAccount();
        }

        public void ViewPreviousEntry()
        {
            var records = account.GetRecords();

            Console.WriteLine($"Начальный бюджет: {records.Budget}.");

            if (records.Employees != null)
            {
                foreach (var employee in records.Employees)
                {
                    Console.WriteLine($"Должность: {employee.Position}, зарплата: {employee.Salary}.");
                }
            }
            if (records.Goods != null)
            {
                foreach (var good in records.Goods)
                {
                    Console.WriteLine($"Цена закупки: {good.PurchasePrice}, цена продажи: {good.SalePrice}," +
                                      $"наценка: {good.Margin}, количество: {good.Count}.");
                }
            }
            if (records.RentalSpaces != null)
            {
                foreach (var rentalSpace in records.RentalSpaces)
                {
                    Console.WriteLine($"Название помещения: {rentalSpace.Title}, аренда: {rentalSpace.Rental}.");
                }
            }

            Console.WriteLine($"Количество месяцев {records.Month}");
            Console.WriteLine($"Доход  {records.TotalRevenue} расход {records.TotalExpenses} прибыль {records.Delta}");
        }

        private void SetLogin()
        {
            Console.WriteLine("Введите логин:");
            login = Console.ReadLine();
        }

        private void SetPassword()
        {
            Console.WriteLine("Введите пароль:");
            password = Console.ReadLine();
        }

        private void SetConfirmPassword()
        {
            Console.WriteLine("Повторите пароль:");
            confirmPassword = Console.ReadLine();
        }
    }
}
