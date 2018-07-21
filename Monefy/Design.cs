using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy
{
    class Design
    {
        public void EmptyScreen()
        {
            for (int i = 0; i < 46; i++)
            {
                Console.SetCursorPosition(10, i + 3);
                if (i == 0 || i == 45)
                {
                    for (int j = 0; j < 134; j++)
                    {
                        Console.Write("=");
                    }
                }
                else
                {
                    Console.Write("|");
                    Console.SetCursorPosition(143, i + 3);
                    Console.Write("|");
                }
            }
            Console.SetCursorPosition(28, 46);
            Console.WriteLine("|      |");
            Console.SetCursorPosition(28, 47);
            Console.WriteLine("|      |");
            Console.SetCursorPosition(31, 46);
            Console.WriteLine("+");
            Console.SetCursorPosition(30, 47);
            Console.WriteLine("Add");
            Console.SetCursorPosition(28, 45);
            Console.WriteLine("========");
            Console.SetCursorPosition(110, 46);
            Console.WriteLine("|      |");
            Console.SetCursorPosition(110, 47);
            Console.WriteLine("|      |");
            Console.SetCursorPosition(113, 46);
            Console.WriteLine("-");
            Console.SetCursorPosition(111, 47);
            Console.WriteLine("Spend");
            Console.SetCursorPosition(110, 45);
            Console.WriteLine("========");
            Console.SetCursorPosition(65, 46);
            Console.WriteLine("|                   |");
            Console.SetCursorPosition(65, 47);
            Console.WriteLine("|                   |");
            Console.SetCursorPosition(67, 46);
            Console.WriteLine("Balance: ");
            Console.SetCursorPosition(65, 45);
            Console.WriteLine("=====================");
        }
        public void ScreenNotations()
        {
            Console.SetCursorPosition(60, 4);
            Console.Write("Accounts");
            Console.SetCursorPosition(60, 6);
            Console.Write("Categories");
            Console.SetCursorPosition(60, 8);
            Console.Write("Statistics");
            Console.SetCursorPosition(60, 10);
            Console.Write("Export to CSV");
            Console.SetCursorPosition(60, 12);
            Console.Write("Settings");
            Console.SetCursorPosition(60, 14);
            Console.Write("Statistics");
            Console.SetCursorPosition(60, 16);
            Console.WriteLine("Exit");
        }
    }
}
