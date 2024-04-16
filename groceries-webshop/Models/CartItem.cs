using System.ComponentModel.DataAnnotations.Schema;

namespace groceries_webshop.Models
{
    public class CartItem
    {
        /*
            CartItem is a table meant to store individual items in shoppingcarts.
            The items are associated to their respective shopping-carts (Accounts)
            through the AccountID column. 
         */

        public int ID { get; set; }

        [ForeignKey("AccountID")]
        public int AccountID { get; set; }
        public Account Account { get; set; }

        [ForeignKey("ProductID")]
        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}
