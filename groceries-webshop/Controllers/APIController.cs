using groceries_webshop.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace groceries_webshop.Controllers
{
    [Route("/api")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly AppDbContext database;

        public APIController(AppDbContext database)
        {
            this.database = database;
        }

        [HttpGet("/products")]
        public IEnumerable<Models.Product> GetProducts([FromQuery] string? name, [FromQuery] string? category) 
        {

            IEnumerable<Models.Product> products = new List<Models.Product>();

            return products;
        }
    }
}
