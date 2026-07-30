using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class Category
    {
        [Key]
        [JsonIgnore]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

        [JsonIgnore]
        public List<Product> products { get; set; }

    }
}
