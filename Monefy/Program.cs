using System;
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
                              "\n3. Accounts" +
                              "\n4. Categories" +
                              "\n5. Statistics" +
                              "\n6. Export to CSV" +
                              "\n7. Settings" +
                              "\n8. Exit");
        }
        //account screen
        static public void AccountScreen()
        {
            Console.WriteLine("1. Add\n" +
                              "2. Edit\n" +
                              "3. Delete\n" +
                              "4. Select account\n" +
                              "5. Exit\n");
        }
        //categories screen
        static public void CategoryScreen()
        {
            Console.WriteLine("1. Add\n" +
                              "2. Edit\n" +
                              "3. Delete\n" +
                              "4. Exit\n");
        }
        //account list print
        static public void PrintAllAccounts()
        {
            foreach (var item in accounts)
            {
                Console.WriteLine(item.ToString());
                Console.WriteLine("=========================");
            }
        }
        //account list check function
        static public bool AccountCheck()
        {
            if (accounts.Count==0)
            {
                Console.WriteLine("No account exist. To proceed further add account first");
                return false;
            }
            return true;
        }
        //static function to add new account
        static public void AddAccount(int ID)
        {
            Console.WriteLine("Enter the account name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the money balance:");
            double money = Double.Parse(Console.ReadLine());
            Console.WriteLine("Select the currency:\n1.AZN\t2.USD\t3.EURO");
            CURR cURR=new CURR();
            ConsoleKeyInfo cur = Console.ReadKey(true);
            if (cur.Key == ConsoleKey.D1 || cur.Key == ConsoleKey.NumPad1)
                cURR = CURR.AZN;
            else if (cur.Key == ConsoleKey.D2 || cur.Key == ConsoleKey.NumPad2)
                cURR = CURR.USD;
            else if (cur.Key == ConsoleKey.D3 || cur.Key == ConsoleKey.NumPad3)
                cURR = CURR.EURO;
            Console.WriteLine("Enter the money to save in hidden balance(optional):");
            double hidden = Double.Parse(Console.ReadLine());
            int id = ID;
            accounts.Add(new Account { Name = name, Money = money, Currency = cURR, HiddenBalance = hidden, Account_ID = id });
        }
        static public void EditAccount(int ID) {
            RemoveAccount(ID);
            AddAccount(ID);
            accounts = accounts.OrderBy(x => x.Account_ID).ToList();
        }
        static public void SelectAccount(int ID)
        {
            Current_Account_ID = ID;
        }
        static public void RemoveAccount(int Account_ID)
        {
            foreach (var item in accounts)
            {
                if (item.Account_ID==Account_ID)
                {
                    accounts.Remove(item);
                    break;
                }
            }
        }
        //static function to add spending
        static public void SpendMoney()
        {
            //if no accounts yet, first add account
            if (!AccountCheck())
            {
                AddAccount(1);
                //get account ID to work with
                Current_Account_ID = 1;
            }
            Console.WriteLine("Enter the money spent:");
            double money = Double.Parse(Console.ReadLine());
            //select currency of spent money
            Console.WriteLine("Select the currency of spent money:\n1.AZN\t2.USD\t3.EURO");
            ConsoleKeyInfo select = Console.ReadKey(true);
            CURR cURR = new CURR();
            if (select.Key == ConsoleKey.D1 || select.Key == ConsoleKey.NumPad1)
                cURR = CURR.AZN;
            else if (select.Key == ConsoleKey.D2 || select.Key == ConsoleKey.NumPad2)
                cURR = CURR.USD;
            else if (select.Key == ConsoleKey.D3 || select.Key == ConsoleKey.NumPad3)
                cURR = CURR.EURO;
            //if accounts already exist work with existing or add new one
            else
            {
                foreach (var item in accounts)
                {
                    Console.WriteLine(item.ToString());
                }
                Console.WriteLine("======================");
                Console.WriteLine($"Select the account ID or add new account by entering {accounts.Last().Account_ID + 1} :");
                Current_Account_ID = Int32.Parse(Console.ReadLine());
                //add new account
                if (Current_Account_ID == accounts.Last().Account_ID + 1)
                    AddAccount(Current_Account_ID);
            }
            //category spending add
            accounts[Current_Account_ID - 1].SpendOnCategory(accounts[Current_Account_ID - 1].categories_expense, Type.Expense,money,cURR);
        }
        //static function to review all operations
        static public void ShowOperations()
        {
            accounts[Current_Account_ID - 1].ShowOps();
        }
        //static function to add money
        static public void AddMoney()
        {
            //if no accounts yet, first add account
            if (!AccountCheck())
            {
                AddAccount(1);
                //get account ID to work with
                Current_Account_ID = 1;
                accounts[0].categories_income[0].MoneySpent = accounts[0].Money;
            }
            else
            {
                Console.WriteLine("Enter the money amount:");
                double money = Double.Parse(Console.ReadLine());
                //select currency of spent money
                Console.WriteLine("Select the currency:\n1.AZN\t2.USD\t3.EURO");
                ConsoleKeyInfo select = Console.ReadKey(true);
                CURR cURR = new CURR();
                if (select.Key == ConsoleKey.D1 || select.Key == ConsoleKey.NumPad1)
                    cURR = CURR.AZN;
                else if (select.Key == ConsoleKey.D2 || select.Key == ConsoleKey.NumPad2)
                    cURR = CURR.USD;
                else if (select.Key == ConsoleKey.D3 || select.Key == ConsoleKey.NumPad3)
                    cURR = CURR.EURO;
                //category spending add
                accounts[Current_Account_ID - 1].SpendOnCategory(accounts[Current_Account_ID - 1].categories_income, Type.Income, money, cURR);
            }
        }

        static void Main(string[] args)
        {
            ConsoleKeyInfo choice;
            //InitialData();
            while (true)
            {
                Console.Clear();
                StartScreen();
                choice = Console.ReadKey(true);
                if (choice.Key == ConsoleKey.NumPad1 || choice.Key == ConsoleKey.D1)
                {
                    SpendMoney();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else if (choice.Key == ConsoleKey.NumPad2 || choice.Key == ConsoleKey.D2)
                {
                    AddMoney();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else if (choice.Key == ConsoleKey.NumPad3 || choice.Key == ConsoleKey.D3)
                {
                    //print all existing accounts
                    Console.Clear();
                    PrintAllAccounts();
                    //after printing new screen appears allowing work with accounts
                    AccountScreen();
                    //selection of the next operation with accounts
                    ConsoleKeyInfo accountSelection = Console.ReadKey(true);
                    if (accountSelection.Key == ConsoleKey.NumPad1 || accountSelection.Key == ConsoleKey.D1) {
                        if (!AccountCheck())
                            AddAccount(1);
                        else
                            AddAccount(accounts.Last().Account_ID + 1);
                    }
                    else if (accountSelection.Key == ConsoleKey.D2 || accountSelection.Key == ConsoleKey.NumPad2)
                    {
                        if (AccountCheck())
                        {
                            Console.WriteLine("Enter the ID of the account you want to edit: ");
                            int ID = Int32.Parse(Console.ReadLine());
                            EditAccount(ID);
                        }                     
                    }
                    else if (accountSelection.Key == ConsoleKey.D3 || accountSelection.Key == ConsoleKey.NumPad3)
                    {
                        if (AccountCheck()){
                            Console.WriteLine("Enter the ID of the account you want to delete: ");
                            int ID = Int32.Parse(Console.ReadLine());
                            RemoveAccount(ID);
                        }
                    }
                    else if(accountSelection.Key == ConsoleKey.D4 || accountSelection.Key == ConsoleKey.NumPad4)
                    {
                        if (AccountCheck())
                        {
                            Console.WriteLine("Enter the ID of the account");
                            int id = Int32.Parse(Console.ReadLine());
                            SelectAccount(id);
                        }
                    }
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else if (choice.Key == ConsoleKey.NumPad4 || choice.Key == ConsoleKey.D4)
                {
                    if (!AccountCheck())
                    {
                        AddAccount(1);
                        Current_Account_ID = 1;
                        accounts[0].categories_income[0].MoneySpent = accounts[0].Money;
                    }
                    else if (accounts.Count != 0 && Current_Account_ID == 0)
                    {
                        Console.WriteLine("You have not chosen the account to work with, please select account first");
                        PrintAllAccounts();
                        Console.WriteLine("Select ID of the account");
                        int id = Int32.Parse(Console.ReadLine());
                        SelectAccount(id);
                    }
                    accounts[Current_Account_ID-1].PrintCategories();
                    
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                }
                else if (choice.Key == ConsoleKey.NumPad5 || choice.Key == ConsoleKey.D5)
                {
                    

                }
                else if (choice.Key == ConsoleKey.NumPad6 || choice.Key == ConsoleKey.D6)
                {
                    accounts[Current_Account_ID - 1].ShowOps();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else if (choice.Key == ConsoleKey.NumPad7 || choice.Key == ConsoleKey.D7)
                {

                }
                else
                    Environment.Exit(0);
            }        
        }
    }
}
