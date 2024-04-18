using groceries_webshop.Data;
using groceries_webshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

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

        public ActionResult OnPostSearch(string? name, Category? category)
        {
            name = name == null ? "" : name;

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

        public void OnGet(string q, Category category)
        {

            if (q != "" && category != Category.All)
            {
                Products = _context.Products.Where(p => p.Name.Contains(q)).Where(p => p.Category.Equals(category)).ToList();
            }
            else if (q != "")
            {
                Products = _context.Products.Where(p => p.Name.Contains(q)).ToList();
            }
            else if (category != Category.All)
            {
                Products = _context.Products.Where(p => p.Category.Equals(category)).ToList();
            }
            else
            {
                Products = _context.Products.ToList();
            }

            // get products from _context
            Products = _context.Products.Take(9).ToList();
            PageNr = 1;
        }
    }
}