using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop2City.DataLayer.Entities.Products;

namespace Shop2City.Core.DTOs.Customers
{
    public class ProductVM
    {
        public  Product Product { get; set; }=new Product();

        [ValidateNever]
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
        [ValidateNever]
        public  IEnumerable<SelectListItem> Categories { get; set; }
    }
}
