using System;
using System.Collections.Generic;
using System.Globalization;
using Shop.CursoreConsole;
using Shop.PL;
namespace Shop.Settings
{
    public class OperationOverGoods
    {
        private readonly NumberStyles style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign;
        private readonly CultureInfo culture = CultureInfo.CreateSpecificCulture("en-GB");

        private readonly CalculaterOfRevenue calculater;
        private readonly CursorForSelect cursor; 
        private CursorForSelect insideCursor;
        private Dictionary<string, Action> dictionaryGoods;
        

        private decimal purchasePrice = 0;
        private decimal salePrice = 0;
        private int count = 0;

        private bool IsGoods { get; set; }

        public OperationOverGoods(CalculaterOfRevenue calculater)
        {
            this.calculater = calculater;
            cursor = new CursorForSelect(new System.Collections.Generic.Dictionary<string, Action>()
            {
                {"Добавить товар.", CreateGood },
                {"Редактировать товар.", UpdateGood },
                {"Удалить товар.", DeleteGood },
                {"Назад.", ToBack }
            });

            IsToBack = true;
        }

        public CursorForSelect CursorForSelect { get { return cursor; } }

        public bool IsToBack { get; set; }

        private void CreateGood()
        {
            InputDataForGood();

            calculater.CreateGood(purchasePrice, salePrice, count);
            Console.WriteLine("Товар добавлен.");
            Console.ReadLine();
        }

        private void UpdateGood()
        {
            IsGoods = true;
            insideCursor = new CursorForSelect(CreateDictionaryGoods());
            while(IsGoods)
            {
                insideCursor.RenderCursor();
                insideCursor.Move(CursorForSelect.InputData());
            }

            if(insideCursor.Cursor.Position == dictionaryGoods.Count - 1)
            {
                return;
            }

            InputDataForGood();

            calculater.UpdateGood(purchasePrice, salePrice, count, insideCursor.Cursor.Position);

            Console.WriteLine("Данные изменины.");
            Console.ReadLine();
        }

        private void DeleteGood()
        {
            IsGoods = true;
            insideCursor = new CursorForSelect(CreateDictionaryGoods());
            if(dictionaryGoods.Count < 1)
            {
                Console.WriteLine("У Вас нету товаров");
                Console.ReadLine();
                return;
            }

            while(IsGoods)
            {
                insideCursor.RenderCursor();
                insideCursor.Move(CursorForSelect.InputData());
            }

            if (insideCursor.Cursor.Position == dictionaryGoods.Count - 1)
            {
                return;
            }

            calculater.DeleteGood(insideCursor.Cursor.Position);

            Console.WriteLine("Данные удалены.");
            Console.ReadLine();
        }

        private void ToBack()
        {
            IsToBack = true;
        }

        private Dictionary<string, Action> CreateDictionaryGoods()
        {
            dictionaryGoods = new Dictionary<string, Action>();
            foreach (var item in calculater.GetGoods())
            {
                dictionaryGoods.Add($"Цена закупки: {item.PurchasePrice}, \n" +
                                    $"\tЦена продажи: {item.SalePrice}, \n" +
                                    $"\tМаржа: {item.Margin},\n" +
                                    $"\tКоличество:  {item.Count}", ToBackGoods);
            }
            dictionaryGoods.Add("Назад.", ToBackGoods);

            return dictionaryGoods;
        }

        private void ToBackGoods()
        {
            IsGoods = false;
        }

        private void InputDataForGood()
        {
            Console.WriteLine("Введите закупочную цену товара.");
            decimal.TryParse(Console.ReadLine(), style, culture, out purchasePrice);
            Console.WriteLine("Введите цену продажи.");
            decimal.TryParse(Console.ReadLine(), style, culture, out salePrice);
            Console.WriteLine("Введите количество.");
            int.TryParse(Console.ReadLine(), out count);
        }
    }
}


    
    