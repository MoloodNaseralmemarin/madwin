using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shop2City.Const;

namespace Shop2City.DataLayer.Entities.Products
{
    [Table("ProductGalleries", Schema = "Production")]
    public class ProductGallery
    {
        [Key]
        [Display(Name = "شناسه")]
        public int ProductGalleryId { get; set; }

        public int ProductId { get; set; }

        [Display(Name = "تصویر محصول")]
        [Required(ErrorMessage = ErrorMessage.Required)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLength)]
        public string FileName { get; set; }

        [Display(Name = "عنوان عکس")]
        [Required(ErrorMessage = ErrorMessage.Required)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLength)]
        public string Title { get; set; }


        [Display(Name = "متن جایگزین")]
        [Required(ErrorMessage = ErrorMessage.Required)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLength)]
        public string Alt { get; set; }

        public bool isDelete { get; set; }


        #region Relationship
        public Product Product { get; set; }
        #endregion
    }
}
