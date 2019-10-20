using System.ComponentModel.DataAnnotations;

namespace MvcDemo.Data
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }

        public Country Country { get; set; }
    }
}
