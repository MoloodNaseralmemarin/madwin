
using Shop2City.DataLayer.Entities.Common;
using Shop2City.DataLayer.Entities.Ordering;
using Shop2City.DataLayer.Entities.Products;
using Shop2City.DataLayer.Entities.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop2City.DataLayer.Entities.Orders
{
    [Table("Orders", Schema = "Orders")]
    public class OrderModel:BaseEntity
    {
        public int UserId { get; set; }

        [Display(Name = "گروه اصلی")]
        public int CategoryId { get; set; }

        public int SubCategoryId { get; set; }


        public int Width { get; set; }

        public int Height { get; set; }

        public int Count { get; set; }


         public decimal Price { get; set; }
        public decimal Cost { get; set; }


        public int TypePostId { get; set; }

        public decimal TotalCost { get; set; }

        public decimal DisCountCost { get; set; } = 0;


        /// <summary>
        /// وضعیت پرداخت
        /// </summary>
        public bool IsFinaly { get; set; }
        public User User { get; set; }

        #region Order
        [ForeignKey(nameof(CategoryId))]
        public ProductGroup OrderCategory { get; set; }

        [ForeignKey(nameof(SubCategoryId))]
        public ProductGroup OrderSubCategory { get; set; }
        #endregion


        public TypePost TypePost { get; set; }

        public virtual List<OrderDetailModel> OrderDetails { get; set; }


    }
}
