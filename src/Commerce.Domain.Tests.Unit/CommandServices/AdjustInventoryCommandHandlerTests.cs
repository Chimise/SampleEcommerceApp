using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commerce.Domain.ProductInventories.Commands;
using Commerce.Domain.ProductInventories.Events;
using Commerce.Domain.Tests.Unit.Fakes;
using Commerce.Domain.ProductInventories;
using Xunit;

namespace Commerce.Domain.Tests.Unit.CommandServices
{
    public class AdjustInventoryCommandHandlerTests
    {
        [Fact]
        public void CreateWithNullRepositoryWillThrow()
        {
            Action action = () => new AdjustInventoryCommandHandler(repository: null!, inventoryAdjuster: new StubEventHandler<InventoryAdjusted>());

            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void CreateWithNullHandlerWillThrow()
        {
            Action action = () => new AdjustInventoryCommandHandler(repository: new InMemoryInventoryRepository(), inventoryAdjuster: null!);

            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public async void IncreasingInventoryOnNewProductPublishesExpectedEvent()
        {
            Guid productId = Guid.NewGuid();
            var command = new AdjustInventoryCommand { ProductId = productId, Decrease = false, Quantity = 10 };
            var expectedEvent = new { ProductId = productId, QuantityAdjustment = 10 };
            var handler = new SpyEventHandler<InventoryAdjusted>();
            var sut = new AdjustInventoryCommandHandler(new InMemoryInventoryRepository(), handler);

            await sut.ExecuteAsync(command);

            Assert.Equivalent(
                expected: expectedEvent,
                actual: new {handler.HandledEvent.ProductId, handler.HandledEvent.QuantityAdjustment},
                strict: true
                );
        }

        [Fact]
        public
            async void DecreasingInventoryOnNewProductPublishesExpectedEvent()
        {
            var productId = Guid.NewGuid();
            var command = new AdjustInventoryCommand { ProductId = productId, Decrease = true, Quantity = 10 };
            var expectedEvent = new { ProductId = productId, QuantityAdjustment = -10 };
            var handler = new SpyEventHandler<InventoryAdjusted>();
            var repository = new InMemoryInventoryRepository();
            var inventory = new ProductInventory { Id = productId, Quantity = 20 };
            repository.Save(inventory);

            var sut = new AdjustInventoryCommandHandler(repository, handler);

            await sut.ExecuteAsync(command);

            Assert.Equivalent(
                expected: expectedEvent,
                actual: new { handler.HandledEvent.ProductId, handler.HandledEvent.QuantityAdjustment },
                strict: true
                );
        }

    }
}
