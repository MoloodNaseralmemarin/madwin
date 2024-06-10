using Shop2City.DataLayer.Entities.Products;
using Shop2City.DataLayer.Entities.Users;


namespace Shop2City.DataLayer.Entities.ShoppingCartItems
{
    public class ShoppingCartItem
    {
        #region ctor

        #endregion
        #region Field
        public int ShoppingCartItemId { get; set; }
        public DateTime RegisterDateTime { get; set; }=DateTime.Now;

        public int UserId { get; set; }


        public string? CookieId { get; set; }
        public string? Session { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        #endregion
        #region Relationship
        public Product? Product { get; set; }

        public User? User { get; set; }
        #endregion
    }
}
