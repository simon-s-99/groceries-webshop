using groceries_webshop.Data;
using groceries_webshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace groceries_webshop.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext database;
        public List<Product> Products { get; set; } = new List<Product>();

        public IndexModel(AppDbContext database)
        {
            this.database = database;
        }

		public void OnGetPageNr(int pageNr)
		{

		}

		public void OnGet()
        {
			// get products from database
			Products = database.Products.ToList();
		}
    }
}