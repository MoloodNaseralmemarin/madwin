using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shop2City.Const;
using Shop2City.DataLayer.Entities.Properties;
using Shop2City.DataLayer.Entities.Users;
using Shop2City.DataLayer.Entities.Orders;
using Shop2City.DataLayer.Entities.Common;


namespace Shop2City.DataLayer.Entities.Products
{
    [Table("Products", Schema = "Production")]
    public class Product:BaseEntity
    {
        #region ctor
        public Product()
        {
            CreateDate=DateTime.Now;
            LastUpdateDate=DateTime.Now;
            IsDelete = false;
        }
        #endregion
        #region Field

        [Display(Name = "کاربر ثبت کننده")]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "دسته اصلی")]
        public int ProductGroupId { get; set; }

        [Display(Name = "گروه اصلی")]
        public int? CategoryId { get; set; }

        [Display(Name = "گروه فرعی")]
        public int? SubCategoryId { get; set; }

        [Display(Name = "عنوان ( حداقل سه کلمه)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.Required)]
        [MaxLength(450, ErrorMessage = ErrorMessage.MaxLength)]
        public string? Title { get; set; }


        [Display(Name = "توضیح مختصر")]
        public string? ShortDescription { get; set; }


        [Display(Name = "توضیح کامل")]
        public string? FullDescription { get; set; }

        [Display(Name = "قیمت")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.Required)]
        public int Price { get; set; }


        [Display(Name = "کلمه کلیدی")]
        public string? Tags { get; set; }

        public bool IsStatus { get; set; }

        #endregion
        #region Relationship

        [ForeignKey("ProductGroupId")]
        public ProductGroup? ProductGroup { get; set; }
        [ForeignKey("CategoryId")]
        public ProductGroup? Category { get; set; }
        [ForeignKey("SubCategoryId")]
        public ProductGroup? SubCategory { get; set; }
        public User User { get; set; }
        public List<ProductGallery> ProductGalleries { get; set; }
        public List<ProductProperty>? ProductProperties { get; set; }

        public List<PropertyTechnicalProduct>? PropertyTechnicalProducts { get; set; }

        public virtual List<ShoppingCarts.ShoppingCart>? ShoppingCart { get; set; }
        public virtual List<FactorDetail>? FactorDetails { get; set; }

        #endregion
    }
}

