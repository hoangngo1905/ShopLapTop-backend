using Microsoft.AspNetCore.Mvc;
using ShopLapTop.Repository;

namespace ShopLapTop.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        private readonly CategoryRepository _categoryRepo;

        public NavbarViewComponent(CategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _categoryRepo.GetAll();
            return View(categories);
        }
    }
}
