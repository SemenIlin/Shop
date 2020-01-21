using System;
using System.Collections.Generic;

namespace Shop.CursoreConsole
{
    public class CursorForSelect
    {  
        private readonly Dictionary<string, Action> action;
        private readonly string[] commands;
        private readonly Cursor cursor = new Cursor { View = '>', Position = 0 };
        
        private char[] choise;

        public CursorForSelect(Dictionary<string, Action> action)
        {
            this.action = action;
            commands = new string[action.Count];
            CreateCommands();
            ChoiseUser();
        }

        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    cursor.Position -= 1;
                    break;
                case Direction.Down:
                    cursor.Position += 1;
                    break;
                case Direction.Enter:
                    action[commands[cursor.Position]].Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), $"Unknown direction: {direction}");
            }
            VerifyOutOfBounds();
        }

        public Cursor Cursor { get{ return cursor; } }

        public void RenderCursor()
        {
            Console.Clear();        

            for ( int position = 0 ; position < choise.Length; position++)
            {
                if (position == cursor.Position)
                {
                    choise[position] = cursor.View;
                }
                else
                {
                    choise[position] = ' ';
                }
                

                Console.Write(choise[position] + " " + commands[position]);
                Console.WriteLine();
            }
        }

        public static Direction InputData()
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    return Direction.Up;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    return Direction.Down;
                case ConsoleKey.Enter:
                    return Direction.Enter;
                default:
                    return InputData();
            }
        }

        private void VerifyOutOfBounds()
        {
            if (cursor.Position >= choise.Length)
            {
                cursor.Position = choise.Length - 1;
            }
            else if (cursor.Position <= 0)
            {
                cursor.Position = 0;
            }
        }

        private string[] CreateCommands()
        {
            int iterator = 0;
            foreach (var item in action)
            {
                commands[iterator] = item.Key;
                ++iterator;                
            }

            return commands;
        }

        private void ChoiseUser()
        {
            choise = new char[action.Count];
        }
    }
}
