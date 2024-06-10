using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Shop2City.Const;
using Shop2City.DataLayer.Entities.Orders;

namespace Shop2City.Core.DTOs.Orders
{
    #region ShowCartViewModel
    public class ShowCartViewModel
    {
        public string ImageName { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }

    }
    public class ShowCartItemViewModel
    {
        public int productId { get; set; }

        public int quantity { get; set; }

        public int totalPrice { get; set; }
    }

    public class ShowFactorViewModel
    {
        public int OrderDetailId { get; set; }
        public string ImageName { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public int Sum { get; set; }

    }


    public class ShowOrderViewModel1
    {
        //Cost = o.Cost,
        //           Count = o.Count,
        //           CreateDate = DateTime.Now,
        //           Height = o.Height,
        //           ProductName = o.Product.ProductName,
        //           OrderId=o.Id,
        //           TotalCost = o.TotalCost,
        //           Width=o.Width,

        public int ProductId { get; set; }
        [Display(Name = "عرض")]
        public int Width { get; set; }

        [Display(Name = "ارتفاع")]
        public int Height { get; set; }

        [Display(Name = "تعداد")]
        public int Count { get; set; }
        public decimal Cost { get; set; }
        public decimal TotalCost { get; set; }

    }



    public class OrderViewModel
    {

        [Display(Name = "گروه اصلی")]
        [Required(ErrorMessage = "انتخاب گروه اصلی الزامی می باشد")]
        public int CategoryId { get; set; }

        [Display(Name = "گروه فرعی")]
        [Required(ErrorMessage = "انتخاب گروه فرعی الزامی می باشد")]
        public int SubCategoryId { get; set; }

        [Display(Name = "ارتفاع")]
        [Required(ErrorMessage ="وارد کردن اندازه ارتفاع الزامی می باشد")]
        [Range(0, 400, ErrorMessage = ErrorMessage.Range)]
        public int Height { get; set; }

        [Display(Name = "عرض")]
        [Required(ErrorMessage = "وارد کردن اندازه عرض الزامی می باشد")]
        [Range(0, 400, ErrorMessage = ErrorMessage.Range)]

        public int Width { get; set; }

        [Display(Name = "تعداد")]

        [Range(1, 400, ErrorMessage = ErrorMessage.Range)]
        public int Count { get; set; }
    }

    public class OrderDetailViewModel
    { 
        public int Id { get; set; }

        public int orderId { get; set; }
        public int CalculationId { get; set; }
        public DateTime CreateDate { get; set; }

        public string CalculationTitle { get; set; }

        public decimal Cost { get;set; }

        public decimal TotalCost { get; set; }

    }
    #endregion
}
