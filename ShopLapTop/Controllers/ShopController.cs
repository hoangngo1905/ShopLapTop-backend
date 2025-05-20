using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopLapTop.Model.Entities;
using ShopLapTop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopLapTop.Controllers
{
    public class ShopController : Controller
    {
        private readonly ILogger<ShopController> _logger;
        private readonly ProductRepository _productRepo;
        private readonly CategoryRepository _categoryRepo;
        private readonly ManufacturerRepository _manufacturerRepo;

        public ShopController(ILogger<ShopController> logger, ShopLapTopContext context)
        {
            _logger = logger;
            _productRepo = new ProductRepository(context);
            _categoryRepo = new CategoryRepository(context);
            _manufacturerRepo = new ManufacturerRepository(context);
        }

        public IActionResult Index(string category, string manufacturer, string search, int page = 1)
        {
            var allProducts = _productRepo.GetAll().ToList();

            if (!string.IsNullOrEmpty(category) && category != "all")
            {
                if (int.TryParse(category, out int categoryId))
                {
                    allProducts = allProducts.Where(p => p.CategoryId == categoryId).ToList();
                }
            }

            if (!string.IsNullOrEmpty(manufacturer) && manufacturer != "all")
            {
                if (int.TryParse(manufacturer, out int manufacturerId))
                {
                    allProducts = allProducts.Where(p => p.ManufacturerId == manufacturerId).ToList();
                }
            }

            if (!string.IsNullOrEmpty(search))
            {
                allProducts = allProducts.Where(p => p.Name.ToLower().Contains(search.ToLower())).ToList();
            }

            int pageSize = 6;
            int totalProducts = allProducts.Count;
            int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            var paginatedProducts = allProducts.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(paginatedProducts);
        }

        public IActionResult Detail(int id)
        {
            var product = _productRepo.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            var imageUrls = _productRepo.GetImageUrlsByProductId(id);
            ViewBag.ImageUrls = imageUrls;

            var specifications = _productRepo.GetSpecificationsByProductId(id);
            ViewBag.Specifications = specifications;

            return View(product);
        }
    }
}
