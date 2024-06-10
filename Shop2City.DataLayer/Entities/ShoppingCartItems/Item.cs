using System;
using System.Collections.Generic;
using System.Text;
using Shop2City.DataLayer.Entities.Products;
using Shop2City.DataLayer.Entities.ShoppingCarts;

namespace Shop2City.DataLayer.Entities.ShoppingCarts
{
    public class Item
    {
        public  int Id { get; set; }

        public decimal price { get; set; }

        public int quantityInStock { get; set; }

        public Product product { get; set; }

    }
}
