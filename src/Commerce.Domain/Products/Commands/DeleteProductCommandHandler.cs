using Commerce.Domain.Common;
using Commerce.Domain.Products.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.Products.Commands
{
    public class DeleteProductCommandHandler: ICommandHandler<DeleteProduct>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            ArgumentNullException.ThrowIfNull(productRepository, nameof(productRepository));
            _productRepository = productRepository;
        }
        
        public async Task ExecuteAsync(DeleteProduct command) 
        {
            await _productRepository.Delete(command.ProductId);
        }
    }
}
