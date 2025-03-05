using Commerce.Domain.Common;
using Commerce.Domain.ProductInventories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.Orders.Events
{
    public class OrderApprovedEventHandler: IEventHandler<OrderApproved>
    {
        private readonly ILocationService _locationService;
        private readonly IInventoryManagement _inventoryManagement;
        public OrderApprovedEventHandler(ILocationService locationService, IInventoryManagement inventoryManagement)
        {
            _locationService = locationService;
            _inventoryManagement = inventoryManagement;
        }

        public void Handle(OrderApproved e)
        {
            var warehouses = _locationService.FindWarehouses();
            _inventoryManagement.NotifyWarehouses(warehouses);
        }

    }
}
