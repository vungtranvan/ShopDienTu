using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopDienTu.ViewModels.System.Users
{
    public class UpdateUserRequest
    {
        public Guid Id { get; set; }
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
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Không được để trống")]
        public string Email { get; set; }

        [Display(Name = "Số Điện Thoại")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Không được để trống")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Tài Khoản")]
        [Required(ErrorMessage = "Không được để trống")]
        public string UserName { get; set; }
    }
}
