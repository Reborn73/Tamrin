using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tamrin.Entities.User;

namespace Tamrin.Api.Models
{
    public class SignInUserDto
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "{0} را وارد نمایید.", AllowEmptyStrings = false)]
        [MaxLength(120, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد.")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل نامعتبر است.")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "{0} را وارد نمایید.", AllowEmptyStrings = false)]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد.")]
        public string Password { get; set; }
    }

    public class RegisterUserDto : IValidatableObject
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "{0} را وارد نمایید.", AllowEmptyStrings = false)]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد.")]

        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "{0} را وارد نمایید.", AllowEmptyStrings = false)]
        [MaxLength(120, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد.")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل نامعتبر است.")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "{0} را وارد نمایید.", AllowEmptyStrings = false)]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد.")]

        public string Password { get; set; }

        [Display(Name = "جنسیت")]
        [Required(ErrorMessage = "{0} را وارد نمایید.", AllowEmptyStrings = false)]
        public GenderType GenderType { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (UserName.Equals("test", StringComparison.OrdinalIgnoreCase))
                yield return new ValidationResult("نام کاربری نمی تواند test باشد.", new[] { nameof(UserName) });
            if (Password.Equals("123"))
                yield return new ValidationResult("کلمه عبور نمی تواند ۱۲۳ باشد.", new[] { nameof(Password) });
        }
    }
}
