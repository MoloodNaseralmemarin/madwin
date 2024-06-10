using Shop2City.DataLayer.Entities.ShoppingCartItems;


namespace Shop2City.Core.Services.ShoppingCart
{
    public interface IShoppingCartService
    {
        int AddShoppingCartItem(ShoppingCartItem shoppingCartItem);

    }
}
