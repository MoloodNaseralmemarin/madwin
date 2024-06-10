using Shop2City.DataLayer.Entities.Ordering;
using Shop2City.DataLayer.Entities.Products;
using Shop2City.DataLayer.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop2City.Core.Services.Wages
{
    public interface IWageService
    {
        int WageVlaue();

        List<WageModel> GetWages();

        WageModel GetWagesById(int id);
        #region Edit
        void UpdateWage(WageModel wageModel);
        #endregion


    }
}
