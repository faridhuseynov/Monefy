using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy
{
    enum CURR { AZN, USD, EURO };
    [Serializable]
    class Account
    {
        //account name
        public string Name { get; set; }

        //account's money amount
        public double Money { get; set; }

        //account's money currency
        public CURR Currency { get; set; }

        //account's hidden balance
        public double HiddenBalance { get; set; }

        //accounts ID
        public int Account_ID { get; set; }
        //list of categories
        public List<Category> categories_expense = new List<Category>()
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
        };
        public List<Category> categories_income = new List<Category>()
        {
            new Category { Name="SALARY",ID=1,type=Type.Income },
            new Category { Name="DEPOSITS",ID=2,type=Type.Income},
            new Category { Name="SAVINGS",ID=3,type=Type.Income }
        };
        //list of the Operations
        public List<Operations> Ops = new List<Operations>();
        //override ToString
        public override string ToString()
        {
            return $"Account: {Name}\nID:{Account_ID}\nBalance: {Money}{Currency.ToString()}";
        }
        //static dictionary for the currencies
        static public Dictionary<string, double> Exchange = new Dictionary<string, double> { { "AZN", 1 }, { "USD", 1.7 }, { "EURO", 2.088 } };
        
        //check operations for chosen account
        public void ShowOps()
        {
            foreach (var item in Ops)
            {
                Console.WriteLine(item.ToString());
                Console.WriteLine("============================");
            }
        }
        //add new category
        public void NewCategoryAdd(List <Category> categories,Type type)
        {
            Category category = new Category();
            Console.WriteLine("Enter the category name:");
            category.Name = Console.ReadLine();
            category.type = type;
            category.ID = categories.Count + 1;
            categories.Add(category);
        }
        //function OpsAdd
        public void OpsAdd(int CategoryID, double money, CURR currency)
        {
            //first check whether ops date should be changed
            Console.WriteLine("Do you want to specify date of the operation? Press 1 to set the date or 2 to use today's date by default");
            ConsoleKeyInfo ops_date = Console.ReadKey(true);
            //simple if function
            DateTime opsdate;
            if (ops_date.Key == ConsoleKey.NumPad1 || ops_date.Key == ConsoleKey.D1)
            {
                //enter ops date
                Console.WriteLine("Enter day of operation:");
                int day=Int32.Parse(Console.ReadLine());
                Console.WriteLine("Enter month of operation:");
                int month = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Enter year of operation:");
                int year = Int32.Parse(Console.ReadLine());
                opsdate = new DateTime(year, month, day);
            }
            else
            {
                //set the time to current time by default
                opsdate = DateTime.Now;
            }
            //note of operation added
            Console.WriteLine("Enter the note for the operation");
            string note = Console.ReadLine();
            //operation added to the list of operations
            Ops.Add(new Operations { ID_Account = Account_ID, ID_Category = CategoryID, MoneySpent = money, OpsCurrency = currency,Note=note,date=opsdate });
        }
        //money being spent 
        public void SpendOnCategory(List<Category> categories,Type category_type, double money,CURR currency)
        {
            //check whether spent currency is the same with accounts currency
            if (Currency!=currency)
            {
                if(Currency==CURR.AZN)
                    money *= Exchange[currency.ToString()];
                else if(Currency==CURR.USD){
                    if (currency==CURR.AZN)
                        money /= Exchange[Currency.ToString()];
                    else
                        money=money*Exchange[currency.ToString()]/Exchange["USD"];
                    }
                 else if(Currency==CURR.EURO){
                    if (currency==CURR.AZN)
                        money /= Exchange[Currency.ToString()];
                    else
                        money=money*Exchange[currency.ToString()]/Exchange["EURO"];
                    }
            }
            //print the categories list for user's choice
            foreach (var item in categories)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine($"Select the ID of the category or enter {categories.Count+1} to add new category:");
            int Category_ID = Int32.Parse(Console.ReadLine());
            if (Category_ID == categories.Count+1)
                NewCategoryAdd(categories,category_type);
            foreach (var item in categories)
            {
                //required category found by CategoryID
                if (item.ID==Category_ID)
                {
                    item.MoneySpent += money;
                }
            }
            //money amount subtracted from account's balance if expense 
            if (category_type == Type.Expense)
                Money -= money;
            //money amount added to account's balance if income
            else
                Money += money;
            OpsAdd(Category_ID, money, Currency);
        }
        //Edit category
        public void EditCategory(List<Category> categories,Type type, int CategoryID)
        {
            foreach (var item in categories)
            {
                if (item.ID==CategoryID)
                {
                    Category temp = new Category();
                    //set the category name
                    Console.WriteLine("Enter category name:");
                    string cat_name = Console.ReadLine();
                    //name to be uppercase and with space in beginning
                    temp.Name = " " + cat_name.ToUpper();
                    //set the category type
                    temp.type = type;
                    //set category ID
                    temp.ID = CategoryID;
                    //set category money spent
                    temp.MoneySpent = item.MoneySpent;
                    categories.Remove(item);
                    categories.Add(temp);
                    Console.WriteLine($"Category {item.Name} successfully edited");
                    return;
                }
            }
            Console.WriteLine("Category with this ID not found");
        }
        //Delete category
        public void DeleteCategory(List<Category> categories, int CategoryID)
        {
            foreach (var item in categories)
            {
                if (item.ID==CategoryID)
                {
                    categories.Remove(item);
                    Console.WriteLine($"Category {item.Name} has been successfully deleted");
                    return;
                }
            }
            Console.WriteLine("Category with specified ID not found");
        }
        //Print Specific Category
        public void PrintSpecifingCategory(List<Category> categories){
            foreach (var item in categories)
	        {
                Console.WriteLine($"{item.ToString()}{Currency.ToString()}");
	        }
        }
        //Print Categories
        public void PrintCategories()
        {
            //Print all Expense type categories
            Console.WriteLine("Expense:\n");
            foreach (var item in categories_expense)
            {
                Console.WriteLine($"{item.ToString()}{ Currency.ToString()}");
            }
            Console.WriteLine("===============================");
            //Print all Income type categories
            Console.WriteLine("Income:\n");
            foreach (var item in categories_income)
            {
                    Console.WriteLine($"{item.ToString()}{ Currency.ToString()}");
            }
        }
    }
}
