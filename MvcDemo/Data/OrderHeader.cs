using System;
using System.ComponentModel.DataAnnotations;

namespace MvcDemo.Data
{
    public class OrderHeader
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }

        public Customer Customer { get; set; }
    }
}
