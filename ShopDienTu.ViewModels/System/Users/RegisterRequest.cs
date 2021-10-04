using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopDienTu.ViewModels.System.Users
{
    public class RegisterRequest
    {
        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Không được để trống")]
        public string FirstName { get; set; }

        [Display(Name = "Họ")]
        [Required(ErrorMessage = "Không được để trống")]
        public string LastName { get; set; }

        [Display(Name = "Ngày Sinh")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Không được để trống")]
        public DateTime Dob { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Không được để trống")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Số Điện Thoại")]
        [Required(ErrorMessage = "Không được để trống")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Tài Khoản")]
        [Required(ErrorMessage = "Không được để trống")]
        public string UserName { get; set; }

        [Display(Name = "Mật Khẩu")]
        [Required(ErrorMessage = "Không được để trống")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Nhập lại Mật Khẩu")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Không được để trống")]
        [Compare("Password", ErrorMessage = "Mật khẩu không trùng khớp")]
        public string ConfirmPassword { get; set; }
    }
}
