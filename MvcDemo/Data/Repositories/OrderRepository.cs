using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MvcDemo.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MvcDemoDbContext _context;

        public OrderRepository(MvcDemoDbContext context)
        {
            _context = context;

            // Force la création de la base. Utilisé ici uniquement 
            // parce que l'on travaille avec une In-Memory database
            // (avec un SGBD traditionnel, on utilise les migrations)
            _context.Database.EnsureCreated();

        }

        public IEnumerable<OrderSummary> GetOrderSummaries()
        {
            var orders =
                _context.OrderLines
                        .Include(ol => ol.OrderHeader)
                            .ThenInclude(oh => oh.Customer)
                        .GroupBy(ol => ol.OrderHeader.Id)
                        .Select(g => new OrderSummary
                        {
                            OrderId = g.Key,
                            Customer = g.First().OrderHeader.Customer.Name,
                            Total = g.First().OrderHeader.TotalAmount,
                            Products = g.Select(l => l.Product.Name)
                        }).ToList();

            return orders;
        }
    }
}
