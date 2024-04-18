using groceries_webshop.Data;
using groceries_webshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

		public ActionResult OnPostPageNr(bool nextPage, int pageNr, int? lastDisplayedProduct)
		{
			if (nextPage)
			{
				// handle user being on last page and clicking on next button
				int lastProduct = _context.Products
					.OrderByDescending(p => p.ID)
					.First()
					.ID;
				if (lastDisplayedProduct == lastProduct ||
					lastDisplayedProduct == 0)
				{
					return RedirectToPage("/Index",
						new { pageNr });
				}
				
				// normal handling for next page (user is not on last page)
				pageNr += 1;
				return RedirectToPage("/Index",
					new { pageNr });
			}
			else
			{
				pageNr = (pageNr == 1) ? 1 : pageNr - 1;
				return RedirectToPage("/Index",
					new { pageNr });
			}
		}

        public ActionResult OnPostSearch(string? name, Category? category)
        {
            name = string.IsNullOrEmpty(name) ? "" : name;

            return RedirectToPage("/Index", new { q = name, category});
            //if (name != null && category != Category.All)
            //{
            //    Products = _context.Products.Where(p => p.Name.Contains(name)).Where(p => p.Category.Equals(category)).ToList();
            //    return RedirectToPage("/Index", new { name, category });
            //}
            //else if (name != null)
            //{
            //    return RedirectToPage("/Index", new { name });
            //}
            //else if (category != Category.All)
            //{
            //    Products = _context.Products.Where(p => p.Category.Equals(category)).ToList();
            //    return RedirectToPage("/Index", new { category });

            //}
            //else
            //{
            //    Products = _context.Products.ToList();
            //    return RedirectToPage("/Index");
            //}
        }

		public void OnGet(int pageNr)
		{
            //if (q != "" && category != Category.All)
            //{
            //    Products = _context.Products.Where(p => p.Name.Contains(q)).Where(p => p.Category.Equals(category)).ToList();
            //}
            //else if (q != "")
            //{
            //    Products = _context.Products.Where(p => p.Name.Contains(q)).ToList();
            //}
            //else if (category != Category.All)
            //{
            //    Products = _context.Products.Where(p => p.Category.Equals(category)).ToList();
            //}
            //else
            //{
            //    Products = _context.Products.ToList();
            //}

            // get products to display from _context 
            if (pageNr < 1) { pageNr = 1; }
            Products = _context.Products
                .Skip(9 * (pageNr - 1))
                .Take(9)
                .ToList();
            PageNr = pageNr;
        }
	}
}