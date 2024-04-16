using groceries_webshop.Data;
using groceries_webshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace groceries_webshop.Pages
{
    public class ShoppingCartModel : PageModel
    {
        private readonly AppDbContext _context;

        public ShoppingCartModel(AppDbContext context)
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

        public void ClearCart(int id)
        {
            _context.CartItems.RemoveRange(_context.CartItems.Where(c => c.AccountID == id));
            _context.SaveChanges();
        }

        public IActionResult OnPost(int id)
        {
            ClearCart(id);
            return RedirectToPage("/ShoppingCart");
        }

        public void OnGet(int id)
        {
            // Get logged in account
            Account = _context.Accounts.Find(id);

            // Get account's cart items
            CartItems = _context.CartItems.Where(c => c.AccountID == id).ToList();

            // Get products from cartitems
            foreach (CartItem cartItem in CartItems)
            {
                Product product = _context.Products.Where(p => p.ID == cartItem.ProductID).FirstOrDefault();
                Products.Add(product);
            }
        }
    }
}
