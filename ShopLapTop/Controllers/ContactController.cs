using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopLapTop.Model.Entities;
using System;

namespace ShopLapTop.Controllers
{
    public class ContactController : Controller
    {
        private readonly ShopLapTopContext _context;

        public ContactController(ShopLapTopContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                // Lấy tên người dùng đã đăng nhập từ Identity
                string username = User.Identity.Name;

                // Lấy thông tin người dùng từ cơ sở dữ liệu
                var user = _context.Users.SingleOrDefault(u => u.Username == username);

                if (user != null)
                {
                    // Gán thông tin người dùng vào feedback trước khi lưu vào cơ sở dữ liệu
                    feedback.CreatedAt = DateTime.Now;
                    feedback.UserId = user.Id; // Giả sử Id của người dùng là UserId trong Feedback model
                    _context.Feedbacks.Add(feedback);
                    _context.SaveChanges();

                    // Redirect hoặc trả về thông báo thành công
                    return RedirectToAction("Index");
                }
                else
                {
                    // Xử lý khi không tìm thấy thông tin người dùng
                    // Bạn có thể chọn phương thức phù hợp, ví dụ: trả về lỗi hoặc thông báo không tìm thấy người dùng
                }
            }
            // Nếu có lỗi xảy ra, trả về lại trang contact với thông báo lỗi
            return View("Index", feedback);
        }
    }
}
