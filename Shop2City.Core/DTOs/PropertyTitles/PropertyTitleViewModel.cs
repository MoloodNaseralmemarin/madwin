using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop2City.DataLayer.Entities.Properties;

namespace Shop2City.Core.DTOs.PropertyTitles
{
    public class PropertyTitleForAdminViewModel
    {
        public List<PropertyTitle> PropertyTitles { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
