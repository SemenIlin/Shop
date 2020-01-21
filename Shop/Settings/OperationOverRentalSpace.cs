using System;
using System.Collections.Generic;
using System.Globalization;
using Shop.CursoreConsole;
using Shop.PL;

namespace Shop.Settings
{
    public class OperationOverRentalSpace
    {
        private readonly NumberStyles style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign;
        private readonly CultureInfo culture = CultureInfo.CreateSpecificCulture("en-GB");

        private readonly CalculaterOfRevenue calculater;
        private readonly CursorForSelect cursor;
        private CursorForSelect insideCursor;
        private Dictionary<string, Action> dictionaryRentalSpace;

        private string title = String.Empty;
        private decimal rentalSpace = 0;

        private bool IsRentalSpace { get; set; }

        public OperationOverRentalSpace(CalculaterOfRevenue calculater)
        {
            this.calculater = calculater;
            cursor = new CursorForSelect(new Dictionary<string, Action>()
            {
                { "Добавить помещение.", CreateRentalSpace },
                { "Редактировать помещение.", UpdateRentalSpace },
                { "Удалить помещение.", DeleteRentalSpace },
                { "Назад.", ToBack }
            });

            IsToBack = true;
        }

        public CursorForSelect CursorForSelect { get { return cursor; } }

        public bool IsToBack { get; set; }

        private void CreateRentalSpace()
        {
            InputDataForRentalSpace();

            calculater.CreateRentalSpace(title, rentalSpace);
        }

        private void UpdateRentalSpace()
        {
            IsRentalSpace = true;
            insideCursor = new CursorForSelect(CreateDictionaryRentalSpace());
            while (IsRentalSpace)
            {
                insideCursor.RenderCursor();
                insideCursor.Move(CursorForSelect.InputData());
            }

            if (insideCursor.Cursor.Position == dictionaryRentalSpace.Count - 1)
            {
                return;
            }

            InputDataForRentalSpace();

            calculater.UpdateRentalSpace(title, rentalSpace, insideCursor.Cursor.Position);

            Console.WriteLine("Данные изменины.");
            Console.ReadLine();
        }

        private void DeleteRentalSpace()
        {
            IsRentalSpace = true;
            insideCursor = new CursorForSelect(CreateDictionaryRentalSpace());
            if (dictionaryRentalSpace.Count < 1)
            {
                Console.WriteLine("У Вас нету арендуемых помещений");
                Console.ReadLine();
                return;
            }

            while (IsRentalSpace)
            {
                insideCursor.RenderCursor();
                insideCursor.Move(CursorForSelect.InputData());
            }

            if (insideCursor.Cursor.Position == dictionaryRentalSpace.Count - 1)
            {
                return;
            }

            calculater.DeleteRentalSpace(insideCursor.Cursor.Position);
            
            Console.WriteLine("Данные удалены.");
            Console.ReadLine();
        }

        private void ToBack()
        {
            IsToBack = true;
        }

        private Dictionary<string, Action> CreateDictionaryRentalSpace()
        {
            dictionaryRentalSpace = new Dictionary<string, Action>();
            foreach (var item in calculater.GetRentalSpace())
            {
                dictionaryRentalSpace.Add($"{item.Title}: {item.Rental}", ToBackRentalSpace);
            }
            dictionaryRentalSpace.Add("Назад.", ToBackRentalSpace);

            return dictionaryRentalSpace;
        }

        public void ToBackRentalSpace()
        {
            IsRentalSpace = false;
        }

        private void InputDataForRentalSpace()
        {
            Console.WriteLine("Введите название арендуемого помещения.");
            title = Console.ReadLine();
            Console.WriteLine("Введите арендную плату.");
            decimal.TryParse(Console.ReadLine(), style, culture, out rentalSpace);
        }
    }
}