using groceries_webshop.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace groceries_webshop.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Product> Products { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
