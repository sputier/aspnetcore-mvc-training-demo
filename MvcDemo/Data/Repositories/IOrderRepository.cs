using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MvcDemo.Data.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<OrderSummary> GetOrderSummaries();
    }
}
