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
        private readonly IEventHandler<InventoryAdjusted> _handler;

        public AdjustInventoryCommandHandler(IInventoryRepository repository, IEventHandler<InventoryAdjusted> handler)
        {
            ArgumentNullException.ThrowIfNull(repository, nameof(repository));
            ArgumentNullException.ThrowIfNull(handler, nameof(handler));

            _repository = repository;
            _handler = handler;
        }

        public async Task ExecuteAsync(AdjustInventoryCommand command)
        {
            var quantityAdjustment = command.Quantity * (command.Decrease ? -1 : 1);
            var inventory = await _repository.GetByIdOrNull(command.ProductId) ?? new ProductInventory {
                Id = command.ProductId,
            };

            var price = inventory.Quantity + quantityAdjustment;
            if (price < 0) throw new InvalidOperationException($"Can't decrease inventory below zero");
            
            await this._repository.Save(inventory);
            this._handler.Handle(new InventoryAdjusted(command.ProductId, quantityAdjustment));

        }
    }
}
