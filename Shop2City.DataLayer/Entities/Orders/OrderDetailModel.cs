
using Shop2City.DataLayer.Entities.Common;

namespace Shop2City.DataLayer.Entities.Orders
{
    public class OrderDetailModel : BaseEntity
    {
        public int OrderId { get; set; }
        public decimal Cost { get; set; }
        public decimal TotalCost { get; set; }
        public OrderModel Order { get; set; }

    }
}
