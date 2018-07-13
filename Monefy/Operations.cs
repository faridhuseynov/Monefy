using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy
{
    class Operations
    {
        //account ID 
        public int AccountID { get; set; }

        //category ID
        public int CategoryID { get; set; }

        //spent money on category
        public int MoneySpent { get; set; }

        //date of the operation
        public DateTime date { get; set; } = new DateTime();

        //note on the operation
        public string  Note { get; set; }

        //currency operation was performed
        public CURR OpsCurrency { get; set; }
    }
}
