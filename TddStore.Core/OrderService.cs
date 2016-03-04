using System;
using System.Collections.Generic;
using TddStore.Core.Exceptions;

namespace TddStore.Core
{
    public class OrderService
    {
        private IOrderDataService _orderDataService;
        private ICustomerService _customerService;
        private IOrderFulfillmentService _orderFulfillmentService;
        private string USERNAME = "Bob";
        private string PASSWORD = "Foo";

        public OrderService(IOrderDataService orderDataService, ICustomerService customerService, IOrderFulfillmentService orderFulfillmentService)
        {
            _orderDataService = orderDataService;
            _customerService = customerService;
            _orderFulfillmentService = orderFulfillmentService;
        }

        public object PlaceOrder(Guid customerId, ShoppingCart shoppingCart)
        {
            foreach (var item in shoppingCart.Items)
            {
                if (item.Quantity == 0)
                {
                    throw new InvalidOrderException();
                }
            }

            var customer = _customerService.GetCustomer(customerId);

            // Open Session
            var sessionId = _orderFulfillmentService.OpenSession(USERNAME, PASSWORD);
            var firstItemId = shoppingCart.Items[0].ItemId;
            var firstItemQty = shoppingCart.Items[0].Quantity;

            // Check Inventory Level
            var isInInventory = _orderFulfillmentService.IsInInventory(sessionId, firstItemId, firstItemQty);

            // Place Order
            var orderForFulfillmentService = new Dictionary<Guid, int>();
            orderForFulfillmentService.Add(firstItemId, firstItemQty);
            _orderFulfillmentService.PlaceOrder(sessionId, orderForFulfillmentService, customer.ShippingAddress.ToString());

            // Close Session
            _orderFulfillmentService.CloseSession(sessionId);

            var order = new Order();
            return _orderDataService.Save(order);
        }
    }
}