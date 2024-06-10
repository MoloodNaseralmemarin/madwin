using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Shop2City.Core.DTOs.ShopCart;
using Shop2City.DataLayer.Context;
using Shop2City.DataLayer.Entities.ShoppingCartItems;

namespace Shop2City.Core.Services.ShoppingCart
{
    public class ShoppingCartService : IShoppingCartService
    {
        
        private readonly Shop2CityContext _context;

        public ShoppingCartService( Shop2CityContext context)
        {
            
            _context = context;
        }

        public int AddShoppingCartItem(ShoppingCartItem shoppingCartItem)
        {

            _context.ShoppingCartItems.Add(shoppingCartItem);
            _context.SaveChanges();
            return shoppingCartItem.ShoppingCartItemId;

        }
    }
}
