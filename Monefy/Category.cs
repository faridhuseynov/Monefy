using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy
{
    enum Type { Expense, Income}
    class Category
    {
        //category name
        public string Name { get; set; }

        //category Type
        public Type type { get; set; }

        //category ID
        public int ID { get; set; }

        //money spent on category
        public int MoneySpent { get; set; }

        //override ToString()
        public override string ToString()
        {
            return $"{ID}. {Name}: {MoneySpent}";
        }
    }
}
