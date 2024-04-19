using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using groceries_webshop.Models;
using groceries_webshop.Data;

namespace groceries_webshop.Pages
{
    public class ProductDetailsModel : PageModel
    {
        private readonly AppDbContext _context;

        public ProductDetailsModel(AppDbContext context)
        {
            _context = context;
        }

        public Product SelectedProduct { get; set; }

        public ActionResult OnPost(int accountID, int productID)
        {
            _context.CartItems.Add(
                new CartItem
                {
                    AccountID = accountID,
                    ProductID = productID
                });

            _context.SaveChanges();

            // Id is required to prevent an exception
            return RedirectToPage("/ProductDetails", new {id = productID});
        }
        public void OnGet(int id)
        {
            SelectedProduct = _context.Products.Find(id);
        }
    }
}
