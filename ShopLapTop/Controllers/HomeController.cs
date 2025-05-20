using Microsoft.AspNetCore.Mvc;
using ShopLapTop.Model.Entities;
using ShopLapTop.Models;
using ShopLapTop.Repository;
using System.Diagnostics;

namespace ShopLapTop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductRepository _productRepo;

        public HomeController(ILogger<HomeController> logger, ShopLapTopContext context)
        {
            _logger = logger;
            _productRepo = new ProductRepository(context);
        }

        public IActionResult Index()
        {
            var newProduct = _productRepo.GetAll().OrderByDescending(p => p.UpdateAt).Take(8).ToList(); // hiển thị 6 sản phẩm mới
            var topProduct = _productRepo.GetAll().OrderByDescending(p => p.Discount).Take(8).ToList(); // hiển thị 6 sản phẩm bán chạy
            var tupleModel = new Tuple<List<Product>, List<Product>>(newProduct, topProduct); // cái ống để nối 2 list
            return View(tupleModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ProductDetail(int id)
        {
            // Chuyển hướng sang action Detail trong ShopController, truyền id của sản phẩm
            return RedirectToAction("Detail", "Shop", new { id = id });
        }
    }
}
