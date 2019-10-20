using System.Collections.Generic;

namespace MvcDemo.Data.Repositories
{
    public class OrderSummary
    {
        public int OrderId { get; set; }

        public string Customer { get; set; }

        public decimal Total { get; set; }

        public IEnumerable<string> Products { get; set; }
    }
}
