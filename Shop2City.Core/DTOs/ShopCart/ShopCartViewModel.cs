using System.ComponentModel.DataAnnotations;
using Shop2City.DataLayer.Entities.Products;

namespace Shop2City.Core.DTOs.ShopCart
{
    public class ShopCartItemViewModel
    {
       
        [Display(Name="شناسه محصول")]
        public int productId { get; set; }

        public Product Product { get; set; }

        [Display(Name = "تعداد محصول")]
        public int quantity { get; set; }

        [Display(Name = "قیمت کل")]
        public int totalPrice { get; set; }

    }

    public class ShopCartShowViewModel
    {
        public int productId { get; set; }

        public string productImage { get; set; }

        public string productTitleFa { get; set; }

        public string productTitleEn { get; set; }

        public int productGuantity { get; set; }

        public decimal Price { get; set; }

        public decimal  productSum { get; set; }
    }

    public class ShopCartViewModel
    {
        [Display(Name = "تعداد محصول")]
        public int quantity { get; set; }

        [Display(Name = "قیمت کل")]
        public int totalPrice { get; set; }
    }

    public class ShopCartItem
    {
        public int productId { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
    }

    public class CartVm
    {
        public int Count { get; set; }

        public int TotalPrice { get; set; }
        public List<ProductInCartVM> ProductInCartVm { get; set; }
    }

    public class ProductInCartVM
    {
        public int productId { get; set; }
        public string productTitleFa { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
    }
}
