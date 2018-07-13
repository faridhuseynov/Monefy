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
        public int ID { get; set; }

        //list of categories
        public List<Category> categories;

        //override ToString
        public override string ToString()
        {
            return $"Account: {Name}\nID:{ID}\nBalance: {Money}{Currency.ToString()}"; 
        }
        //add new category
        public void NewCategoryAdd(Category category)
        {
            categories.Add(category);
        }
         
        //money being spent 
        public void SpendOnCategory(int CategoryID, int money)
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
