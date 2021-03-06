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
        private readonly string USERNAME = "Bob";
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

            PlaceOrderWithFulfillmentService(shoppingCart, customer);

            var order = new Order();
            return _orderDataService.Save(order);
        }

        private void PlaceOrderWithFulfillmentService(ShoppingCart shoppingCart, Customer customer)
        {
            // Open Session
            var sessionId = OpenOrderFulfillmentService();

            PlaceOrderWithFulfillmentService(sessionId, shoppingCart, customer);

            // Close Session
            CloseOrderFulfillmentService(sessionId);
        }

        private void PlaceOrderWithFulfillmentService(Guid sessionId, ShoppingCart shoppingCart, Customer customer)
        {
            // Check Inventory Level
            var orderForFulfillmentService = CheckInventoryLevels(sessionId, shoppingCart);
            // Place Order
            _orderFulfillmentService.PlaceOrder(sessionId, orderForFulfillmentService, customer.ShippingAddress.ToString());
        }
 
        private Dictionary<Guid, int> CheckInventoryLevels(Guid sessionId, ShoppingCart shoppingCart)
        {
            var orderForFulfillmentService = new Dictionary<Guid, int>();

            foreach (var shoppingCartItem in shoppingCart.Items)
            {
                var itemId = shoppingCartItem.ItemId;
                var itemQty = shoppingCartItem.Quantity;
                var isInInventory = _orderFulfillmentService.IsInInventory(sessionId, itemId, itemQty);

                if (isInInventory)
                {
                    orderForFulfillmentService.Add(itemId, itemQty);
                }
            }
            return orderForFulfillmentService;
        }
 
        private void CloseOrderFulfillmentService(Guid sessionId)
        {
            _orderFulfillmentService.CloseSession(sessionId);
        }
 
        private Guid OpenOrderFulfillmentService()
        {
            var sessionId = _orderFulfillmentService.OpenSession(USERNAME, PASSWORD);
            return sessionId;
        }
    }
}