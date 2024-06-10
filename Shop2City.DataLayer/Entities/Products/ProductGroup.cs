using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shop2City.Const;
using Shop2City.DataLayer.Entities.Common;
using Shop2City.DataLayer.Entities.Ordering;
using Shop2City.DataLayer.Entities.Orders;

namespace Shop2City.DataLayer.Entities.Products
{
    public class ProductGroup :BaseEntity
    {
        #region ctor
        public ProductGroup()
        {

        }
        #endregion
        #region Field

        [Display(Name = "گروه اصلی")]
        public int? ParentId { get; set; }

        [Display(Name = "عنوان ")]
        [Required(AllowEmptyStrings =false, ErrorMessage = ErrorMessage.Required)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLength)]
        public string Title { get; set; }

        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; }
        #endregion
        #region Relationship

        [ForeignKey("parentId")]
        public List<ProductGroup> ProductGroups { get; set; } 


        [InverseProperty("ProductGroup")]
        public List<Product> Products { get; set; }


        [InverseProperty("Category")]
        public List<Product> Category { get; set; }


        [InverseProperty("SubCategory")]
        public List<Product> SubCategory { get; set; }


        #region order
        [InverseProperty("OrderCategory")]
        public List<OrderModel> OrderCategory { get; set; }

        [InverseProperty("OrderSubCategory")]
        public List<OrderModel> OrderSubCategory { get; set; }
        #endregion

        #endregion
    }
}
