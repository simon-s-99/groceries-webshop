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
		private readonly AppDbContext _context;

		public APIController(AppDbContext context)
		{
			_context = context;
		}

		// sub-class only used for the api results
		public class ProductWithImage : Product
		{
			public string Image { get; set; }
		}

		[HttpGet("/products")]
		public List<ProductWithImage> GetProducts(
			[FromQuery] string? name,
			[FromQuery] string? categoryString,
			[FromQuery] int page)
		{
			List<Product> productsWithoutImage = new List<Product>();
			List<ProductWithImage> products = new List<ProductWithImage>();

			// dictionary to map the string from the query to a category,
			// so that the user of the api get images based on pre-defined categories
			Dictionary<string, Category> categoryMap = new Dictionary<string, Category>
			{
				{ "Fruits", Category.Fruits },
				{ "Vegetables", Category.Vegetables },
				{ "Nuts", Category.Nuts },
				{ "Legumes", Category.Legumes },
				{ "Condiments", Category.Condiments },
				{ "Other", Category.Other },
				{ "Berries", Category.Berries },
				{ "Seeds", Category.Seeds }
			};

			Category? category = null;
			if (categoryString != null)
			{
				// If dictionary contains value, set category to value in dictionary, prevents an exception
				if (categoryMap.TryGetValue(categoryString, out _))
				{
					category = categoryMap[categoryString];
				}

			}

			// if page query is 0, return first ten posts
			page = (page < 1) ? 1 : page;

			const int resultsPerPage = 10;
			int postsToSkip = resultsPerPage * (page - 1);

			// If both name and category are specified, filter first by name, then by category
			if (name != null && category != null)
			{
				productsWithoutImage = _context.Products
					.Where(p => p.Name.Contains(name))
					.Where(p => p.Category == category)
					.Skip(postsToSkip)
					.Take(resultsPerPage)
					.ToList();
			}
			// filter response based on name query (contains for broader search)
			else if (name != null)
			{
				productsWithoutImage = _context.Products
					.Where(p => p.Name.Contains(name))
					.Skip(postsToSkip)
					.Take(resultsPerPage)
					.ToList();
			}
			// filter by category (assigned based on categoryString)
			else if (category != null)
			{
				productsWithoutImage = _context.Products
					.Where(p => p.Category == category)
					.Skip(postsToSkip)
					.Take(resultsPerPage)
					.ToList();
			}
			else
			{
				productsWithoutImage = _context.Products
					.Skip(postsToSkip)
					.Take(resultsPerPage)
					.ToList();
			}

			foreach (Product product in productsWithoutImage)
			{
				// append image url to products returned by api
				ProductWithImage newProduct = new ProductWithImage
				{
					ID = product.ID,
					Name = product.Name,
					Price = product.Price,
					Category = product.Category,
					Description = product.Description,
					Image = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/images/products/" +
						product.Name.ToLower().Replace(" ", "") +
						".jpg"
				};

				products.Add(newProduct);
			}

			return products;
		}
	}
}
