using Microsoft.AspNetCore.Mvc;
using ShopLapTop.Model.Entities;
using ShopLapTop.Models;
using Microsoft.AspNetCore.Authorization;
using ShopLapTop.ViewModels;


namespace ShopLapTop.Controllers
{
    public class CartController : Controller
    {
        private readonly ShopLapTopContext db;

        public CartController(ShopLapTopContext context)
        {
            db = context;
        }

        const string CART_KEY = "MYCART";
        public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(CART_KEY) ?? new List<CartItem>();

        public IActionResult Index()
        {
            return View(Cart);
        }

        public IActionResult AddToCart(int id, int quantity = 1) // Them vao gio hang
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.MaHh == id);
            if (item == null)
            {
                var hangHoa = db.Products.SingleOrDefault(p => p.Id == id);
                if (hangHoa == null)
                {
                    TempData["Message"] = $"Không tìm thấy {id}";
                    return Redirect("/404");
                }
                item = new CartItem
                {
                    MaHh = hangHoa.Id,
                    TenHH = hangHoa.Name,
                    DonGia = (double)hangHoa.Price,
                    Hinh = hangHoa.Thumbnail ?? string.Empty,
                    SoLuong = quantity
                };
                gioHang.Add(item);
            }
            else
            {
                item.SoLuong += quantity;
            }

            HttpContext.Session.Set(CART_KEY, gioHang);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int id) // Xoa san pham trong gio hang
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.MaHh == id);
            if (item != null)
            {
                gioHang.Remove(item);
                HttpContext.Session.Set(CART_KEY, gioHang);
            }
            return RedirectToAction("Index");
        }

        public IActionResult DecreaseQuantity(int id) // Giam so luong san pham
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.MaHh == id);
            if (item != null)
            {
                if (item.SoLuong > 1)
                {
                    item.SoLuong--;
                }
                else
                {
                    gioHang.Remove(item);
                }
                HttpContext.Session.Set(CART_KEY, gioHang);
            }
            return RedirectToAction("Index");
        }

        public IActionResult CartSummary() // Dem so san pham trong gio hang
        {
            var totalItems = Cart.Sum(item => item.SoLuong);
            return PartialView("_CartSummary", totalItems);
        }

        [Authorize]
        [HttpGet]
        public IActionResult CheckOut()
        {
            if (Cart.Count == 0)
            {
                return Redirect("/");
            }
            return View(Cart);
        }

        [Authorize]
        [HttpPost]
        public IActionResult CheckOut(CheckOutVM model)
        {
            if (ModelState.IsValid)
            {
                // Lấy tên người dùng đã đăng nhập từ Identity
                string username = User.Identity.Name;

                // Lấy thông tin người dùng từ cơ sở dữ liệu
                var user = db.Users.SingleOrDefault(u => u.Username == username);

                if (user != null)
                {
                    var order = new Order
                    {
                        Fullname = model.HoTen,
                        Phonenumber = model.DienThoai,
                        Email = model.Email,
                        Address = model.DiaChi,
                        Note = model.GhiChu,
                        OrderDetails = Cart.Select(item => new OrderDetail
                        {
                            ProductId = item.MaHh,
                            Quantity = item.SoLuong,
                            Price = (decimal)item.DonGia
                        }).ToList(),
                        Total = (decimal?)(Cart.Sum(item => item.ThanhTien) + 30000),
                        Orderdate = DateTime.Now,
                        User = user // Gán thông tin người dùng vào đơn hàng
                    };
                    db.Orders.Add(order);
                    db.SaveChanges();

                    HttpContext.Session.Remove(CART_KEY);
                    HttpContext.Session.SetString("OrderSuccess", "true");
                    return RedirectToAction("OrderSuccess", "Cart");
                }
            }
            return View(model);
        }


        public IActionResult OrderSuccess()
        {
            var orderSuccess = HttpContext.Session.GetString("OrderSuccess");
            if (orderSuccess == "true")
            {
                HttpContext.Session.Remove("OrderSuccess");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
