using groceries_webshop.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using groceries_webshop.Models;

namespace groceries_webshop.Controllers
{
    [Route("/api")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly AppDbContext _database;

        public APIController(AppDbContext database)
        {
            _database = database;
        }

		public enum Category
		{
			Fruits,
			Vegetables,
			Nuts,
			Legumes,
			Condiments,
			Other,
			Berries,
			Seeds
		}

		[HttpGet("/products")]
        public List<Product> GetProducts([FromQuery] string? name, [FromQuery] Category? category) 
        {

            List<Product> products = new List<Product>();

			// If both name and category are specified, filter first by name, then by category
			if (name != null && category != null)
			{
				products = _database.Products.Where(p => p.Name == name).Where(p => p.Category.Equals(category)).ToList();
			}
			else if (name != null)
			{
				products = _database.Products.Where(p => p.Name == name).ToList();
			}
			else if (category != null)
			{
				products = _database.Products.Where(p => p.Category.Equals(category)).ToList();
			}
			else
			{
				products = _database.Products.ToList();
			}

			return products;
        }
    }
}
