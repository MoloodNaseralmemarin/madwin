using System.ComponentModel.DataAnnotations;
using Shop2City.Const;
using Shop2City.DataLayer.Entities.Common;
using Shop2City.DataLayer.Entities.Ordering;
using Shop2City.DataLayer.Entities.Orders;
using Shop2City.DataLayer.Entities.Products;

namespace Shop2City.DataLayer.Entities.Users
{
    public class User:BaseEntity
    {
        #region Ctor
        public User()
        {

        }
        #endregion
        #region Field
        [Display(Name = "نام")]
        [Required(ErrorMessage = ErrorMessage.Required)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLength)]
        public string FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = ErrorMessage.Required)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLength)]
        public string LastName { get; set; }
        [Display(Name = "شماره همراه")]
        [Required(ErrorMessage = ErrorMessage.Required)]
        [MaxLength(11, ErrorMessage = ErrorMessage.MaxLength)]
        public string CellPhone { get; set; }

        [Display(Name = "شماره ثابت")]
        [MaxLength(11, ErrorMessage = ErrorMessage.MaxLength)]
        public string TelPhone { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = ErrorMessage.Required)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLength)]
        public string? UserName { get; set; }

        [Display(Name = "کلمه عبور")]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLength)]
        public string Password { get; set; }

        [Display(Name = "آدرس پستی")]
        [MaxLength(500, ErrorMessage = ErrorMessage.MaxLength)]
        public string Address { get; set; }


        #endregion
        #region Relationship

        public virtual List<UserRole> UserRoles { get; set; }
        public virtual List<Product> Products { get; set; }
        public virtual List<Factor> Factors { get; set; }

        public virtual List<OrderModel> OrderModels { get; set; }

        public List<LoginHistory> LoginHistories { get; set; }

        public List<ShoppingCarts.ShoppingCart> ShoppingCart { get; set; }
        #endregion
    }
}
