using Commerce.Domain.Common;
using Commerce.Domain.Products.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.Products.Commands
{
    public class InsertProductCommandHandler: ICommandHandler<InsertProduct>
    {
        private readonly IProductRepository _productRepository;

        public InsertProductCommandHandler(IProductRepository productRepository)
        {
            ArgumentNullException.ThrowIfNull(productRepository, nameof(productRepository));
            _productRepository = productRepository;
        }

        public Task ExecuteAsync(InsertProduct command)
        {
            _productRepository.Save(new Product
            {
                Id = command.ProductId,
                Name = command.Name,
                Description = command.Description ?? string.Empty,
                UnitPrice = command.UnitPrice,
            });

            return Task.CompletedTask;
        }
    }
}
