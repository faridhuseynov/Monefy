using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy
{

    class Program
    {
        public void StartScreen()
        {
            Console.WriteLine("1.Add Account");
        }
        static List<Account> accounts = new List<Account>();
        static void Main(string[] args)
        {
            Account a = new Account { Name = "AZN CASH", ID = 1, Currency = CURR.AZN, Money = 500 };
            accounts.Add(a);
            accounts[0].SpendOnCategory(2,200);
            foreach (var item in accounts)
            {
            Console.WriteLine(item.ToString()); 
            }
            
        }
    }
}
