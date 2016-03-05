using System;
using System.Collections.Generic;
using System.Linq;

namespace TddStore.Core
{
    public interface IOrderFulfillmentService
    {
        Guid OpenSession(string user, string password);
        bool IsInInventory(Guid sessionId, Guid itemNumber, int quantity);
        bool PlaceOrder(Guid sessionId, IDictionary<Guid, int> items, string mailingAddress);
        void CloseSession(Guid sessionId);
    }
}
