

using Shop2City.DataLayer.Entities.Products;

namespace Shop2City.Core.DTOs.ShoppingCart
{
    public class CartItemViewModel
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Count { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int Total => Quantity * Price;
        //public string? Image { get; set; }


        //اینجا منظورشو نفهمیدم که چی به چیه
        public CartItemViewModel()
        {
        }

        public CartItemViewModel(Product product)
        {
            ProductId = product.Id;
            ProductName = product.Title;
            Price = product.Price;
            Quantity = 1;
            //Image = product.ProductImages.OrderByDescending(pp => pp.productId).First().productImageName;
        }

    }
    //اینو متوجه نشدم
    public class SmallCartViewModel
    {
        public int NumberOfItems { get; set; }
        public decimal TotalAmount { get; set; }
    }

    //اینم متوجه نشدم
    public class CartViewModel
    {
        public List<CartItemViewModel>? CartItemViewModels { get; set; }
        public decimal GrandTotal { get; set; }
    }

    public class ShopCartitemTest
    {
        public int productId { get; set; }

        public int count { get; set; }

        public int price { get; set; }

    }

    //برای نمایش سبد خرید
    public class ShopCartTest
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public int Sum { get; set; }
        public string ImageName { get; set; }
    }

}

