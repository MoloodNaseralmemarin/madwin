using System.ComponentModel.DataAnnotations;

namespace Shop2City.DataLayer.Entities.Properties
{
   public class PropertyTechnical
    {
        #region ctor
        public PropertyTechnical()
        {

        }
        #endregion
        #region Field
        [Key]
        public int propertyTechnicalId { get; set; }
        public string propertyTechnicalTitle { get; set; }
        #endregion
        #region Relationship
        public  List<PropertyTechnicalProduct> PropertyTechnicalProducts { get; set; }
        #endregion
    }
}
