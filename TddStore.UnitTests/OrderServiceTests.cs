using NUnit.Framework;
using System;
using System.Linq;
using TddStore.Core;
using TddStore.Core.Exceptions;
using Telerik.JustMock;

namespace TddStore.UnitTests
{
    [TestFixture]
    public class OrderServiceTests
    {
        private IOrderDataService _orderDataService;
        private OrderService _orderService;

        [TestFixtureSetUp]
        public void SetupTestFixture()
        {
            _orderDataService = Mock.Create<IOrderDataService>();
            _orderService = new OrderService(_orderDataService);
        }

        [Test]
        public void WhenUserPlacesACorrectOrderThenAnOrderNumberShouldBeReturned()
        {
            // Arrange
            var shoppingCart = new ShoppingCart();
            shoppingCart.Items.Add(new ShoppingCartItem { ItemId = Guid.NewGuid(), Quantity = 1 });
            var customerId = Guid.NewGuid();
            var expectedOrderId = Guid.NewGuid();

            Mock.Arrange(() => _orderDataService.Save(Arg.IsAny<Order>()))
                .Returns(expectedOrderId)
                .OccursOnce();

            // Act
            var result = _orderService.PlaceOrder(customerId, shoppingCart);

            // Assert
            Assert.AreEqual(expectedOrderId, result);
            Mock.Assert(_orderDataService);
        }

        [Test]
        public void WhenAUserAttemptsToOrderAnItemWithAQuantityOfZeroThrowInvalidOrderException()
        {
            // Arrange
            var shoppingCart = new ShoppingCart();
            shoppingCart.Items.Add(new ShoppingCartItem { ItemId = Guid.NewGuid(), Quantity = 0 });
            var customerId = Guid.NewGuid();

            Mock.Arrange(() => _orderDataService.Save(Arg.IsAny<Order>()))
                .OccursNever();

            // Act
            //_orderService.PlaceOrder(customerId, shoppingCart);

            // Assert
            Assert.Throws<InvalidOrderException>(() =>
                {
                    _orderService.PlaceOrder(customerId, shoppingCart);
                });
            Mock.Assert(_orderDataService);
        }
    }
}
