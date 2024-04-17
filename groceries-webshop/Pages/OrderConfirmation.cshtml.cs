using groceries_webshop.Data;
using groceries_webshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace groceries_webshop.Pages
{
    public class OrderConfirmationModel : PageModel
    {
        private readonly AppDbContext _context;

        public OrderConfirmationModel(AppDbContext context)
        {
            _context = context;
        }

        public Account Account { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public List<Product> Products { get; set; } = new List<Product>();

        public decimal GetTotal()
        {
            decimal total = 0;

            foreach (Product product in Products)
            {
                total += product.Price;
            }

            return total;
        }

        public void OnGet(int id)
        {
            // Get logged in account
            Account = _context.Accounts.Find(id);

            // Get account's cart items
            CartItems = _context.CartItems.Where(c => c.AccountID == id).ToList();

            // Get products from cart items
            foreach (CartItem cartItem in CartItems)
            {
                Product product = _context.Products.Where(p => p.ID == cartItem.ProductID).FirstOrDefault();
                Products.Add(product);
            }
        }
    }
}
