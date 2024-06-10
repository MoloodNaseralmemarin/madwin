
using Shop2City.DataLayer.Entities.Common;
using Shop2City.DataLayer.Entities.Products;

namespace Shop2City.DataLayer.Entities.Orders
{
    public class CalculationDetailModel : BaseEntity
    {
        public int OrderId { get; set; }

        public int ProductSelectedCalculationId { get; set; }
        public decimal Cost { get; set; }
        public decimal TotalCost { get; set; }


        public OrderModel Order { get; set; }

        public CategorySelectedCalculationModel ProductSelectedCalculation { get; set; }
    }
}
