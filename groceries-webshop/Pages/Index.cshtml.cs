using groceries_webshop.Data;
using groceries_webshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace groceries_webshop.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        public List<Product> Products { get; set; } = new List<Product>();

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public ActionResult OnPost(int accountID, int productID)
        {
            _context.CartItems.Add(
                new CartItem
                {
                    AccountID = accountID,
                    ProductID = productID
                });

            _context.SaveChanges();
            return RedirectToPage();
        }

        public void OnGetPageNr(int pageNr)
        {

        }

        public void OnGet()
        {
            // get products from _context
            Products = _context.Products.ToList();
        }
    }
}