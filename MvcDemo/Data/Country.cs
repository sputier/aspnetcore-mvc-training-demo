using System.ComponentModel.DataAnnotations;

namespace MvcDemo.Data
{
    public class Country
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
