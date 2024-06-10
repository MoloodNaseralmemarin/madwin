using System.ComponentModel.DataAnnotations;
using Shop2City.Const;

namespace Shop2City.Core.DTOs.Account
{
    #region RegisterViewModel
    public class RegisterViewModel
    {
        [Display(Name = "نام", Prompt = "نام")]
        [Required(ErrorMessage = ErrorMessage.Required)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLength)]
        public string firstName { get; set; }

        [Display(Name = "نام خانوادگی", Prompt = "نام خانوادگی")]
        [Required(ErrorMessage = ErrorMessage.Required)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLength)]
        public string lastName { get; set; }

        [Display(Name = "شماره همراه", Prompt = "شماره همراه")]
        [Required(ErrorMessage = ErrorMessage.Required)]
        [MaxLength(11, ErrorMessage = ErrorMessage.MaxLength)]
        public string cellPhone { get; set; }

        [Display(Name = "آدرس پستی",Prompt ="آدرس")]
        [MaxLength(500, ErrorMessage = ErrorMessage.MaxLength)]
        [Required(ErrorMessage = ErrorMessage.Required)]
        public string address { get; set; }

        [Display(Name = "کلمه عبور", Prompt = "کلمه عبور")]
        [Required(ErrorMessage = ErrorMessage.Required)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLength)]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Display(Name = "تکرار کلمه عبور", Prompt = "تکرار کلمه عبور")]
        [Required(ErrorMessage = ErrorMessage.Required)]
        [Compare("password", ErrorMessage = ErrorMessage.Compare)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLength)]
        [DataType(DataType.Password)]
        public string confirmPassword { get; set; }

    }
    #endregion

    #region LoginViewModel
    public class LoginViewModel
    {
        [Display(Name = "شماره همراه", Prompt = "شماره همراه")]
        [Required(ErrorMessage = ErrorMessage.Required)]
        [MaxLength(11, ErrorMessage = ErrorMessage.MinLength)]
        public string userName { get; set; }

        [Display(Name = "کلمه عبور", Prompt = "کلمه عبور")]
        [Required(ErrorMessage = ErrorMessage.Required)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLength)]
        public string password { get; set; }

        [Display(Name = "مرا را به خاطر بسپار")]
        public bool rememberMe { get; set; }
    }
    #endregion

    #region ForgotPasswordViewModel
    public class ForgotPasswordViewModel
    {
        [Display(Name = "پست الکترونیکی", Prompt = "پست الکترونیکی")]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLength)]
        [EmailAddress(ErrorMessage = ErrorMessage.EmailAddress)]
        [Required(ErrorMessage = ErrorMessage.Required)]
        public string email { get; set; }
    }
    #endregion

    #region ResetPassword
    public class ResetPasswordViewModel
    {
        public string activeCode { get; set; }

        [Display(Name = "کلمه عبور", Prompt = "کلمه عبور")]
        [Required(ErrorMessage = ErrorMessage.Required)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLength)]
        public string password { get; set; }

        [Display(Name = "تکرار کلمه عبور", Prompt = "تکرار کلمه عبور")]
        [Compare("password", ErrorMessage = ErrorMessage.Compare)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLength)]
        public string confirmPassword { get; set; }
    }
    #endregion

}
