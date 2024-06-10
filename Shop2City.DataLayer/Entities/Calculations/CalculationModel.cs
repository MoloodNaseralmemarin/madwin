using Shop2City.DataLayer.Entities.Common;
using Shop2City.DataLayer.Entities.Products;
using System.ComponentModel.DataAnnotations;


namespace Shop2City.DataLayer.Entities.Calculations
{
    public class CalculationModel:BaseEntity
    {
        [Display(Name="عنوان")]
        public string Name { get; set; }
        [Display(Name = "قیمت")]
        public int PurchasePrice { get; set; }


        public virtual List<CategorySelectedCalculationModel?> ProductSelectedCalculationModels { get; set; }


    }
}
