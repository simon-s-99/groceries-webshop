namespace groceries_webshop.Models
{
    public enum Category
    {
        Fruits,
        Vegetables,
        Nuts,
        Legumes,
        Condiments,
        Other
    }

    public class Product
    {
        public int ID { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("image")]
        public string? Image { get; set; }
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        [JsonPropertyName("category")]
        public Category Category { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }
}
