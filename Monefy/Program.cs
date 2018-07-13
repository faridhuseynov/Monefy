using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy
{
    class Program
    {
        static void Main(string[] args)
        {
            Account a = new Account { Name = "AZN CASH", ID = 1, Currency = CURR.AZN, Money = 500 };
            Console.WriteLine(a.ToString());
        }
    }
}
