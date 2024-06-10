using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shop2City.DataLayer.Entities.Orders
{
    public class GiftCard
    {
        public int GiftCardId { get; set; }

        /// <summary>
        /// Gets or sets the amount
        /// </summary>
        [Display(Name="مقدار اولیه")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gift card is activated
        /// </summary>
        [Display(Name = "کارت هدیه فعال شده")]
        public bool IsGiftCardActivated { get; set; }

        /// <summary>
        /// Gets or sets a gift card coupon code
        /// </summary>
        [Display(Name = "کد کوپن")]
        public string GiftCardCouponCode { get; set; }

        /// <summary>
        /// Gets or sets a message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOn { get; set; }
    }
}
