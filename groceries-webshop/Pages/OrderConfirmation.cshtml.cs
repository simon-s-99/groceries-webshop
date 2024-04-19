using groceries_webshop.Data;
using groceries_webshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace groceries_webshop.Pages
{
    public class OrderConfirmationModel : PageModel
    {

        // Get the temp data from the checkout method in shopping cart
        [TempData]
        public string Total {  get; set; }


        public void OnGet()
        {

        }
    }
}
