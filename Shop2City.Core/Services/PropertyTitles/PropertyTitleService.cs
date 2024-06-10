using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shop2City.Core.DTOs.PropertyTitles;
using Shop2City.DataLayer.Context;
using Shop2City.DataLayer.Entities.Properties;

namespace Shop2City.Core.Services.PropertyTitles
{
    public class PropertyTitleService : IPropertyTitleService
    {
        private readonly Shop2CityContext _context;

        public PropertyTitleService(Shop2CityContext context)
        {
            _context = context;
        }
       public PropertyTitleForAdminViewModel GetPropertyTitle(int pageId = 1, string title = "")
        {
            IQueryable<PropertyTitle> result = _context.PropertyTitles.Include(pt=>pt.Properties);
            // Show Item In Page
            var take = 5;
            var skip = (pageId - 1) * take;
            var list = new PropertyTitleForAdminViewModel
            {
                CurrentPage = pageId,
                PageCount = (int)Math.Ceiling(decimal.Divide(result.Count(), take)),
                PropertyTitles = result.Skip(skip).Take(take).ToList()
            };
            return list;
        }
    }
}
