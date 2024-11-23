using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shop2City.DataLayer.Entities.Products;

namespace Shop2City.DataLayer.Entities.Orders
{
    [Table("FactorDetails", Schema = "Factors")]
    public class FactorDetail
    {
        public FactorDetail()
        {  
        }

        [Key]
        [Display(Name = "شناسه")]
        public int Id { get; set; }

        [Required]
        public int FactorId { get; set; }

        [Required]
        public int ProductId { get; set; }

        /// <summary>
        /// تعداد
        /// </summary>
        [Required]
        public int Quantity { get; set; }

        /// <summary>
        /// آخرین قیمت                                                                                                                                                                                                                                      
        /// </summary>
        [Required]
        public int Price { get; set; }


        #region Relationship
        public Factor Factor { get; set; }
        public Product Product { get; set; }
        #endregion
    }
}
