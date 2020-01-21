using System;
using System.Collections.Generic;
using System.Globalization;
using Shop.CursoreConsole;
using Shop.PL;

namespace Shop.Settings
{
    public class OperationOverEmployees
    {
        private readonly NumberStyles style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign;
        private readonly CultureInfo culture = CultureInfo.CreateSpecificCulture("en-GB");

        private Dictionary<string, Action> dictionaryEmployees;

        private readonly CalculaterOfRevenue calculater;
        private readonly CursorForSelect cursor;
        private CursorForSelect insideCursor;

        private decimal salary = 0;
        private string position = String.Empty;       

        private bool IsEmployee { get; set; }

        public OperationOverEmployees(CalculaterOfRevenue calculater)
        {
            this.calculater = calculater;

            cursor = new CursorForSelect(new Dictionary<string, Action>()
            {
                {"Добавить работника.", CreateEmployee },
                { "Редактировать работника.", UpdateEmployee },
                { "Удалить работника.", DeleteEmployee },
                { "Назад", ToBack }
            });

            IsToBack = true;
        }

        public CursorForSelect CursorForSelect { get { return cursor; } }

        public bool IsToBack { get; set; }

        private void CreateEmployee()
        {
            InputDataForEmployee();

            calculater.AddEmployee(position, salary);

            Console.WriteLine("Работник добавлен.");
            Console.ReadLine();
        }

        private void UpdateEmployee()
        {
            IsEmployee = true;
            insideCursor = new CursorForSelect(CreateDictionaryEmployees());
            while (IsEmployee)
            {
                insideCursor.RenderCursor();
                insideCursor.Move(CursorForSelect.InputData());                
            }

            if(insideCursor.Cursor.Position == dictionaryEmployees.Count - 1)
            {
                return;
            }

            InputDataForEmployee();

            calculater.UpdateEmployee(position, salary, insideCursor.Cursor.Position);

            Console.WriteLine("Данные изменины.");
            Console.ReadLine();
        }

        private void DeleteEmployee()
        {
            if(CreateDictionaryEmployees().Count < 1)
            {
                Console.WriteLine("У Вас нету работников.");
                Console.ReadLine();
                return;
            }

            IsEmployee = true;
            insideCursor = new CursorForSelect(CreateDictionaryEmployees());

            while (IsEmployee)
            {
                insideCursor.RenderCursor();
                insideCursor.Move(CursorForSelect.InputData());               
            }

            if(insideCursor.Cursor.Position == dictionaryEmployees.Count - 1)
            {
                return;
            }

            calculater.DeleteEmployee(insideCursor.Cursor.Position);

            Console.WriteLine("Данные удалены.");
            Console.ReadLine();
        }

        private void ToBack()
        {
            IsToBack = true;
        }

        private Dictionary<string,Action> CreateDictionaryEmployees()
        {
            dictionaryEmployees = new Dictionary<string, Action>();
            foreach(var item in calculater.GetEmployees())
            {
                dictionaryEmployees.Add($"{item.Position}: {item.Salary}", ToBackEmployee);
            }
            dictionaryEmployees.Add("Назад.", ToBackEmployee);

            return dictionaryEmployees;
        }

        private void ToBackEmployee()
        {
            IsEmployee = false;
        }

        private void InputDataForEmployee()
        {
            Console.WriteLine("Введите должность.");
            position = Console.ReadLine();
            Console.WriteLine("Введите зарплату.");
            decimal.TryParse(Console.ReadLine(), style, culture, out salary);
        }
    }
}
