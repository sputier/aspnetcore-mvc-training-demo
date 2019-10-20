using System.ComponentModel.DataAnnotations;

namespace MvcDemo.Data
{
    public class OrderLine
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        public OrderHeader OrderHeader { get; set; }
        public Product Product { get; set; }
    }
}
