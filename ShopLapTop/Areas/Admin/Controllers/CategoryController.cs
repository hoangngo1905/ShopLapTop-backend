using ShopLapTop.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using ShopLapTop.Repository;

namespace ShopLapTop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _categoryRepo;

        public CategoryController(ShopLapTopContext context)
        {
            _categoryRepo = new CategoryRepository(context);
        }

        public IActionResult Index()
        {
            return View(_categoryRepo.GetAll().ToList());
        }

        // THÊM
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepo.Insert(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _categoryRepo.Delete(id);
            return RedirectToAction("Index");
        }

        // SỬA
        public IActionResult Edit(int id)
        {
            var category = _categoryRepo.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepo.Update(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }
    }
}
