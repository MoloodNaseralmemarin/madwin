using Microsoft.EntityFrameworkCore;
using Shop2City.Core.DTOs.Orders;
using Shop2City.Core.Services.Products;
using Shop2City.Core.Services.Users;
using Shop2City.DataLayer.Context;
using Shop2City.DataLayer.Entities.Orders;
using Shop2City.DataLayer.Entities.Users;

namespace Shop2City.Core.Services.Factors
{
    public class FactorService : IFactorService
    {
        private readonly Shop2CityContext _context;
        private readonly IUserService _userService;
        private readonly IProductService _productService;

        public FactorService(Shop2CityContext context, IUserService userService, IProductService productService)
        {
            _context = context;
            _userService = userService;
            _productService = productService;
        }

        public int AddFactor(string userName, int productId, int Count)
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
                    FactorSum = 0,
                    TotalPrice = 0,

                };
                _context.Factors.Add(order);
                _context.SaveChanges();
                _context.FactorDetails.Add(new FactorDetail
                {
                    FactorId = order.Id,
                    ProductId = productId,
                    Quantity = Count,
                    Price = product.Price * Count,
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
                        Quantity = Count,
                        Price = product.Price * Count,
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

            }

            UpdatePriceOrder(order.Id);
            return order.Id;

        }
        #region آپدیت قیمت سفارش
        public async void UpdatePriceOrder(int orderId)
        {
            var order = await GetFactorByFactorId(orderId);
            order.FactorSum = _context.FactorDetails
                .Where(d => d.FactorId == orderId)
                .Sum(d => d.Price);
            order.TotalPrice = order.FactorSum;
            _context.Factors.Update(order);
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

        public List<Factor> GetAllOrder(int orderId)
        {

            IQueryable<Factor> result = _context.Factors;

            if (orderId != 0)
            {
                result = result.Where(p => p.Id == orderId);
            }
            return result
                .Include(o => o.User)
                .OrderByDescending(o => o.CreatedDate == DateTime.Now)
                .ToList();
        }

        public List<FactorDetail> GetAllFactorDetailByUserId(int userId)
        {
            return _context.FactorDetails
                .Include(od => od.Product)
                .Include(od => od.Factor)
                .ThenInclude(od => od.User)
                .Where(od => od.Factor.UserId == userId && !od.Factor.IsFinaly)
                .ToList();

        }

        public async Task<FactorDetail?> GetFactorDetailByProductId(int productId, int userId)
        {
            var result =
                await _context.FactorDetails.FirstOrDefaultAsync(fd =>
                    fd.ProductId == productId && fd.Factor.UserId == userId);
            return result;
        }

        public Factor GetFactorForUserPanel(string userName, int factorId)
        {
            int userId = _userService.GetUserIdByUserName(userName);
            return _context.Factors
                .Include(f => f.FactorDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefault(o => o.UserId == userId && o.Id == factorId && !o.IsFinaly);
        }

        public int GetPricePostByTypePostId(int typepostId)
        {
            return _context.TypePosts
                .Single(u => u.TypePostId == typepostId).pricePost;
        }

        public List<TypePost> TypePosts()
        {
            return _context.TypePosts.ToList();
        }

        public List<Factor> GetUserOrders(int sessionId)
        {
            return _context.Factors
                .Where(o => o.Id == sessionId)
                .ToList();
        }

        public bool IsUserInProduct(string userName, int productId)
        {
            int userId = _userService.GetUserIdByUserName(userName);

            return _context.UserProducts.Any(c => c.userId == userId && c.productId == productId);
        }



        public void UpdateUserIdInOrder(Factor order)
        {
            _context.Factors.Update(order);
            _context.SaveChanges();
        }

        public List<Factor> GetUserOrders(string userName)
        {
            var userId = _userService.GetUserIdByUserName(userName);
            return _context.Factors.Where(o => o.UserId == userId).ToList();
        }

        public Factor GetOrderByUserId(int userId)
        {
            return _context.Factors.Find(userId);
        }

        public int GetFactorIdByUserId(int userId)
        {
            var showFactor = _context.Factors
                .OrderBy(o => o.Id)
                .LastOrDefault(u => u.UserId == userId).Id;

            if (showFactor == null) return 0;
            else
                return showFactor;

        }

        public void UpdateFactor(Factor factor)
        {
            _context.Factors.Update(factor);
            _context.SaveChanges();
        }

        public async Task<Factor> GetFactorByFactorId(int factorId)
        {
            return await _context.Factors.FindAsync(factorId);
        }

        public void DeleteFactorItem(int productId, int userId)
        {

            // جزئیات فاکتور نسبت به شناسه محصول به دست میارم
            var product = GetFactorDetailByProductId(productId, userId);
            //  _context.FactorDetails.RemoveRange(product);
            _context.SaveChanges();
        }

        public int GetTypePostPriceByTypePostId(int TypePostId)
        {
            return _context.TypePosts.Find(TypePostId).pricePost;
        }

        public Factor GetUserFactors(string userName, bool isfinaly)
        {
            int userId = _userService.GetUserIdByUserName(userName);
            return _context.Factors
              .Include(f => f.FactorDetails).ThenInclude(f => f.Product)
               .Include(f => f.User)
              .FirstOrDefault(o => o.UserId == userId && o.IsFinaly == isfinaly);

        }

        public int CountFactors()
        {
            return _context.Factors.Count();
        }

        public async Task<int> GetCountFactorIsFinaly(int userId)
        {
            var count = await _context.Factors
                .Where(f => f.IsFinaly && f.UserId == userId)
                .CountAsync();

            return count > 0 ? count : 0;
        }

        #region GetFactorByUserId
        public List<Factor> GetAllFactorByUserId(int userId)
        {
            var result = _context.Factors
                .Include(o => o.User)

                .Where(f => f.UserId == userId && !f.IsFinaly).ToList();
            return result;

        }
        #endregion
        public void DeleteFactor(int userId)
        {
            var factor = GetAllFactorByUserId(userId);
            if (factor.Count <= 0) return;
            _context.Factors.RemoveRange(factor);
            _context.SaveChanges();

        }

        public void DeleteFactorDetail(int userId)
        {
            var factorDetail = GetAllFactorDetailByUserId(userId);
            if (factorDetail.Count <= 0) return;
            _context.FactorDetails.RemoveRange(factorDetail);
            _context.SaveChanges();


        }

        public List<FactorDetail> GetAllFactorDetailByFactorId(int factorId)
        {
            return _context.FactorDetails
                .Include(od => od.Product)
                .Include(od => od.Factor)
                .ThenInclude(od => od.User)
                .Where(od => od.Factor.Id == factorId && !od.Factor.IsFinaly)
                .ToList();
        }

        public async Task<DiscountUseType> UseDiscount(int factorId, string code)
        {
            //سفارش آماده
            var discount = _context.DisCounts.SingleOrDefault(d => d.disCountCode == code && d.Item == "D");

            if (discount == null)
                return DiscountUseType.NotFound;

            //if (discount.startDate < DateTime.Now)
            //    return DiscountUseType.ExpirationDate;

            //if (discount.endDate <= DateTime.Now)
            //    return DiscountUseType.ExpirationDate;


            if (discount.useableCount != null && discount.useableCount < 1)
                return DiscountUseType.Finished;

            var factor = await GetFactorByFactorId(factorId);

            if (_context.UserDiscountCodes.Any(d => d.userId == factor.Id && d.disCountId == discount.Id))
                return DiscountUseType.UserUsed;

            decimal percent = (factor.FactorSum * discount.disCountPercent) / 100;
            factor.DisCountCost = factor.FactorSum - percent;
            factor.TotalPrice = factor.DisCountCost;
            UpdateFactor(factor);

            if (discount.useableCount != null)
            {
                discount.useableCount -= 1;
            }

            _context.DisCounts.Update(discount);
            _context.UserDiscountCodes.Add(new UserDiscountCode()
            {
                userId = factor.UserId,
                disCountId = discount.Id
            });
            _context.SaveChanges();
            return DiscountUseType.Success;
        }


        public Factor GetFactorByUserId(int userId)
        {
            return _context.Factors
                .Include(o => o.User)
                .FirstOrDefault(o => o.UserId == userId
                && !o.IsFinaly);
        }
    }
}
