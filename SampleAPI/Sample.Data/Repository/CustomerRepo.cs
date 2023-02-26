using Sample.Data.Interface;
using Sample.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Data.Repository
{
    internal class CustomerRepo
    {
        List<Customer> lisCustomers = new List<Customer>
        {
            new Customer{CustomerId=1,
                Firstname = "FirstName1",
                LastName = "LastName1",
                Address= "Austin" },
            new Customer{CustomerId=2,
                Firstname = "FirstName2",
                LastName = "LastName2",
                Address= "Austin" },
            new Customer{CustomerId=3,
                Firstname = "FirstName3",
                LastName = "LastName3",
                Address= "Austin" },
            new Customer{CustomerId=4,
                Firstname = "FirstName4",
                LastName = "LastName4",
                Address= "Austin" },
            new Customer{CustomerId=5,
                Firstname = "FirstName5",
                LastName = "LastName5",
                Address= "Austin" },
        };
        public List<Customer> GetAllCustomer()
        {
            return lisCustomers;
        }
        public Customer GetCustomer(int id)
        {
            return lisCustomers.FirstOrDefault(x => x.CustomerId == id);
        }
    }
}


