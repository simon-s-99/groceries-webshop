//using groceries_webshop.Data;
//using groceries_webshop.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;

//namespace groceries_webshop.Pages
//{
//    public class OrderConfirmationModel : PageModel
//    {
//        private readonly AppDbContext _context;

//        public Account Account { get; set; }

//        public OrderConfirmationModel(AppDbContext context)
//        {
//            _context = context;
//        }

//        public void OnGet(int id)
//        {
//            Account = _context.Accounts.Find(id);
//        }
//    }
//}
