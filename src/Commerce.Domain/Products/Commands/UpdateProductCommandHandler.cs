using Commerce.Domain.Common;
using Commerce.Domain.Products.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.Products.Commands
{
    public class UpdateProductCommandHandler: ICommandHandler<UpdateProduct>
    {
        private readonly IProductRepository _productRepository;
        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task ExecuteAsync(UpdateProduct command)
        {
            var product = await _productRepository.GetById(command.ProductId);

            product.Name = command.Name ?? product.Name;
            product.Description = command.Description ?? product.Description;
            product.UnitPrice = command.UnitPrice ?? product.UnitPrice;
            
            _productRepository.Save(product);
        }
    }
}
