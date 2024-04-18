﻿using groceries_webshop.Data;
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

		public void OnGet(int pageNr)
		{
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