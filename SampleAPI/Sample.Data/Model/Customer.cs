using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Data.Model
{
    internal class Customer
    {
        public int CustomerId { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }

        public string Address { get; set; }
    }
}
