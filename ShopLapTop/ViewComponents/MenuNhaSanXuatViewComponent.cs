using Microsoft.AspNetCore.Mvc;
using ShopLapTop.Repository;
using System.Collections.Generic;
using System.Linq;

public class MenuNhaSanXuatViewComponent : ViewComponent
{
    private readonly ManufacturerRepository _manufacturerRepo;
    private readonly ProductRepository _productRepo;

    public MenuNhaSanXuatViewComponent(ManufacturerRepository manufacturerRepo, ProductRepository productRepo)
    {
        _manufacturerRepo = manufacturerRepo;
        _productRepo = productRepo;
    }

    public IViewComponentResult Invoke()
    {
        var manufacturers = _manufacturerRepo.GetAll().ToList();
        var manufacturerCounts = new Dictionary<int, int>();

        foreach (var manufacturer in manufacturers)
        {
            var count = _productRepo.GetAll().Count(p => p.ManufacturerId == manufacturer.Id);
            manufacturerCounts.Add(manufacturer.Id, count);
        }

        ViewBag.ManufacturerCounts = manufacturerCounts;
        return View(manufacturers);
    }
}
