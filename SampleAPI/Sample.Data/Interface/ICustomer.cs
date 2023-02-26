using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample.Data.Model;

namespace Sample.Data.Interface
{
    internal interface ICustomer
    {
        List<Customer> GetAllCustomer();
        Customer GetCustomer(int id);
    }
}
