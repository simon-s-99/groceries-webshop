using groceries_webshop.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace groceries_webshop.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
