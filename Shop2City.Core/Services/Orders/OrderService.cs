
using Microsoft.EntityFrameworkCore;
using Shop2City.Core.DTOs.Orders;
using Shop2City.Core.Services.Products;
using Shop2City.Core.Services.Users;
using Shop2City.DataLayer.Context;
using Shop2City.DataLayer.Entities.DisCounts;
using Shop2City.DataLayer.Entities.Orders;
using Shop2City.DataLayer.Entities.Users;

namespace Shop2City.Core.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly Shop2CityContext _context;
        private readonly IUserService _userService;
        private readonly IProductService _productService;

        public OrderService(Shop2CityContext context, IUserService userService, IProductService productService)
        {
            _context = context;
            _userService = userService;
            _productService = productService;
        }
        public async Task<OrderModel> GetOrderForUserPanel(int userId, int orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderCategory)
                .Include(o => o.OrderSubCategory)
                .Include(f => f.OrderDetails)
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.UserId == userId && o.Id == orderId && !o.IsFinaly);
        }

        public int AddOrder(string userName, int productId)
        {
            var userId = _userService.GetUserIdByUserName(userName);
            //اول چک کنیم ببینیم سفارش باز داره یعنی پرداخت نهایی نکرده است
            //اگر فاکتور باز داشت باید به اون فاکتور اضافه بشه
            //اگر فاکتور باز داشت باید به اون فاکتور اضافه بشه

            var order = _context.Factors
                .FirstOrDefault(o => o.UserId == userId && !o.IsFinaly);

            var product = _productService.GetProductByProductId(productId);

            //اگر سفارش خالی بود
            if (order == null)
            {
                //یعنی سفارش بازی ندازه و ثبت سفارش نکرده و باید سفارش جدید ثبت کنید
                order = new Factor()
                {
                    UserId = userId,
                    IsFinaly = false,
                    CreatedDate = DateTime.Now,
                    FactorSum = product.Price,
                    

                };
                _context.Factors.Add(order);
                _context.SaveChanges();
                _context.FactorDetails.Add(new FactorDetail
                {
                    FactorId = order.Id,
                    ProductId = productId,
                    Quantity = 1,
                    Price = product.Price,
                });
                _context.SaveChanges();
            }
            else
            {
                //کاربر فاکتور باز داره
                //الان باید چک کنیم از کالای انتخاب شده توی فاکتورش هست یا نه 

                FactorDetail detail = _context.FactorDetails
                    .SingleOrDefault(d => d.FactorId == order.Id && d.FactorId == productId);
                // اگر همچنین کالایی وجود نداشت
                if (detail == null)
                {
                    detail = new FactorDetail()
                    {
                        FactorId = order.Id,
                        ProductId = productId,
                        Quantity = 1,
                        Price = product.Price,
                        //price = _context.Products.Find(productId).Price
                    };
                    _context.FactorDetails.Add(detail);

                }
                //اینجا محصول وجود نداشت توی فاکتور باز
                else
                {
                    //یدونه به تعدادش اضافه کن
                    detail.Quantity += 1;
                    _context.Update(detail);
                }
                _context.SaveChanges();
                UpdatePriceOrder(order.Id);
            }


            return order.Id;

        }
        #region آپدیت قیمت سفارش
        public void UpdatePriceOrder(int orderId)
        {
            var order = GetOrderByOrderId(orderId);
            order.TotalCost = _context.FactorDetails
                .Where(d => d.FactorId == orderId)
                .Sum(d => d.Price);
            _context.Orders.Update(order);
            _context.SaveChanges();
        }
        #endregion

        public bool FinallyOrder(string userName, int orderId)
        {
            int userId = _userService.GetUserIdByUserName(userName);
            var order = _context.Factors.Include(o => o.FactorDetails).ThenInclude(od => od.Product)
                .FirstOrDefault(o => o.Id == orderId && o.UserId == userId);

            if (order == null || order.IsFinaly)
            {
                return false;
            }
            return false;
        }
        //uesed
        public List<OrderModel> GetAllOrder()
        {
            IQueryable<OrderModel> result = _context.Orders
                .Include(u=>u.OrderCategory)
                .Include(u=>u.OrderSubCategory)
                .Include(u => u.User);
            return result.ToList();
        }
        //uesed
        //changed
        public List<OrderDetailModel> GetAllOrderDetailByOrderId()
        {
            return _context.OrderDetails
                .Include(od => od.Order)
                .ThenInclude(od => od.User)
                .ToList();

        }
        public List<CalculationDetailModel> GetAllCalculationDetailModelByOrderId(int orderId)
        {
            return _context.CalculationDetails
                .Include(od => od.Order)
                .ThenInclude(od => od.OrderDetails)
                .Include(od => od.ProductSelectedCalculation)
                .ThenInclude(od => od.Calculation)
                .Where(od => od.OrderId == orderId)
                .ToList();

        }
        List<OrderDetailModel> GetAllOrderDetailByCategory(int categoryId, int subCategoryId)
        {
            return _context.OrderDetails

                //  .ThenInclude(od => od.User)
                //.Where(od => od.cate == productId)

                .ToList();
        }

        public OrderModel GetOrderForUserPanel(string userName)
        {
            int userId = _userService.GetUserIdByUserName(userName);
            return _context.Orders
                .Include(od => od.OrderDetails)
                .FirstOrDefault(o => o.UserId == userId && !o.IsFinaly);
        }

        public int GetPricePost(int typePostId)
        {
            return _context.TypePosts
                .Single(u => u.TypePostId == typePostId).pricePost;

        }

        public List<TypePost> TypePosts()
        {
            return _context.TypePosts.
                ToList();
        }








        public int GetOrderIdByUserId(int userId)
        {
            return _context.Orders
                .OrderBy(o => o.Id)
                .LastOrDefault(u => u.UserId == userId).Id;
        }







        public void UpdateOrder(OrderModel order)
        {
            _context.Orders.Update(order);
           _context.SaveChanges();
        }

        public OrderModel GetOrderByUserId(int userId)
        {
            return _context.Orders
                .Include(o => o.OrderCategory)
                .Include(o => o.OrderSubCategory)
                .Include(o => o.User)
                .FirstOrDefault(o => o.UserId == userId
                && !o.IsFinaly);
        }
        public OrderModel GetOrderByOrderId(int orderId)
        {
            return _context.Orders
                .Include(o => o.OrderCategory)
                .Include(o => o.OrderSubCategory)
                .Include(o => o.User)
                .FirstOrDefault(o => o.Id == orderId);
        }



        public void DeleteOrderItem(int orderId)
        {
            var order = GetOrderByOrderId(orderId);
            _context.Orders.RemoveRange(order);
            _context.SaveChanges();
        }

        public int GetTypePostPriceByTypePostId(int TypePostId)
        {
            return _context.TypePosts.Find(TypePostId).pricePost;
        }

        public int CountOrders()
        {
            return _context.Orders.Count();
        }

        public OrderModel GetUserOrders(string userName, bool isfinaly)
        {
            int userId = _userService.GetUserIdByUserName(userName);
            return _context.Orders

              .Include(o => o.OrderDetails)
              .Include(o => o.OrderCategory)
              .Include(o => o.OrderSubCategory)
               .Include(f => f.User)

              .FirstOrDefault(o => o.UserId == userId && o.IsFinaly == isfinaly);
        }


        public async Task<int> GetCountOrderIsFinaly(int userId)
        {
            var count = await _context.Orders
                .Where(f => f.IsFinaly && f.UserId == userId)
                .CountAsync();

            return count > 0 ? count : 0;
        }


        public int GetCountOrderIsNotFinaly(string userName)
        {
            var userId = _userService.GetUserIdByUserName(userName);
            var count = _context.Orders
                .Where(f => !f.IsFinaly && f.UserId == userId)
                .Count();
            if (count > 0) return count;
            return 0;
        }

        public List<OrderDetailModel> GetAllOrderDetailByUserId(int userId)
        {
            return _context.OrderDetails
                .Include(od => od.Order)
                .ThenInclude(od => od.User)
                .Where(od => od.Order.UserId == userId && !od.Order.IsFinaly)
                .ToList();
        }

        public List<OrderModel> GetAllOrderByUserId(int userId)
        {
            var result = _context.Orders
                .Include(o => o.User)
                .Where(f => f.UserId == userId && !f.IsFinaly).ToList();
            return result;
        }

        public void DeleteOrder(int userId)
        {
            var order = GetAllOrderByUserId(userId);
            if (order.Count <= 0) return;
            _context.Orders.RemoveRange(order);
            _context.SaveChanges();
        }

        public void DeleteOrderDetail(int userId)
        {
            var orderDetail = GetAllOrderDetailByUserId(userId);
            if (orderDetail.Count <= 0) return;
            _context.OrderDetails.RemoveRange(orderDetail);
            _context.SaveChanges();
        }

        public int GetPricePostByTypePostId(int typePostId)
        {
            return _context.TypePosts
                .Single(u => u.TypePostId == typePostId).pricePost;
        }


        public async Task<DiscountUseType> UseDiscount(int orderId, string code)
        {
            //سفارش با اندازه دلخواه
            var discount =await _context.DisCounts.SingleOrDefaultAsync(d => d.disCountCode == code && d.Item=="D");

            if (discount == null)
                return DiscountUseType.NotFound;

            //if (discount.startDate < DateTime.Now)
            //    return DiscountUseType.ExpirationDate;

            //if (discount.endDate <= DateTime.Now)
            //    return DiscountUseType.ExpirationDate;


            if (discount.useableCount != null && discount.useableCount < 1)
                return DiscountUseType.Finished;

            var order = GetOrderByOrderId(orderId);

            if (_context.UserDiscountCodes.Any(d => d.userId == order.Id && d.disCountId == discount.Id))
                return DiscountUseType.UserUsed;

            //به دست آوردن مبلغ تخفیف
            decimal percent = (order.TotalCost * discount.disCountPercent) / 100;
            //محاسبه قیمت بعد از اعمال مبلغ تخفیف
            //مبلغ تخفیف از مبلغ کل کم میکنیم 
            //مبلغ کل = تعداد * قیمت محصول


            order.DisCountCost = order.TotalCost - percent;
            order.TotalCost = order.DisCountCost;
            UpdateOrder(order);

            if (discount.useableCount != null)
            {
                discount.useableCount -= 1;
            }

            _context.DisCounts.Update(discount);
            _context.UserDiscountCodes.Add(new UserDiscountCode()
            {
                userId = order.UserId,
                disCountId = discount.Id
            });
            await _context.SaveChangesAsync();
            return DiscountUseType.Success;
        }

        public List<DisCount> GetAllDisCounts()
        {
            return _context.DisCounts.ToList();
        }

        public DisCount GetDisCountById(int discountId)
        {
            return _context.DisCounts.Find(discountId);
        }

        public void UpdateDisCount(DisCount discount)
        {
            _context.DisCounts.Update(discount);
            _context.SaveChanges();
        }

        public async Task<List<OrderModel>> GetUserOrdersByUserId(int userId)
        {
            return await _context.Orders
                 .Include(o => o.OrderCategory)
                 .Include(o => o.OrderSubCategory)
                 .Include(o => o.User)
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }
    }
}