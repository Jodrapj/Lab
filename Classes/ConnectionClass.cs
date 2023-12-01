using Lab.AppData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Classes
{
    public class Connect
    {
        public static DB1Entities c;
        public static DB1Entities context
        { 
            get
            {
                if(c == null)
                    c = new DB1Entities();
                return c;
            }
        }
    }
}
