using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Services.Interfaces
{
    interface ITotalAmountService
    {
        decimal getTotalAmountByCustomerID(int id);
    }
}
