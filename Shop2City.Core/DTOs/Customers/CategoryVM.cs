using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop2City.DataLayer.Entities.ShoppingCarts;

namespace Shop2City.Core.DTOs.Customers
{
    public class CategoryVM
    {
        public Category Category { get; set; }=new Category();
        public IEnumerable<Category> categories { get; set; }=new List<Category>();
    }
}
