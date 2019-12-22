using System;

namespace Shop
{
    class Program
    {
        static void Main()
        {
            MyShop myShop = new MyShop();
            var tableExpenses = new ConsoleTable("Аренда", "Зарплата", "Закупка", "Итого");
            var tableRevenueFromSales = new ConsoleTable("№", "Цена продажи","Наценка", "Количество", "Итого с продаж");

            decimal purchaseToy;
            decimal salesToy;
            decimal surcharge;
            decimal totalRevenue;
            int count;

            Console.WriteLine("Введите свой бюджет.");
            myShop.Budget = myShop.VerificationOfData();    
            
            try
            {
                Console.WriteLine("Введите арендную плату.");
                myShop.ValueRentalSpace = myShop.VerificationOfData();
                myShop.Budget -= myShop.ValueRentalSpace;
                if (myShop.Budget <= 0)
                {
                    throw new Exception();
                }
                myShop.CreateRentalSpace();

                Console.WriteLine("Введите зарплату бухгалтеру.");

                myShop.SalaryForAccountant = myShop.VerificationOfData();
                myShop.Budget -= myShop.SalaryForAccountant;
                if (myShop.Budget <= 0)
                {
                    throw new Exception();
                }

                Console.WriteLine("Введите зарплату продавцу.");

                myShop.SalaryForSeller = myShop.VerificationOfData();
                myShop.Budget -= myShop.SalaryForSeller;
                if (myShop.Budget <= 0)
                {
                    throw new Exception();
                }

                Console.WriteLine("Введите зарплату грузчику.");

                myShop.SalaryForPorter = myShop.VerificationOfData();
                myShop.Budget -= myShop.SalaryForPorter;
                if (myShop.Budget <= 0)
                {
                    throw new Exception();
                }

                Console.WriteLine("Введите зарплату закупщику.");

                myShop.SalaryForPurchasingAgent = myShop.VerificationOfData();
                myShop.Budget -= myShop.SalaryForPurchasingAgent;
                if (myShop.Budget <= 0)
                {
                    throw new Exception();
                }

                myShop.AddStaff();

                Console.WriteLine("Введите закупочную цену игрушки.");
                myShop.PricePurchaseToy = myShop.VerificationOfData();
                if (myShop.Budget >= myShop.PricePurchaseToy)
                {
                    myShop.NumberOfToys = (int)Math.Floor(myShop.Budget / myShop.PricePurchaseToy);
                    Console.WriteLine("На оставшиеся деньги можно купить {0} игрушек.", myShop.NumberOfToys);

                    myShop.Budget -= myShop.PricePurchaseToy * myShop.NumberOfToys;
                }

                Console.WriteLine("Введите цену при продажи игрушки.");
                myShop.PriceSalesToy = myShop.VerificationOfData();

                myShop.CreateGoods();

                purchaseToy = myShop.GetGoods().PurchasePrice;
                count = myShop.GetGoods().Count;
                salesToy = myShop.GetGoods().SalePrice;
                surcharge = salesToy - purchaseToy;
                totalRevenue = salesToy * count;

                myShop.CreateExpenses();

                tableExpenses.AddRow(myShop.GetTotalConstExpenses().RentalSpaseExpenses.ToString(),
                                     myShop.GetTotalConstExpenses().EmployeesExpenses.ToString(),
                                     myShop.GetTotalVariableExpenses().VariableExpenses.ToString(),
                                     myShop.GetTotalExpenses().Expenses.ToString());

                tableRevenueFromSales.AddRow("1", salesToy.ToString(), surcharge.ToString(), count.ToString(), totalRevenue.ToString());

                Console.WriteLine("Затраты");
                tableExpenses.Print();
                Console.WriteLine();
                Console.WriteLine("Доход от продажи товара");
                tableRevenueFromSales.Print();

                myShop.CreateDelta(1);
                Console.WriteLine(" Дельта за один месяц: {0}.", myShop.GetDelta().DeltaFromShop);
            }
            catch
            {                
                Console.WriteLine("No money");
            }

            Console.ReadLine();
        }
    }
}
