﻿using Shop2City.Core.DTOs.Orders;
using Shop2City.DataLayer.Entities.DisCounts;
using Shop2City.DataLayer.Entities.Orders;

namespace Shop2City.Core.Services.Orders
{
    public interface IOrderService
    {
        //used
        List<OrderModel> GetAllOrder();
        List<OrderModel> GetUserOrdersByUserName(string userName);

        //useed
        List<OrderDetailModel> GetAllOrderDetailByOrderId();


        OrderModel GetOrderForUserPanel(string userName, int orderId);

        int AddOrder(string userName, int productId);

        void UpdatePriceOrder(int orderId);
        OrderModel GetOrderForUserPanel(string userName);

        bool FinallyOrder(string userName,int orderId);


        List<TypePost> TypePosts();

        int GetPricePost(int typePostId);
        void UpdateOrder(OrderModel order);

        OrderModel GetOrderByUserId(int userId);

        OrderModel GetOrderByOrderId(int orderId);

        int GetOrderIdByUserId(int userId);

        void DeleteOrderItem(int productId);

        int CountOrders();

        List<CalculationDetailModel> GetAllCalculationDetailModelByOrderId(int orderId);


        OrderModel GetUserOrders(string userName, bool isfinaly);

  




        void DeleteOrder(int userId);

        void DeleteOrderDetail(int userId);


        int GetPricePostByTypePostId(int typePostId);

        int GetCountOrderIsFinaly(string userName);

        #region DisCount

        DiscountUseType UseDiscount(int orderId, string code);

        #endregion


    }
}

       