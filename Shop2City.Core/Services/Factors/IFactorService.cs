﻿using Shop2City.DataLayer.Entities.Orders;

namespace Shop2City.Core.Services.Factors
{
    public interface IFactorService
    {
       

        int AddFactor(string userName, int productId,int Count);

        Factor GetFactorForUserPanel(string userName, int factorId);
        Factor GetUserFactors(string userName, bool isfinaly);

        List<FactorDetail> GetAllFactorDetailByUserId(int userId);
        List<Factor> GetAllFactorByUserId(int userId);


        List<FactorDetail> GetAllFactorDetailByFactorId(int factorId);


        bool FinallyOrder(string userName,int orderId);



        List<Factor> GetAllOrder(int factorId = 0);

        

        Task<FactorDetail?> GetFactorDetailByProductId(int productId,int userId);

        List<TypePost> TypePosts();

        int GetPricePostByTypePostId(int typePostId);
        void UpdateFactor(Factor order);

        Factor GetFactorByFactorId(int factorId);

        int GetFactorIdByUserId(int userId);

        void DeleteFactorItem(int productId,int userId);

        void DeleteFactor(int userId);

        void DeleteFactorDetail(int userId);

        int CountFactors();

        int GetCountFactorIsFinaly(string userName);

        #region TypePost
        int GetTypePostPriceByTypePostId(int TypePostId);
        #endregion




    }
}

       