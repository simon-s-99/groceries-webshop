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

        public decimal GetTotal()
        {
            decimal total = 0;

            foreach (Product product in Account.ShoppingCart)
            {
                total += product.Price;
            }

            return total;
        }

        public void OnGet(int id)
        {
            Account = _context.Accounts.Find(id);
        }
    }
}
