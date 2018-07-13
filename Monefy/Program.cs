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
        static void Main(string[] args)
        {
            Account a = new Account { Name = "Farid", ID = 1, Currency = CURR.AZN, Money = 500 };
        }
    }
}
