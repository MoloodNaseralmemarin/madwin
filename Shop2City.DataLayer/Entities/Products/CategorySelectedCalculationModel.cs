

using Shop2City.DataLayer.Entities.Calculations;
using Shop2City.DataLayer.Entities.Common;
using Shop2City.DataLayer.Entities.Orders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop2City.DataLayer.Entities.Products
{
    public class CategorySelectedCalculationModel : BaseEntity
    {
        #region Properties

        public int CategoryId { get; set; }

        public int SubCategoryId { get; set; }

        public int CalculationId { get; set; }

        #endregion

        #region Relations

        public CalculationModel Calculation { get; set; }

        #endregion
    }
}
