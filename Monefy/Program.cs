using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy
{

    class Program
    {
        static List<Account> accounts = new List<Account>();
        static public void StartScreen()
        {
            Console.WriteLine("1. Spend" +
                              "\n2. Add money" +
                              "\n3. Account" +
                              "\n4. Categories" +
                              "\n5. Statistics" +
                              "\n6. Export to CSV" +
                              "\n7. Settings" +
                              "\n8. Exit");
        }

        static public void InitialData()
        {
            Account a = new Account { Name = "AZN CASH", Account_ID = 1, Currency = CURR.AZN, Money = 500 };
            accounts.Add(a);
            accounts[0].SpendOnCategory(2, 200);
        }

        static public void AddAccount()
        {
            Console.WriteLine("Enter the account name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the money balance:");
            int money = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Select the currency:\n1.AZN\t2.USD\t3.EURO");
            CURR cURR=new CURR();
            ConsoleKeyInfo cur = Console.ReadKey(true);
            if (cur.Key == ConsoleKey.D1 || cur.Key == ConsoleKey.NumPad1)
                cURR = CURR.AZN;
            else if (cur.Key == ConsoleKey.D2 || cur.Key == ConsoleKey.NumPad2)
                cURR = CURR.USD;
            else if (cur.Key == ConsoleKey.D2 || cur.Key == ConsoleKey.NumPad2)
                cURR = CURR.EURO;
            Console.WriteLine("Enter the money to save in hidden balance(optional):");
            int hidden = Int32.Parse(Console.ReadLine());
            int id = accounts.Count + 1;
            accounts.Add(new Account { Name = name, Money = money, Currency = cURR, HiddenBalance = hidden, Account_ID = id });
        }

        static void Main(string[] args)
        {

            ConsoleKeyInfo choice;
            InitialData();
            while (true)
            {
                Console.Clear();
                StartScreen();
                choice = Console.ReadKey(true);
                if (choice.Key==ConsoleKey.NumPad1|| choice.Key == ConsoleKey.D1)
                {
                    if (accounts.Count==0)
                    {
                        AddAccount();
                    }

                }
                else if (choice.Key == ConsoleKey.NumPad2 || choice.Key == ConsoleKey.D2)
                {

                }
                else if (choice.Key == ConsoleKey.NumPad3 || choice.Key == ConsoleKey.D3)
                {

                }
                else if (choice.Key == ConsoleKey.NumPad3 || choice.Key == ConsoleKey.D4)
                {

                }
                else if (choice.Key == ConsoleKey.NumPad3 || choice.Key == ConsoleKey.D5)
                {

                }
                else if (choice.Key == ConsoleKey.NumPad3 || choice.Key == ConsoleKey.D6)
                {

                }
                else if (choice.Key == ConsoleKey.NumPad3 || choice.Key == ConsoleKey.D7)
                {

                }
            }
            //foreach (var item in accounts)
            //{
            //    Console.WriteLine(item.ToString());
            //    foreach (var item2 in accounts[0].categories)
            //    {
            //        Console.WriteLine(item2.ToString());
            //    }
            //}
            
        }
    }
}
