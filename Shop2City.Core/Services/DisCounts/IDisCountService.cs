using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Shop2City.Core.DTOs.DisCounts;
using Shop2City.DataLayer.Entities.DisCounts;

namespace Shop2City.Core.Services.DisCounts
{
    public interface IDisCountService
    {
        List<DisCountViewModel> GetAllDisCounts();

        void AddDisCount(DisCount discount);

        DisCount GetDisCountByDisCountId(int disCountId);

        void UpdateDisCount(DisCount discount);

        void DeleteDisCount(int discount);

        bool IsExistDisCountCode(string discountcode);
    }
}
