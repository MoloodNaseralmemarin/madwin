
using Microsoft.EntityFrameworkCore;
using Shop2City.Core.DTOs.Orders;
using Shop2City.DataLayer.Entities.Ordering;
using Shop2City.DataLayer.Entities.Orders;


namespace Shop2City.Core.Services
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IGenericRepository<OrderModel> _orderRepository;
        private readonly IGenericRepository<OrderDetailModel> _orderDetailRepository;
        private readonly IGenericRepository<CalculationDetailModel> _calculationDetailRepository;
        public OrderRepository(IGenericRepository<OrderModel> orderRepository, IGenericRepository<OrderDetailModel> orderDetailRepository, IGenericRepository<CalculationDetailModel> calculationDetailRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _calculationDetailRepository = calculationDetailRepository;
        }

        public async Task<List<ShowOrderViewModel1>> GetAll()
        {
            return await _orderRepository.GetEntitiesQuery()
                .OrderByDescending(o=>o.Id)
                .Select(o=>new ShowOrderViewModel1
                {
                   Cost = o.Cost,
                   Count = o.Count,
                   Height = o.Height,
                   TotalCost = o.TotalCost,
                   Width=o.Width,
                })
                .ToListAsync();
        }

        public async Task AddProductToOrder(OrderModel orderModel)
        {
            orderModel.IsFinaly = false;
            await _orderRepository.AddEntity(orderModel);
            await _orderRepository.SaveChanges();

        }

        public async Task AddOrderDetail(OrderDetailModel orderDetailModel)
        {
            await _orderDetailRepository.AddEntity(orderDetailModel);
            await _orderDetailRepository.SaveChanges();
        }
        public async Task AddCalculationDetail(CalculationDetailModel calculationDetailModel)
        {
            await _calculationDetailRepository.AddEntity(calculationDetailModel);
            await _calculationDetailRepository.SaveChanges();
        }

        public async Task<List<OrderDetailModel>> GetOrderDetails()
        {
            return await _orderDetailRepository.GetEntitiesQuery()
                .Where(o=>o.OrderId==0)
                .ToListAsync();
        }

        public async Task<List<CalculationDetailModel>> GetCalculationDetails()
        {
            return await _calculationDetailRepository.GetEntitiesQuery()
                .Where(o => o.OrderId == 0)
                .ToListAsync();
        }
        
        public async Task UpdateOrderDetail(OrderDetailModel orderDetailModel)
        {
           _orderDetailRepository.UpdateEntity(orderDetailModel);
            await _orderDetailRepository.SaveChanges();
        }

        public async Task<List<OrderDetailViewModel>> GetOrderDetailsByOrderId(int orderId)
        {
            //            return await _orderDetailRepository.GetEntitiesQuery().
            //            Where(od => od.OrderId == orderId)
            //.OrderBy(od => od.Id)
            //.ThenBy(od => od.ProductSelectedCalculation.Calculation.Id)
            //.ThenBy(od => od.Cost)
            //.Select(o => new OrderDetailViewModel
            //{
            //    Id = o.Id,
            //    OrderId = orderId,
            //    CalculationId = o.ProductSelectedCalculation.Calculation.Id,
            //    CreateDate = o.CreateDate,
            //    CalculationTitle = o.ProductSelectedCalculation.Calculation.Name,
            //    Cost = o.Cost,
            //    TotalCost = o.TotalCost,
            //})
            //.ToListAsync();
            //        }
            //    }


            var query= await _orderDetailRepository.GetEntitiesQuery()
                .Where(o=>o.OrderId==orderId)
                .OrderBy(p => p.Id)
                .Select(o => new OrderDetailViewModel
                {
                    Id = o.Id,
                    orderId = orderId,
                   // CalculationId =o.ProductSelectedCalculationId ,
                   // CalculationTitle =o.ProductSelectedCalculation.Calculation.Name,
                    
                    Cost=o.Cost,
                    TotalCost=o.TotalCost,
                      })
                     .ToListAsync();

            return query;

                }
    public void Dispose()
        {
            _orderRepository.Dispose();
        }
    }
}
