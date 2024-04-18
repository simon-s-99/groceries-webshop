using System.Text.Json.Serialization;

namespace groceries_webshop.Models
{
    public enum Category
    {
        Fruits,
        Vegetables,
        Nuts,
        Legumes,
        Condiments,
        Other,
        Berries,
        Seeds
    }

    public class Product
    {
        public int ID { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("category")]
        public Category Category { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }
}
