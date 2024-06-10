using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop2City.DataLayer.Context;
using Shop2City.DataLayer.Entities.Properties;

namespace Shop2City.Core.Services.PropertyTechnicalProducts
{
    public class PropertyTechnicalProductService : IPropertyTechnicalProductService
    {
        private readonly Shop2CityContext _context;
        public PropertyTechnicalProductService(Shop2CityContext context)
        {
            _context = context;
        }
        public List<PropertyTechnicalProduct> listPropertyTechnicalProductByProductId(int productId)
        {
            var q = _context.PropertyTechnicalProducts.Where(a => a.productId == productId)
               // .Include(a => a.PropertyTechnical)
                .ToList();
                return q;
        }
    }
}
