using System;
using System.Collections.Generic;
using System.Text;

namespace Shop2City.Const
{
    public class ErrorMessage
    {
        public const string Required = "لطفاً {0} را وارد کنید.";
        public const string MaxLength = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .";
        public const string MinLength = "{0} نمی تواند کمتر از {1} کاراکتر باشد .";
        public const string Compare = "کلمه های عبور وارد شده یکی نمی باشد";
        public const string EmailAddress = "پست الکترونیکی وارد شده معتبر نمی باشد.";
        public const string RegularExpression = "قالب {0} اشتباه است.";
        public const string Range = "اندازه {0} باید بین {1} تا {2}  باشد";

        public const string InvalidCellPhone = "قبلاً با این شماره همراه ثبت نام کرده اید";
        public const string InvalidEmail = "ایمیل معتبر نمی باشد. ";
        public const string InvalidUserName = "نام کاربری معتبر نمی باشد. ";
        public const string NoUserFound = "کاربری با مشخصات وارد شده یافت نشد.";
        public const string YourAccountIsNotActive = "حساب کاربری شما فعال نمی باشد.";

        public const string CurrentPasswordIsIncorrect = "کلمه عبور فعلی صحیح نمی باشد.";
    }
}
