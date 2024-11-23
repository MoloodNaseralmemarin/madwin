using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Shop2City.Const;
using Shop2City.DataLayer.Entities.Users;

namespace Shop2City.DataLayer.Entities.DisCounts
{
   public class DisCount
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "عنوان")]
        public string Item { get; set; } //O سسفارش با انداره دلخواه -D// سفارش آماده

        [Display(Name ="کد تخفیف")]
        [Required(ErrorMessage = ErrorMessage.Required)]
        [MaxLength(150,ErrorMessage=ErrorMessage.MaxLength)]
        public string disCountCode { get; set; }

        [Display(Name = "درصد کد تخفیف")]
        [Range(0, 100, ErrorMessage = ErrorMessage.Range)]
        [Required(ErrorMessage = ErrorMessage.Required)]
        public int disCountPercent{ get; set; }

        [Display(Name = "تعداد")]
        public int useableCount { get; set; }

        [Display(Name = "تاریخ شروع")]
        public DateTime startDate { get; set; }=DateTime.Now;

        [Display(Name = "تاریخ پایان")]
        public DateTime endDate { get; set; }=DateTime.Now;

        public string? Description { get; set; }
        public bool IsDelete { get; set; }

        public DateTime CreateDate { get; set; }= DateTime.Now;

        public DateTime UpdateDate { get; set; } = DateTime.Now;

        #region Relationship
        public List<UserDiscountCode> UserDiscountCodes { get; set; }
        #endregion



    }
}
