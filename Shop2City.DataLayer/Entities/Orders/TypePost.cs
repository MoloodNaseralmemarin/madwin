using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Shop2City.Const;

namespace Shop2City.DataLayer.Entities.Orders
{
    public class TypePost
    {
        public TypePost()
        {
                
        }
        [Key]
        public int TypePostId { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = ErrorMessage.Required)]
        [MaxLength(200, ErrorMessage = ErrorMessage.MaxLength)]
        public string TypePostTitle { get; set; }
        public int pricePost { get; set; }
         
        public List<Factor> Factors { get; set; }

        public List<OrderModel> Orders { get; set; }
    }
}
