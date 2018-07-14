using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy
{
    enum CURR { AZN, USD, EURO };
    class Account
    {
        //account name
        public string Name { get; set; }

        //account's money amount
        public int Money { get; set; }

        //account's money currency
        public CURR Currency { get; set; }

        //account's hidden balance
        public int HiddenBalance { get; set; }

        //accounts ID
        public int Account_ID { get; set; }

        //list of categories
        public List<Category> categories = new List<Category>()
            {
            new Category { Name=" TAXI",ID=1,type=Type.Expense},
            new Category { Name=" BILL",ID=2,type=Type.Expense},
            new Category { Name=" HOUSE",ID=3,type=Type.Expense},
            new Category { Name=" EATING OUT",ID=4,type=Type.Expense},
            new Category { Name=" TOILETRY",ID=5,type=Type.Expense},
            new Category { Name=" SPORTS",ID=6,type=Type.Expense},
            new Category { Name=" CLOTHES",ID=7,type=Type.Expense},
            new Category { Name=" HEALTH",ID=8,type=Type.Expense},
            new Category { Name=" ENTERTAINMENT",ID=9,type=Type.Expense},
            new Category { Name="TRANSPORT",ID=10,type=Type.Expense},
            new Category { Name="CAR",ID=11,type=Type.Expense},
            new Category { Name="FOOD",ID=12,type=Type.Expense},
            new Category { Name="OTHER",ID=13,type=Type.Expense},
            new Category { Name="SALARY",ID=14,type=Type.Income},
            new Category { Name="OTHER",ID=15,type=Type.Income}
        };
        //static dictionary for the currencies
        static public Dictionary<string, double> Exchange = new Dictionary<string, double>{ {"AZN",1 },{"USD",1.7 },{"EURO",2.088}};

        
        //constructor for account
        public Account()
        {
            
        }
    
        //list of the Operations
        public List<Operations> Ops=new List<Operations>();

        //override ToString
        public override string ToString()
        {
            return $"Account: {Name}\nID:{Account_ID}\nBalance: {Money}{Currency.ToString()}"; 
        }
        //add new category
        public void NewCategoryAdd(Category category)
        {
            categories.Add(category);
        }
        //function OpsAdd
        public void OpsAdd(int CategoryID, int money, CURR currency)
        {
            //set the time to current time by default, will be changed later to have option of changing the date
            DateTime opsdate = DateTime.Now;
            //note of operation added
            Console.WriteLine("Enter the note for the operation");
            string note = Console.ReadLine();
            //operation added to the list of operations
            Ops.Add(new Operations { ID_Account = this.Account_ID, ID_Category = CategoryID, MoneySpent = money, OpsCurrency = currency,Note=note });
        }

        //money being spent 
        public void SpendOnCategory( int CategoryID, int money,CURR currency)
        {
            foreach (var item in categories)
            {
                //required category found by CategoryID
                if (item.ID==CategoryID)
                {
                    item.MoneySpent += money;
                }
            }
            //spent money amount subtracted from account's balance
            Money -= money;
        }
        //Edit category
        public void EditCategory(int CategoryID)
        {
            foreach (var item in categories)
            {
                if (item.ID==CategoryID)
                {
                    Category temp = new Category();
                    //set the category name
                    Console.WriteLine("Enter category name:");
                    string cat_name = Console.ReadLine();
                    //set the category type
                    Console.WriteLine("Select category type\n1.Expense\t2.Income");
                    ConsoleKeyInfo choice = Console.ReadKey();
                    if (choice.Key == ConsoleKey.NumPad1 || choice.Key == ConsoleKey.D1)
                    {
                        temp.type = Type.Expense;
                    }
                    else
                    {
                        temp.type = Type.Income;
                    }
                    //set category ID
                    temp.ID = CategoryID;
                    //set category money spent
                    temp.MoneySpent = item.MoneySpent;
                    categories.Remove(item);
                    categories.Add(temp);
                    Console.WriteLine("Category successfully edited");
                    return;
                }
            }
            Console.WriteLine("Category with this ID not found");
        }

        //Delete category
        public void DeleteCategory(int CategoryID)
        {
            foreach (var item in categories)
            {
                if (item.ID==CategoryID)
                {
                    categories.Remove(item);
                    return;
                }
            }
            Console.WriteLine("Category with specified ID not found");
        }

        //Print Categories
        public void PrintCategories()
        {
            //Print all Expense type categories
            Console.WriteLine("Expense:\n");
            foreach (var item in categories)
            {
                if (item.type==Type.Expense)
                {
                Console.WriteLine(item.ToString());
                }
            }
            Console.WriteLine("===============================");
            //Print all Income type categories
            Console.WriteLine("Income:\n");
            foreach (var item in categories)
            {
                if (item.type == Type.Income)
                {
                    Console.WriteLine(item.ToString());
                }
            }
        }
    }
}
