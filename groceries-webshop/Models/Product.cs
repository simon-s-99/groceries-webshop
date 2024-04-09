namespace groceries_webshop.Models
{
    private enum Category
    {
        Fruits,
        Vegetables,
        Nuts,
        Legumes
    }

    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public string? Description { get; set; }
    }
}
