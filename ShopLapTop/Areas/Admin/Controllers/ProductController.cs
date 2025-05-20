using Microsoft.AspNetCore.Mvc;
using ShopLapTop.Model.Entities;
using ShopLapTop.Repository;
using System;

namespace ShopLapTop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepo;
        private readonly ManufacturerRepository _manufacturerRepo;
        private readonly CategoryRepository _categoryRepo;

        public ProductController(ProductRepository productRepo, ManufacturerRepository manufacturerRepo, CategoryRepository categoryRepo)
        {
            _productRepo = productRepo;
            _manufacturerRepo = manufacturerRepo;
            _categoryRepo = categoryRepo;
        }

        public IActionResult Index()
        {
            var products = _productRepo.GetAll();
            return View(products);
        }

        // THÊM
        public IActionResult Create()
        {
            ViewBag.Manufacturers = _manufacturerRepo.GetAll();
            ViewBag.Categories = _categoryRepo.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.CreateAt = DateTime.Now;
                product.UpdateAt = DateTime.Now;
                _productRepo.Insert(product);
                return RedirectToAction("Index");
            }
            ViewBag.Manufacturers = _manufacturerRepo.GetAll();
            ViewBag.Categories = _categoryRepo.GetAll();
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _productRepo.Delete(id);
            return RedirectToAction("Index");
        }

        // SỬA
        public IActionResult Edit(int id)
        {
            var product = _productRepo.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Manufacturers = _manufacturerRepo.GetAll();
            ViewBag.Categories = _categoryRepo.GetAll();
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                // Lấy sản phẩm hiện tại từ database
                var existingProduct = _productRepo.GetById(product.Id);
                if (existingProduct == null)
                {
                    return NotFound();
                }

                // Chỉ cập nhật các trường cần thiết, không thay đổi CreateAt
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Quantity = product.Quantity;
                existingProduct.Discount = product.Discount;
                existingProduct.Thumbnail = product.Thumbnail;
                existingProduct.Status = product.Status;
                existingProduct.ManufacturerId = product.ManufacturerId;
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.Description = product.Description;
                existingProduct.UpdateAt = DateTime.Now;

                _productRepo.Update(existingProduct);
                return RedirectToAction("Index");
            }
            ViewBag.Manufacturers = _manufacturerRepo.GetAll();
            ViewBag.Categories = _categoryRepo.GetAll();
            return View(product);
        }
    }
}
