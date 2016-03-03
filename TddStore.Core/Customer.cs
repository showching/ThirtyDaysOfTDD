using System;
using System.Linq;

namespace TddStore.Core
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address ShippingAddress { get; private set; }

        public Customer ()
        {
            ShippingAddress = new Address();
        }
    }
}
