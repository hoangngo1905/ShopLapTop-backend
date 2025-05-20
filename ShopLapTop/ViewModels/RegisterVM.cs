using System.ComponentModel.DataAnnotations;

namespace ShopLapTop.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Họ và Tên")]
        [Required(ErrorMessage = "*")]
        [MaxLength(30, ErrorMessage = "Tối đa 30 ký tự")]
        public string Fullname { get; set; }

        [Display(Name = "Tên Đăng Nhập")]
        [Required(ErrorMessage = "*")]
        [MaxLength(20, ErrorMessage = "Tối đa 20 ký tự")]
        public string Username { get; set; }

        [Display(Name = "Mật Khẩu")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "*")]
        [EmailAddress(ErrorMessage = "Chưa đúng định dạng")]
        public string Email { get; set; }

        [Display(Name = "Số Điện Thoại")]
        [Required(ErrorMessage = "*")]
        [MaxLength(20, ErrorMessage = "Tối đa 20 ký tự")]
        [RegularExpression(@"0[98753]\d{8}", ErrorMessage = "Chưa đúng định dạng SĐT Việt Nam")]
        public string Phonenumber { get; set; }

        [Display(Name = "Địa Chỉ")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string Address { get; set; }
    }
}
