using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopLapTop.Model.Entities;
using ShopLapTop.ViewModels;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShopLapTop.Controllers
{
    public class UserController : Controller
    {
        private readonly ShopLapTopContext db;

        public UserController(ShopLapTopContext context)
        {
            db = context;
        }

        #region DangKy
        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DangKy(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Fullname = model.Fullname,
                    Username = model.Username,
                    Password = model.Password,
                    Email = model.Email,
                    Phonenumber = model.Phonenumber,
                    Address = model.Address
                };

                db.Users.Add(user);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
        #endregion

        #region DangNhap
        [HttpGet]
        public IActionResult DangNhap(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DangNhap(LoginVM model, string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            if (ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.NameIdentifier, user.Username)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties { IsPersistent = true };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không đúng");
                }
            }
            return View(model);
        }
        #endregion

        [Authorize]
        public IActionResult Profile()
        {
            var username = HttpContext.User.Identity.Name;

            var user = db.Users.FirstOrDefault(u => u.Username == username);

            return View(user);
        }


        [Authorize]
        public async Task<IActionResult> DangXuat()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
