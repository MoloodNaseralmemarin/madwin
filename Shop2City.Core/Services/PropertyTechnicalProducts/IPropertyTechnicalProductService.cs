using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop2City.DataLayer.Entities.Properties;

namespace Shop2City.Core.Services.PropertyTechnicalProducts
{
    public interface IPropertyTechnicalProductService
    {
        List<PropertyTechnicalProduct> listPropertyTechnicalProductByProductId(int productId);
    }
}
