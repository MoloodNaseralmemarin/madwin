using Microsoft.EntityFrameworkCore;
using Shop2City.DataLayer.Context;
using Shop2City.DataLayer.Entities.DisCounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<DisCount> GetAllDisCounts()
        {
            return _context.DisCounts.ToList();
        }
    }
}
