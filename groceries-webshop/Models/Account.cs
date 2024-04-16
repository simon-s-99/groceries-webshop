namespace groceries_webshop.Models
{
    public class Account
    {
        public int ID { get; set; }
        public string OpenIDIssuer { get; set; }
        public string OpenIDSubject { get; set; }
        public string Name { get; set; }
        //public ICollection<Product>? ShoppingCart { get; } = new List<Product>(); // <-- not working

        // navigation property
        //public Product Product { get; set; }
    }
}
