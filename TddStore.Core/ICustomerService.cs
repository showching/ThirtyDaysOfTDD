using System;
using System.Linq;

namespace TddStore.Core
{
    public interface ICustomerService
    {
        Customer GetCustomer(Guid customerId);
    }
}
