using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Shop2City.DataLayer.Entities.DisCounts;
using Shop2City.DataLayer.Entities.Orders;

namespace Shop2City.DataLayer.Entities.Users
{
   public class UserDiscountCode
    {
        public UserDiscountCode()
        {

        }
        [Key]
        public int Id { get; set; }
        public int userId { get; set; }
        public int disCountId { get; set; }


        #region Relationship
        public User User { get; set; }
        public DisCount DisCount { get; set; }
        #endregion

    }
}
