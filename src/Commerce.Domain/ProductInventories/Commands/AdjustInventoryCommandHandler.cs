using Commerce.Domain.Common;
using Commerce.Domain.ProductInventories.Events;
using Commerce.Domain.ProductInventories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.ProductInventories.Commands
{
    public class AdjustInventoryCommandHandler: ICommandHandler<AdjustInventoryCommand>
    {
        private readonly IInventoryRepository _repository;
        private readonly IEventHandler<InventoryAdjusted> _inventoryAdjuster;

        public AdjustInventoryCommandHandler(IInventoryRepository repository, IEventHandler<InventoryAdjusted> inventoryAdjuster)
        {
            ArgumentNullException.ThrowIfNull(repository, nameof(repository));
            ArgumentNullException.ThrowIfNull(inventoryAdjuster, nameof(inventoryAdjuster));

            _repository = repository;
            _inventoryAdjuster = inventoryAdjuster;
        }

        public async Task ExecuteAsync(AdjustInventoryCommand command)
        {
            var quantityAdjustment = command.Quantity * (command.Decrease ? -1 : 1);
            var inventory = await _repository.GetByIdOrNull(command.ProductId) ?? new ProductInventory {
                Id = command.ProductId,
            };

            var price = inventory.Quantity + quantityAdjustment;
            if (price < 0) throw new InvalidOperationException($"Can't decrease inventory below zero");
            
            this._repository.Save(inventory);
            this._inventoryAdjuster.Handle(new InventoryAdjusted(command.ProductId, quantityAdjustment));

        }
    }
}
