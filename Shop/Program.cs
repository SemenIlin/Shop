using System;
using Shop.CursoreConsole;
using Shop.PL;
using Shop.Settings;
using Exceptions;

namespace Shop
{
    class Program
    {
        static void Main()
        {
            Account account = new Account();
            SignInOut signInOut = new SignInOut(account);
            CalculaterOfRevenue calculater = new CalculaterOfRevenue(account);

            try
            {
                ChoiseOperationForCalculateRevevenue mainScreen = new ChoiseOperationForCalculateRevevenue(calculater, signInOut);
                while(true)
                {
                    try
                    {
                        signInOut.CursorForSelect.RenderCursor();
                        signInOut.CursorForSelect.Move(CursorForSelect.InputData());

                        while (signInOut.IsLogin())
                        {
                            try
                            {
                                mainScreen.CursorForSelect.RenderCursor();
                                mainScreen.CursorForSelect.Move(CursorForSelect.InputData());
                                while (mainScreen.CustomerSettingsScreen.IsSettings)
                                {
                                    mainScreen.CustomerSettingsScreen.CursorForSelect.RenderCursor();
                                    mainScreen.CustomerSettingsScreen.CursorForSelect.Move(CursorForSelect.InputData());
                                    while (!mainScreen.CustomerSettingsScreen.OperationOverEmployees.IsToBack)
                                    {
                                        mainScreen.CustomerSettingsScreen.OperationOverEmployees.CursorForSelect.RenderCursor();
                                        mainScreen.CustomerSettingsScreen.OperationOverEmployees.CursorForSelect.Move(CursorForSelect.InputData());
                                    }
                                    while (!mainScreen.CustomerSettingsScreen.OperationOverGoods.IsToBack)
                                    {
                                        mainScreen.CustomerSettingsScreen.OperationOverGoods.CursorForSelect.RenderCursor();
                                        mainScreen.CustomerSettingsScreen.OperationOverGoods.CursorForSelect.Move(CursorForSelect.InputData());
                                    }
                                    while (!mainScreen.CustomerSettingsScreen.OperationOverRentalSpace.IsToBack)
                                    {
                                        mainScreen.CustomerSettingsScreen.OperationOverRentalSpace.CursorForSelect.RenderCursor();
                                        mainScreen.CustomerSettingsScreen.OperationOverRentalSpace.CursorForSelect.Move(CursorForSelect.InputData());
                                    }
                                }
                            }
                            catch (ValidationException ex)
                            {
                                Console.WriteLine(ex.Message);
                                Console.ReadLine();
                            }
                            catch(ArgumentOutOfRangeException ex)
                            {
                                Console.WriteLine(ex.Message);
                                Console.ReadLine();
                            }
                            catch (InvalidCastException ex)
                            {
                                Console.WriteLine(ex.Message);
                                Console.ReadLine();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                Console.ReadLine();
                            }
                        }
                    }
                    catch(ValidationException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadLine();
                    }
                    catch (InvalidCastException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadLine();
                    }
                }                
            }
            
            catch (InvalidCastException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }

            Console.ReadLine();
        }
    }
}
