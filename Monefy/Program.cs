﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy
{

    class Program
    {
        static int Current_Account_ID;
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

        //static function to add new account
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

        //static function to add spending
        static public void SpendMoney()
        {
            //if no accounts yet, first add account
            if (accounts.Count == 0)
            {
                AddAccount();
                //get account ID to work with
                Current_Account_ID = 1;
            }
            //if accounts already exist work with existing or add new one
            else
            {
                foreach (var item in accounts)
                {
                    Console.WriteLine(item.ToString());
                }
                Console.WriteLine("======================");
            }
            Console.WriteLine($"Select the account ID or add new account by entering {accounts.Count + 1} :");
            Current_Account_ID = Int32.Parse(Console.ReadLine());
            //add new account
            if (Current_Account_ID == accounts.Count + 1)
                AddAccount();
            //print the categories list for user's choice
            foreach (var item in accounts[Current_Account_ID-1].categories)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("Select the ID of the category:");
            int Category_ID = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Select the money spent:");
            int money = Int32.Parse(Console.ReadLine());
            //category spending add
            accounts[Current_Account_ID - 1].SpendOnCategory(Category_ID,money);
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
                if (choice.Key == ConsoleKey.NumPad1 || choice.Key == ConsoleKey.D1)
                {
                    SpendMoney();
                    Console.ReadKey();
                }
                else if (choice.Key == ConsoleKey.NumPad2 || choice.Key == ConsoleKey.D2)
                {

                }
                else if (choice.Key == ConsoleKey.NumPad3 || choice.Key == ConsoleKey.D3)
                {

                }
                else if (choice.Key == ConsoleKey.NumPad4 || choice.Key == ConsoleKey.D4)
                {

                }
                else if (choice.Key == ConsoleKey.NumPad5 || choice.Key == ConsoleKey.D5)
                {

                }
                else if (choice.Key == ConsoleKey.NumPad6 || choice.Key == ConsoleKey.D6)
                {

                }
                else if (choice.Key == ConsoleKey.NumPad7 || choice.Key == ConsoleKey.D7)
                {

                }
                else
                    Environment.Exit(0);
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
