using groceries_webshop.Data;
using groceries_webshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace groceries_webshop.Pages
{
	public class IndexModel : PageModel
	{
		private readonly AppDbContext _context;

		public IndexModel(AppDbContext context)
		{
			_context = context;
		}

		public List<Product> Products { get; set; } = new List<Product>();

		public int PageNr { get; set; }

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

		public void OnGetPageNr(bool nextPage, int pageNr)
		{
			if (pageNr == 1 && !nextPage)
			{
				return;
			}

			//if (Math.Round(Products.Count / 9) == pageNr && nextPage)
			//{ 
			
			//}

			if (nextPage)
			{
				Products = _context.Products
					.Skip((pageNr - 1) * 9)
					.Take(9)
					.ToList();
				PageNr = pageNr;
			}
			else
			{

			}
		}

		public void OnGet()
		{
			// get products from _context
			Products = _context.Products.Take(9).ToList();
			PageNr = 1;
		}
	}
}