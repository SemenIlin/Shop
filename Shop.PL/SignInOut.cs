using System;
using Shop.PL.Models;
using System.Collections.Generic;
using Shop.CursoreConsole;
using Shop.BLL.Models;
using Shop.BLL.Interfaces;
using Shop.BLL.Infrastructure;

namespace Shop.PL
{
    public class SignInOut
    {        
        private readonly Dictionary<string, Action> action;
        private readonly IAccount<UserDTO,RegistrationDTO,SignInDTO> account;
        private readonly UserServiceFromList userService;

        private string login;
        private string password;
        private string confirmPassword;

        public SignInOut(IAccount<UserDTO, RegistrationDTO, SignInDTO> account, UserServiceFromList userService)
        {
            this.account = account;
            this.userService = userService;

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
            account.SignInUser( new SignInDTO {Login = SignInModel.Login, Password = SignInModel.Password });           
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
            account.RegistrationUser(new RegistrationDTO {
                Login = RegistrationModel.Login, 
                Password = RegistrationModel.Password, 
                ConfirmPassword = RegistrationModel.ConfirmPassword
            });
            account.CreateRecord();
        }

        public void ExitFromAccount()
        {
            account.ExitFromAccount();
        }

        public void ToFillUser(int month, decimal budget)
        {
            account.User.Employees = userService.GetEmployees();
            account.User.Goods = userService.GetGoods();
            account.User.RentalSpaces = userService.GetRentalSpaces();
            account.User.TotalRevenue = userService.AnalyticsOfShop.Revenue * month;
            account.User.TotalExpenses = userService.AnalyticsOfShop.TotalExpenses * month; 
            account.User.Delta = userService.Delta.DeltaFromShop;
            account.User.Budget = budget;
            account.User.Month = month;

            account.CreateRecord();
        }

        public void ViewPreviousEntry()
        {
            var records = account.GetRecords();
            Console.WriteLine($"Начальный бюджет {records.Budget}.");
            if (records.Employees != null)
            {
                foreach (var employee in records.Employees)
                {
                    Console.WriteLine($"Должность: {employee.Position}, " +
                        $"зарплата за {records.Month} {EndingsOfTheNouns.Endings.GetNewWord("месяц", records.Month)}:" +
                        $" {employee.Salary * records.Month}.");
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
                    Console.WriteLine($"Название помещения: {rentalSpace.Title}, аренда" +
                        $" за {records.Month} {EndingsOfTheNouns.Endings.GetNewWord("месяц", records.Month)}: {rentalSpace.Rental * records.Month}.");
                }
            }
            Console.WriteLine($"Количество месяцев {records.Month}.");
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
