using groceries_webshop.Data;
using groceries_webshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace groceries_webshop.Pages
{
    public class ListProductsPageModel : PageModel
    {
        private readonly AppDbContext database;
        public List<Product> Products;

        public ListProductsPageModel(AppDbContext database)
        {
            this.database = database;
        }

        public void OnGet()
        {
            // get products from database
            Products = database.Products.ToList();
        }
    }
}
