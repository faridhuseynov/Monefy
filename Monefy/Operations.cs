﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy
{
    [Serializable]
    class Operations
    {
        //account ID 
        public int ID_Account { get; set; }

        //category ID
        public int ID_Category { get; set; }

        //spent money on category
        public double MoneySpent { get; set; }

        //date of the operation
        public DateTime date { get; set; } = new DateTime();

        //note on the operation
        public string  Note { get; set; }

        //currency operation was performed
        public CURR OpsCurrency { get; set; }

        //override ToString() function
        public override string ToString()
        {
            return $"Category ID: {ID_Category}\nMoney spent: { MoneySpent}\nOps Currency: { OpsCurrency.ToString()}\n" +
                $"Note: { Note}\nDate: { date.ToString()}";
        }
    }
}
               