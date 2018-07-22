using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Monefy
{
    [Serializable]
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
                              "\n8. Save/Load data" +
                              "\n9. Exit");
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
            //check the amoun of money, if 0 or negative then stop
            if (money<=0)
            {
                Console.WriteLine($"Money can not be {money}, repeat again");
                return;
            }
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
        //add category function
        static public void AddCategory(List <Category> categories, Type type)
        {
            Category category = new Category();
            Console.WriteLine("Enter category name:");
            category.Name = Console.ReadLine().ToUpper();
            category.type = type;
            if (categories.Count == 0)
            {
                categories.Add(category);
                categories[0].ID = 1;
            }
            else
            {
                category.ID = categories.Last().ID + 1;
                categories.Add(category);
            }
        }
        //statistics function for operations. 2 dates are set as input, mainly for week or 2 different day intervals and
        //set with default values in order to be able to work even with 1 argument for statistics per year, month, daily
        //int type defines whether it will be date interval or daily, monthly, annual
        static public void Statistics(DateTime date1, DateTime date2)
        {
            List<Operations> result=new List<Operations>();
            //result is the list of the operations performed on the specified date
            result = accounts[Current_Account_ID - 1].Ops.Where(x => x.date >= date1 && x.date <= date2 && x.MoneySpent != 0).ToList();
            //double sum is the total amount of money spent on the specified date
            double sum = result.Sum(x=>x.MoneySpent);
            if (sum != 0)
            {
                //dictionary category_spend is the dictionary that will be used to get the money spent per category
                //key is the Category_ID value and value is the money spent
                Dictionary<int, double> category_spend = new Dictionary<int, double>();
                foreach (var item in result)
                    category_spend.Add(item.ID_Category, 0);
                for (int i = 0; i < result.Count; i++)
                    category_spend[result[i].ID_Category] += result[i].MoneySpent;
                foreach (var item in result)
                    Console.WriteLine($"{accounts[Current_Account_ID-1].categories_expense[item.ID_Category-1].Name}:{category_spend[item.ID_Category]/sum*100}%");
            }
            else
                Console.WriteLine("No spending on chosen time interval");
        }
        //function to load and save the data
        static public void StoreData()
        {
            Console.WriteLine("Press S to save or L to load");
            ConsoleKeyInfo data_selection = Console.ReadKey(true);
            BinaryFormatter formatter = new BinaryFormatter();
            if (data_selection.Key == ConsoleKey.S)
            {
                Console.WriteLine("Enter the file name");
                string data = Console.ReadLine();
                using (FileStream fs = new FileStream($"{data}.dat", FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, accounts);
                    Console.WriteLine("File was successfully saved");
                }
            }
            else if (data_selection.Key == ConsoleKey.L)
            {
                if (accounts.Count != 0)
                    Console.WriteLine("The data already loaded");
                else
                {

                    Console.WriteLine("Enter the file name");
                    string data = Console.ReadLine();
                    using (FileStream fs = new FileStream($"{data}.dat", FileMode.OpenOrCreate))
                    {
                        accounts = (List<Account>)formatter.Deserialize(fs);
                        Console.WriteLine("File was successfully loaded");
                    }
                }
            }
        }
        //function to export to CSV
        static void ExportToCSV()
        {
            //create csv stringbuilder
            var csv = new StringBuilder();
            //write the column names first
            csv.AppendLine("Account ID,Category ID,Amount,Currency,Date,Note");
            //first loop is for accounts that have performed the operations
            for (int i = 0; i < accounts.Count; i++)
            {
                //second loop is for operations that were performed
                for (int j = 0; j < accounts[i].Ops.Count; j++)
                {
                    //get account ID
                    var first = accounts[i].Ops[j].ID_Account.ToString();
                    //get category ID
                    var second = accounts[i].Ops[j].ID_Category.ToString();
                    //get money amount
                    var third = accounts[i].Ops[j].MoneySpent.ToString();
                    //get currency of the amount
                    var fourth = accounts[i].Ops[j].OpsCurrency.ToString();
                    //get date of the operation
                    var fifth = accounts[i].Ops[j].date.ToString();
                    //get the note of the operation
                    var sixth = accounts[i].Ops[j].Note.ToString();
                    //all values combined in 1 line string
                    var newLine = string.Format($"{first},{second},{third},{fourth},{fifth},{sixth}");
                    //append line newLine into the csv stringbuilder
                    csv.AppendLine(newLine);
                }
            }
            //save all csv text in data.csv file
            Console.WriteLine("Enter csv file name:");
            string data = Console.ReadLine();
            File.AppendAllText($"{data}.csv", csv.ToString());
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
                    Console.WriteLine("=================================================");
                    CategoryScreen();
                    ConsoleKeyInfo category_selection = Console.ReadKey(true);
                    if (category_selection.Key != ConsoleKey.NumPad4 && category_selection.Key != ConsoleKey.D4)
                    {
                        Console.WriteLine("Select category type:\n1.Expense\t2.Income");
                        ConsoleKeyInfo cat_type = Console.ReadKey(true);
                        if (category_selection.Key == ConsoleKey.NumPad1 || category_selection.Key == ConsoleKey.D1)
                        {
                            if (cat_type.Key == ConsoleKey.NumPad1 || cat_type.Key == ConsoleKey.D1)
                                AddCategory(accounts[Current_Account_ID - 1].categories_expense, Type.Expense);
                            else
                                AddCategory(accounts[Current_Account_ID - 1].categories_income, Type.Income);
                        }
                        else if (category_selection.Key == ConsoleKey.NumPad2 || category_selection.Key == ConsoleKey.D2)
                        {
                            Console.WriteLine("Select category ID to edit:");
                            int ID = Int32.Parse(Console.ReadLine());
                            if (cat_type.Key == ConsoleKey.NumPad1 || cat_type.Key == ConsoleKey.D1)
                            {
                                accounts[Current_Account_ID - 1].EditCategory(accounts[Current_Account_ID - 1].categories_expense, Type.Expense, ID);
                                accounts[Current_Account_ID - 1].categories_expense = accounts[Current_Account_ID - 1].categories_expense.OrderBy(p => p.ID).ToList();
                            }
                            else
                            {
                                accounts[Current_Account_ID - 1].EditCategory(accounts[Current_Account_ID - 1].categories_income, Type.Income, ID);
                                accounts[Current_Account_ID - 1].categories_income = accounts[Current_Account_ID - 1].categories_income.OrderBy(p => p.ID).ToList();
                            }
                        }
                        else if (category_selection.Key == ConsoleKey.NumPad3 || category_selection.Key == ConsoleKey.D3)
                        {
                            Console.WriteLine("Select category ID to delete:");
                            int ID = Int32.Parse(Console.ReadLine());
                            if (cat_type.Key == ConsoleKey.NumPad1 || cat_type.Key == ConsoleKey.D1)
                                accounts[Current_Account_ID - 1].DeleteCategory(accounts[Current_Account_ID - 1].categories_expense, ID);
                            else
                                accounts[Current_Account_ID - 1].DeleteCategory(accounts[Current_Account_ID - 1].categories_income, ID);

                        }


                    }

                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else if (choice.Key == ConsoleKey.NumPad5 || choice.Key == ConsoleKey.D5)
                {
                    //user selects date type for search
                    Console.WriteLine("For date interval - press 1; for annual, monthly or daily - press 2");
                    ConsoleKeyInfo type_select = Console.ReadKey(true);
                    if (type_select.Key == ConsoleKey.D1 || type_select.Key == ConsoleKey.NumPad1)
                    {
                        Console.WriteLine("For day interval press 1 for weekly statistics press 2");
                        type_select = Console.ReadKey(true);
                        if (type_select.Key == ConsoleKey.NumPad1 || type_select.Key == ConsoleKey.D1)
                        {
                            //date for date 1
                            Console.WriteLine("Enter 'from' reference year:");
                            int year = Int32.Parse(Console.ReadLine());
                            Console.WriteLine("Enter 'from' reference month:");
                            int month = Int32.Parse(Console.ReadLine());
                            Console.WriteLine("Enter 'from' reference day:");
                            int day = Int32.Parse(Console.ReadLine());
                            DateTime date1 = new DateTime(year, month, day);
                            //date for date2
                            Console.WriteLine("Enter 'to' reference year:");
                             year = Int32.Parse(Console.ReadLine());
                            Console.WriteLine("Enter 'to' reference month:");
                             month = Int32.Parse(Console.ReadLine());
                            Console.WriteLine("Enter 'to' reference day:");
                             day = Int32.Parse(Console.ReadLine());
                            DateTime date2 = new DateTime(year, month, day);
                            Statistics(date1,date2);
                        }
                        else if (type_select.Key == ConsoleKey.NumPad2 || type_select.Key == ConsoleKey.D2)
                        {
                            //date for date reference
                            Console.WriteLine("Enter 'from' reference year:");
                            int year = Int32.Parse(Console.ReadLine());
                            Console.WriteLine("Enter 'from' reference month:");
                            int month = Int32.Parse(Console.ReadLine());
                            Console.WriteLine("Enter 'from' reference day:");
                            int day = Int32.Parse(Console.ReadLine());
                            DateTime date1 =  new DateTime(year,month,day);
                            DateTime date2 = new DateTime();
                            date2 = date1;
                            date2=date2.AddDays(7);
                            Statistics(date1, date2);
                        }
                    }
                    else if (type_select.Key==ConsoleKey.D2||type_select.Key==ConsoleKey.NumPad2)
                    {
                        Console.WriteLine("For daily statistics press 1, for monthly press 2 for annual press 3");
                        type_select = Console.ReadKey(true);
                        if (type_select.Key == ConsoleKey.NumPad1 || type_select.Key == ConsoleKey.D1)
                        {
                            Console.WriteLine("Enter year:");
                            int year = Int32.Parse(Console.ReadLine());
                            Console.WriteLine("Enter month:");
                            int month = Int32.Parse(Console.ReadLine());
                            Console.WriteLine("Enter day:");
                            int day = Int32.Parse(Console.ReadLine());
                            DateTime date1 = new DateTime(year, month, day);
                            DateTime date2 = new DateTime(year, month, day,23,59,59);
                            Statistics(date1,date2);
                        }
                        else if (type_select.Key == ConsoleKey.NumPad2 || type_select.Key == ConsoleKey.D2)
                        {
                            Console.WriteLine("Enter year:");
                            int year = Int32.Parse(Console.ReadLine());
                            Console.WriteLine("Enter month:");
                            int month = Int32.Parse(Console.ReadLine());
                            DateTime date1 = new DateTime(year, month, 1);
                            DateTime date2 = new DateTime(year, month, 31);
                            Statistics(date1,date2);
                        }
                        else if (type_select.Key == ConsoleKey.NumPad3 || type_select.Key == ConsoleKey.D3)
                        {
                            Console.WriteLine("Enter year:");
                            int year = Int32.Parse(Console.ReadLine());
                            DateTime date1 = new DateTime(year, 1, 1);
                            DateTime date2 = new DateTime(year, 12, 31);
                            Statistics(date1,date2);
                        }
                    }
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else if (choice.Key == ConsoleKey.NumPad6 || choice.Key == ConsoleKey.D6)
                {
                    ExportToCSV();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else if (choice.Key == ConsoleKey.NumPad7 || choice.Key == ConsoleKey.D7)
                {

                }
                else if (choice.Key == ConsoleKey.NumPad8 || choice.Key == ConsoleKey.D8)
                {
                    StoreData();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                    Environment.Exit(0);
            }        
        }
    }
}
