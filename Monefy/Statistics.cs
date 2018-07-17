using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy
{
    static class Extensions
    {
        static public double Statistics (this List<Operations> operations, DateTime date)
        {
            double spent_money = 0;
            foreach (var item in operations)
            {
                if (item.date.Date==date.Date)
                {
                    spent_money += item.MoneySpent;
                }
            }
            return spent_money;
        } 
    }
}
