using Shop2City.Core.DTOs.Orders;
using Shop2City.DataLayer.Entities.Ordering;
using Shop2City.DataLayer.Entities.Orders;

namespace Shop2City.Core.Services
{
    public interface IOrderRepository : IDisposable
    {
        Task<List<ShowOrderViewModel1>> GetAll();

        Task AddProductToOrder(OrderModel orderModel);

        Task AddOrderDetail(OrderDetailModel orderDetailModel);
        Task AddCalculationDetail(CalculationDetailModel calculationDetailModel);

        Task UpdateOrderDetail(OrderDetailModel orderDetailModel);
        Task<List<OrderDetailModel>> GetOrderDetails();

        Task<List<CalculationDetailModel>> GetCalculationDetails();

        Task<List<OrderDetailViewModel>> GetOrderDetailsByOrderId(int orderId);
        

    }
}
