
using System.ComponentModel.DataAnnotations;
using Shop2City.Const;
using Shop2City.DataLayer.Entities.Products;

namespace Shop2City.DataLayer.Entities.Properties
{
    public class PropertyTechnicalProduct
    {
        #region ctor
        public PropertyTechnicalProduct()
        {

        }
        #endregion
        #region Field
        public int propertyTechnicalProductId { get; set; }
        public int productId { get; set; }
        public int propertyTechnicalId { get; set; }

        [Display(Name = "مقدار")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.Required)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLength)]
        [MinLength(3, ErrorMessage = ErrorMessage.MinLength)]
        public string propertyTechnicalProductValue { get; set; }
        #endregion
        #region Relationship
        public Product Product { get; set; }
        public PropertyTechnical PropertyTechnical { get; set; }
        #endregion
    }
}
