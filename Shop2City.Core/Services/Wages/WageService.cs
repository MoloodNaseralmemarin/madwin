using Shop2City.DataLayer.Context;
using Shop2City.DataLayer.Entities.Ordering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shop2City.Core.Services.Wages
{
    public class WageService : IWageService
    {
        private readonly Shop2CityContext _context;

        public WageService(Shop2CityContext context)
        {
            _context = context;
        }
        public int WageVlaue()
        {
            return _context.Wages.OrderBy(w => w.Id).LastOrDefault().Value;
        }

        public List<WageModel> GetWages()
        {
            return _context.Wages.ToList();
        }
        public WageModel GetWagesById(int id)
        {
            return _context.Wages.Find(id);    
        }
        public void UpdateWage(WageModel wage)
        {
            _context.Wages.Update(wage);
            _context.SaveChanges();
        }
    }
}
