using groceries_webshop.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace groceries_webshop.Data
{
    public class SampleData
    {
        public static void Create(AppDbContext database)
        {
            // If there are no fake accounts, add some.
            string fakeIssuer = "https://example.com";
            if (!database.Accounts.Any(a => a.OpenIDIssuer == fakeIssuer))
            {
                database.Accounts.Add(new Account
                {
                    OpenIDIssuer = fakeIssuer,
                    OpenIDSubject = "1111111111",
                    Name = "Brad"
                });
                database.Accounts.Add(new Account
                {
                    OpenIDIssuer = fakeIssuer,
                    OpenIDSubject = "2222222222",
                    Name = "Angelina"
                });
                database.Accounts.Add(new Account
                {
                    OpenIDIssuer = fakeIssuer,
                    OpenIDSubject = "3333333333",
                    Name = "Will"
                });
            }

            // If no products are found, add sample products.
            if (!database.Products.Any())
            {
                string filePath = "./Data/product-data.json";
                string productData = File.ReadAllText(filePath);
                Product[] products = JsonSerializer.Deserialize<Product[]>(productData);

                for (int i = 0; i < products.Length; i++)
                {
                    database.Products.Add(products[i]);
                }

                database.SaveChanges();
            }

            database.SaveChanges();
        }
    }
}
