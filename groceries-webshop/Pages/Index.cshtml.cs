using groceries_webshop.Data;
using groceries_webshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Xml.Linq;

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

        // dictionary to map the string from the query to a category
        Dictionary<string, Category> categoryMap = new Dictionary<string, Category>
        {
            { "fruits", Category.Fruits },
            { "vegetables", Category.Vegetables },
            { "nuts", Category.Nuts },
            { "legumes", Category.Legumes },
            { "condiments", Category.Condiments },
            { "other", Category.Other },
            { "berries", Category.Berries },
            { "seeds", Category.Seeds }
        };

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

        public ActionResult OnPostSearch(string? q, string categoryString)
        {
            q = string.IsNullOrEmpty(q) ? "" : q;

            Category category = (Category)(-1); // THIS SHOULD NOT WORK, ERROR INBOUND
            if (categoryString != "all")
            {
                if (categoryMap.TryGetValue(categoryString, out _))
                {
                    category = categoryMap[categoryString];
                }
            }

            if (q != "" && (int)category >= 0)
            {
                return RedirectToPage("/Index", new { q, category });
            }
            else if (q != "")
            {
                return RedirectToPage("/Index", new { q });
            }
            else if (category != "all")
            {
                // mapå herer
                return RedirectToPage("/Index", new { category });
            }
            else
            {
                return RedirectToPage("/Index");
            }
        }

        public void OnGet(int pageNr, string? q, Category? category)
        {
            if (q == null && category == null)
            {
                // get products to display from _context 
                if (pageNr < 1) { pageNr = 1; }
                Products = _context.Products
                    .Skip(9 * (pageNr - 1))
                    .Take(9)
                    .ToList();
                PageNr = pageNr;

                return;
            }

            if (q != "" && category != "all")
            {
                Products = _context.Products
                    .Where(p => p.Name.Contains(q))
                    .Where(p => p.Category.Equals(category))
                    .ToList();
            }
            else if (q != "")
            {
                Products = _context.Products
                    .Where(p => p.Name.Contains(q))
                    .Take(9)
                    .ToList();
            }
            else if (category != "all")
            {
                Products = _context.Products
                    .Where(p => p.Category.Equals(category))
                    .ToList();
            }
        }
    }
}