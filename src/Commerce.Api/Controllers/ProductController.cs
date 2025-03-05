using Commerce.Api.Common;
using Commerce.Domain.Products.Commands;
using Commerce.Domain.Products.Repositories;
using Commerce.Domain.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Commerce.Api.Controllers
{
    
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICommandHandler<DeleteProduct> _productDeleter;
        private readonly ICommandHandler<InsertProduct> _productInserter;
        private readonly ICommandHandler<UpdateProduct> _productUpdater;
        
        public ProductController(IProductRepository productRepository, ICommandHandler<DeleteProduct> productDeleter, ICommandHandler<InsertProduct> productInserter, ICommandHandler<UpdateProduct> productUpdater)
        {
            ArgumentNullException.ThrowIfNull(productRepository, nameof(productRepository));
            ArgumentNullException.ThrowIfNull(productDeleter, nameof(productDeleter));
            ArgumentNullException.ThrowIfNull(productInserter, nameof(productInserter));
            ArgumentNullException.ThrowIfNull(productUpdater, nameof(productUpdater));

            _productRepository = productRepository;
            _productDeleter = productDeleter;
            _productInserter = productInserter;
            _productUpdater = productUpdater;
        }


        [HttpGet("/api/products/")]
        public async Task<ActionResult<GenericResponse<List<ProductDto>>>> GetProducts()
        {
            var products = await _productRepository.GetAll();
            var returnedProducts = products.Select(prod => 
            new ProductDto(prod.Id, prod.Name, prod.Description, prod.IsFeatured)
            ).ToList();

            return new OkObjectResult(GenericResponse<List<ProductDto>>.Ok(returnedProducts));
        }

        [HttpPost("/api/products/delete")]
        public async Task<ActionResult<GenericResponse>> DeleteProduct([FromBody] DeleteProduct command)
        {
            await _productDeleter.ExecuteAsync(command);

            return new OkObjectResult(new GenericResponse(null, "Product deleted successfully"));
        }

        [HttpPost("/api/products/insert")]
        public async Task<ActionResult<GenericResponse>> InsertProduct([FromBody] InsertProduct command)
        {
            await _productInserter.ExecuteAsync(command);
            return new OkObjectResult(new GenericResponse(null, "Product inserted successfully"));
        }

        [HttpPost("/api/products/update")]
        public async Task<ActionResult<GenericResponse>> UpdateProduct([FromBody] UpdateProduct command)
        {
            await _productUpdater.ExecuteAsync(command);
            return new OkObjectResult(new GenericResponse(null, "Product updated successfully"));
        }

    }

    public record ProductDto(Guid Id, string Name, string Description, bool IsFeatured);
}
