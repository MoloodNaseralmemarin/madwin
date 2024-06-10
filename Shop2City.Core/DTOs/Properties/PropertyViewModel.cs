using System.Collections.Generic;
using Shop2City.DataLayer.Entities.Properties;

namespace Shop2City.Core.DTOs.Properties
{
    public class PropertyForAdminViewModel
    { 
        public List<Property> Properties { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
