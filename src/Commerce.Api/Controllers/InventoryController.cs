using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Commerce.Domain.Products.Repositories;
using Commerce.Api.Common;
using Commerce.Api.Dtos.Inventories;
using System.Runtime.CompilerServices;
using Commerce.Domain.Common;
using Commerce.Domain.ProductInventories.Commands;

namespace Commerce.Api.Controllers
{
    [Route("api/inventory")]
    [ApiController]
    public class AdjustInventoryController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICommandHandler<AdjustInventoryCommand> _adjustInventorHandler;
        public AdjustInventoryController(IProductRepository productRepository, ICommandHandler<AdjustInventoryCommand> adjustInventoryHandler)
        {
            _productRepository = productRepository;
            _adjustInventorHandler = adjustInventoryHandler;
        }

        [HttpGet]
        public async Task<ActionResult<GenericResponse<IEnumerable<DisplayProduct>>>> GetProducts()
        {
            var allProducts = await _productRepository.GetAll();
            var productInventories = allProducts.Select(prod => new DisplayProduct(prod.Name, prod.Id.ToString()));

            return new OkObjectResult(GenericResponse<IEnumerable<DisplayProduct>>.Ok(productInventories));
        }

        [HttpPost("adjustinventory")]
        public async Task<ActionResult<GenericResponse>> AdjustInventory(AdjustInventoryRequest req)
        {
            await _adjustInventorHandler.ExecuteAsync(new AdjustInventoryCommand { Decrease = req.Decrease, ProductId = req.ProductId, Quantity = req.Quantity });
            return new OkObjectResult(GenericResponse.Ok(successMessage: "Inventory adjustment was successful"));
        }

    }

    public record DisplayProduct(string name, string Id);
}
