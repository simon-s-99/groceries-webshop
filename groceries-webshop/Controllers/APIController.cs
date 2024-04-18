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

		public class ProductWithImage : Product
		{
			public string Image { get; set; }
		}

		[HttpGet("/products")]
		public List<ProductWithImage> GetProducts([FromQuery] string? name, [FromQuery] Category? category, [FromQuery] int page)
		{
			List<Product> productsWithoutImage = new List<Product>();
			List<ProductWithImage> products = new List<ProductWithImage>();

			// if page query is 0, return first ten posts
			page = (page < 1) ? 1 : page;

			const int resultsPerPage = 10;
			int postsToSkip = resultsPerPage * (page - 1);

			// If both name and category are specified, filter first by name, then by category
			if (name != null && category != null)
			{
				productsWithoutImage = _database.Products
					.Where(p => p.Name.Contains(name))
					.Where(p => p.Category
					.Equals(category))
					.Skip(postsToSkip)
					.Take(resultsPerPage)
					.ToList();
			}
			else if (name != null)
			{
				productsWithoutImage = _database.Products
                    .Where(p => p.Name.Contains(name))
                    .Skip(postsToSkip)
					.Take(resultsPerPage)
					.ToList();
			}
			else if (category != null)
			{
				productsWithoutImage = _database.Products
					.Where(p => p.Category.Equals(category))
					.Skip(postsToSkip)
					.Take(resultsPerPage)
					.ToList();
			}
			else
			{
				productsWithoutImage = _database.Products
					.Skip(postsToSkip)
					.Take(resultsPerPage)
					.ToList();
			}

			foreach (Product product in productsWithoutImage)
			{
				ProductWithImage newProduct = new ProductWithImage
				{
					ID = product.ID,
					Name = product.Name,
					Price = product.Price,
					Category = product.Category,
					Description = product.Description,
					Image = "https://localhost:5000/images/products/" +
						product.Name.ToLower().Replace(" ", "") + 
						".jpg"
				};

				products.Add(newProduct);
			}

			return products;
		}
	}
}
