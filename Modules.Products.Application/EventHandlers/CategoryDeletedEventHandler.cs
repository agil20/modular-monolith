using Common.Events;
using MediatR;
using Modules.Products.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
namespace Modules.Products.EventHandlers
{
    public class CategoryDeletedEventHandler : INotificationHandler<CategoryDeleteEvent>
    {
        private readonly ProductsDbContext _context;

        public CategoryDeletedEventHandler(ProductsDbContext context)
        {
            _context = context;
        }

        public async Task Handle(CategoryDeleteEvent notification, CancellationToken cancellationToken)
        {
            var products = await _context.Products
                 .Where(p => p.CategoryId == notification.CategoryId)
                 .ToListAsync(cancellationToken);
            if (products.Any())
            {
                foreach (var product in products)
                {
                    _context.Products.Remove(product);

                }



            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
