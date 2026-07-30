using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class Product
    {
        [Key]
        [JsonIgnore]
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public string ProductDescription { get; set; }
        public double ProductPrice { get; set; }

        [ForeignKey("category")]
        public int CategoryId { get; set; }

        [JsonIgnore]
        public Category category { get; set; }
    }
}
