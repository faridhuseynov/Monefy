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
        static public void AccountScreen()
        {
            Console.WriteLine("1. Add\n" +
                              "2. Edit\n" +
                              "3. Delete\n" +
                              "4. Select account\n" +
                              "5. Exit\n");
        }
        // static public void InitialData()
        //{
        //    Account a = new Account { Name = "AZN CASH", Account_ID = 1, Currency = CURR.AZN, Money = 500};
        //    accounts.Add(a);
        //    accounts[0].SpendOnCategory(accounts[0].categories_expense,Type.Expense,200,CURR.AZN);
        //}
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
            int money = Int32.Parse(Console.ReadLine());
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
            int hidden = Int32.Parse(Console.ReadLine());
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
            Console.WriteLine("Enter the money spent:");
            int money = Int32.Parse(Console.ReadLine());
            //select currency of spent money
            Console.WriteLine("Select the currency of spent money:\n1.AZN\t2.USD\t3.EURO");
            ConsoleKeyInfo select = Console.ReadKey(true);
            CURR cURR = new CURR();
            if (select.Key == ConsoleKey.D1 || select.Key == ConsoleKey.NumPad1)
                cURR = CURR.AZN;
            if (select.Key == ConsoleKey.D2 || select.Key == ConsoleKey.NumPad2)
                cURR = CURR.USD;
            if (select.Key == ConsoleKey.D3 || select.Key == ConsoleKey.NumPad3)
                cURR = CURR.EURO;
            //if no accounts yet, first add account
            if (accounts.Count == 0)
            {
                AddAccount(accounts.Count+1);
                //get account ID to work with
                Current_Account_ID = 1;
                accounts[0].Currency = cURR;
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
            {
                AddAccount(accounts.Count+1);
                accounts[accounts.Count].Currency = cURR;
            }
            //select category type
            Console.WriteLine("Select category type:\n1.Expense\t2.Income");
            ConsoleKeyInfo category_select = Console.ReadKey();
            Type category_type = new Type();
            var category_list = new List<Category>();
            if (category_select.Key == ConsoleKey.D1 || category_select.Key == ConsoleKey.NumPad1)
            {
                category_type = Type.Expense;
                category_list = accounts[Current_Account_ID - 1].categories_expense;
            }
            else
            {
                category_type = Type.Income;
                category_list = accounts[Current_Account_ID - 1].categories_income;
            }
            //category spending add
            accounts[Current_Account_ID - 1].SpendOnCategory(category_list,category_type,money,cURR);
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
                        if (accounts.Count == 0)
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

                }
                else if (choice.Key == ConsoleKey.NumPad5 || choice.Key == ConsoleKey.D5)
                {
                    if (accounts.Count==0)
                    {
                        Console.WriteLine("No account found. Add account first");
                        AddAccount(1);
                        Current_Account_ID = 1;
                    }
                    if (Current_Account_ID == 0) { }

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
        }
    }
}
