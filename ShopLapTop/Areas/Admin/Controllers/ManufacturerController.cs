using ShopLapTop.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using ShopLapTop.Repository;

namespace ShopLapTop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManufacturerController : Controller
    {
        private readonly ManufacturerRepository manufacturerRepo;

        public ManufacturerController(ShopLapTopContext context) // Thêm đối số context vào đây
        {
            manufacturerRepo = new ManufacturerRepository(context);
        }

        public IActionResult Index()
        {
            return View(manufacturerRepo.GetAll().ToList());
        }

        // THÊM
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                manufacturerRepo.Insert(manufacturer);
                return RedirectToAction("Index");
            }
            return View(manufacturer);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            manufacturerRepo.Delete(id);
            return RedirectToAction("Index");
        }

        // SỬA
        public IActionResult Edit(int id)
        {
            var manufacturer = manufacturerRepo.GetById(id);
            if (manufacturer == null)
            {
                return NotFound();
            }
            return View(manufacturer);
        }

        [HttpPost]
        public IActionResult Edit(Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                manufacturerRepo.Update(manufacturer);
                return RedirectToAction("Index");
            }
            return View(manufacturer);
        }
    }
}
