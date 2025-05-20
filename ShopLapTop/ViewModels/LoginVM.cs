using System.ComponentModel.DataAnnotations;

namespace ShopLapTop.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "Tên Đăng Nhập")]
        [Required(ErrorMessage = "Chưa điền tên đăng nhập")]
        [MaxLength(20, ErrorMessage = "Tối đa 20 ký tự")]
        public string Username { get; set; }

        [Display(Name = "Mật Khẩu")]
        [Required(ErrorMessage = "Chưa điền mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
