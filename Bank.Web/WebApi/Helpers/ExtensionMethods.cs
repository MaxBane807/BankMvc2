using Bank.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Web.WebApi.Helpers
{
    public static class ExtensionMethods
    {
        public static IEnumerable<Customers> WithoutPasswords(this IEnumerable<Customers> customers)
        {
            return customers.Select(x => x.WithoutPassword());
        }

        public static Customers WithoutPassword(this Customers customer)
        {
            customer.Password = null;
            return customer;
        }
    }
}
