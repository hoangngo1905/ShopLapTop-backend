using Microsoft.AspNetCore.Mvc;
using ShopLapTop.Repository;

public class MenuLoaiViewComponent : ViewComponent
{
    private readonly CategoryRepository _categoryRepo;
    private readonly ProductRepository _productRepo;

    public MenuLoaiViewComponent(CategoryRepository categoryRepo, ProductRepository productRepo)
    {
        _categoryRepo = categoryRepo;
        _productRepo = productRepo;
    }

    public IViewComponentResult Invoke()
    {
        var categories = _categoryRepo.GetAll().ToList();
        var categoryCounts = new Dictionary<int, int>();

        foreach (var category in categories)
        {
            var count = _productRepo.GetAll().Count(p => p.CategoryId == category.Id);
            categoryCounts.Add(category.Id, count);
        }

        ViewBag.CategoryCounts = categoryCounts;
        return View(categories);
    }
}
