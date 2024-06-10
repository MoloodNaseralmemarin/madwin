using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Shop2City.DataLayer.Entities.Users;

namespace Shop2City.DataLayer.Entities.Products
{
    public class UserProduct
    {
        [Key]
        public int userProductId { get; set; }
        public int userId { get; set; }
        public int productId { get; set; }


        public Product Product { get; set; }
        public User User { get; set; }
    }
}
