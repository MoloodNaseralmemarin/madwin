﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Shop2City.DataLayer.Entities.Users;

namespace Shop2City.DataLayer.Entities.Orders
{

    [Table("Factors", Schema = "Orders")]
    public class Factor
    {
        #region ctor
        public Factor()
        {

        }
        #endregion
        #region Field
        [Key]
        [Display(Name = "شناسه")]
        public int FactorId { get; set; }

        [Display(Name = "تاریخ ثبت ")]
        [Required]
        public DateTime createdDate { get; set; }

        [Required]
        public int userId { get; set; }

        /// <summary>
        /// قیمت کل محصولات
        /// </summary>
        [Required]
        public int FactorSum { get; set; }
        /// <summary>
        /// فاکتور پرداخت کرده و فاکتور نهایی شده
        /// </summary>
        public bool IsFinaly { get; set; }
        /// <summary>
        /// مشخص شود که نوع پستی انتخاب کرده
        /// </summary>
        public int typePostId { get; set; }

        /// <summary>
        /// قیمت کل + قیمت پست
        /// </summary>
        public int totalPrice { get; set; }

        #endregion
        #region Relationship
        public virtual User User { get; set; }
        public virtual TypePost TypePost { get; set; }
        public List<FactorDetail> FactorDetails { get; set; }
        #endregion

    }
}