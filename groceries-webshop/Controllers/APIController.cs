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

		[HttpGet("/products")]
        public List<ProductWithImage> GetProducts([FromQuery] string? name, [FromQuery] Category? category) 
        {

			List<Product> productsWithoutImage = new List<Product>();
            List<ProductWithImage> products = new List<ProductWithImage>();

			// If both name and category are specified, filter first by name, then by category
			if (name != null && category != null)
			{
				productsWithoutImage = _database.Products.Where(p => p.Name == name).Where(p => p.Category.Equals(category)).ToList();
			}
			else if (name != null)
			{
				productsWithoutImage = _database.Products.Where(p => p.Name == name).ToList();
			}
			else if (category != null)
			{
				productsWithoutImage = _database.Products.Where(p => p.Category.Equals(category)).ToList();
			}
			else
			{
				productsWithoutImage = _database.Products.ToList();
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
					Image = "https://localhost:5001/images/products/" + product.Name.ToLower().Replace(" ", "") + ".jpg"
				};

				products.Add(newProduct);
			}

			return products;
        }
    }
}
