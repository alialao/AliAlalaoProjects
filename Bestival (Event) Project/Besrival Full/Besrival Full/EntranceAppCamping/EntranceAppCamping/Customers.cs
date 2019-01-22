using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntranceAppCamping
{
    public class Customers
    {
        private string fname;
        private string lname;
        private DateTime custdof;

        public Customers(string firstname, string lastname, DateTime custdob)
        {
            fname = firstname;
            lname = lastname;
            custdof = custdob;
        }


        public override String ToString()
        {
            return this.fname.ToString() + this.lname.ToString() +  " - " + this.custdof.Day + "/" + this.custdof.Month + "/" + this.custdof.Year;

        }

    

    }
}
