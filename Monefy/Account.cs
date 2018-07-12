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
        List<Category> categories;

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
    }
}
