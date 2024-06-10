using System.ComponentModel.DataAnnotations;
using Shop2City.DataLayer.Entities.Products;
using Shop2City.DataLayer.Entities.Users;

namespace Shop2City.DataLayer.Entities.ShoppingCarts
{
    public class ShoppingCart
    {
        [Key]
        public int shoppingCartId { get; set; }
        public int? shoppingCartgroupId { get; set; }
        public int userId { get; set; }
        public string? cookieId { get; set; }
        public int productId { get; set; }
        [Display(Name = "تعداد")]
        public int count { get; set; }
        [Display(Name = "تاریخ")]
        public DateTime registerDateTime { get; set; }

        public User User { get; set; }
        public  Product Product { get; set; }
        

    }
}
