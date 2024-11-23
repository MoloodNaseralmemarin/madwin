using Microsoft.EntityFrameworkCore;
using Shop2City.Core.DTOs.DisCounts;
using Shop2City.DataLayer.Context;
using Shop2City.DataLayer.Entities.DisCounts;
using Shop2City.DataLayer.Entities.ShoppingCarts;


namespace Shop2City.Core.Services.DisCounts
{
    public class DisCountService : IDisCountService
    {
        private readonly Shop2CityContext _context;

        public DisCountService(Shop2CityContext context)
        {
           _context = context;
        }

        public void AddDisCount(DisCount discount)
        {
            _context.DisCounts.Add(discount);
            _context.SaveChanges();
        }

        public void DeleteDisCount(int id)
        {
            var item=GetDisCountByDisCountId(id);
            if (item != null)
            {
                item.IsDelete = true;
                item.UpdateDate = DateTime.UtcNow;
                UpdateDisCount(item);
            }
        }

        public List<DisCountViewModel> GetAllDisCounts()
        {
            var currentDate = DateTime.Now.Date; // تاریخ فعلی

            var results = _context.DisCounts
                .Select(ds => new DisCountViewModel
                {
                    id = ds.Id,
                    item=ds.Item,
                    createDate =ds.CreateDate,
                    disCountPercent=ds.disCountPercent,
                    disCountCode=ds.disCountCode,
                    useableCount=ds.useableCount,
                    startDate=ds.startDate,
                    endDate = ds.endDate,
                    isInactive = ds.endDate.Date < currentDate ? "غیر فعال شده است" : "فعال"
                })
                .ToList();

            return results;
        }


        public DisCount GetDisCountByDisCountId(int disCountId)
        {
            return _context.DisCounts.Find(disCountId);
        }

        public bool IsExistDisCountCode(string discountcode)
        {
            return _context.DisCounts
             .Any(dc => dc.disCountCode == discountcode);
        }

        public void UpdateDisCount(DisCount discount)
        {
            _context.Update(discount);
            _context.SaveChanges();
        }
    }
}
