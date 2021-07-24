using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopDienTu.ViewModels.System.Users
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName ko được để trống");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password ko được để trống");
               // .MinimumLength(6).WithMessage("Password có ít nhất 6 ký tự");
        }
    }
}
