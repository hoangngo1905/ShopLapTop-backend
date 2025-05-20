using Microsoft.EntityFrameworkCore;
using ShopLapTop.IRepository;
using ShopLapTop.Model.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ShopLapTop.Repository
{
    public class ProductRepository : GenericRepository<Product>
    {
        private readonly ShopLapTopContext _context;

        public ProductRepository(ShopLapTopContext context) : base(context)
        {
            _context = context;
        }

        public new List<Product> GetAll()
        {
            return _context.Products
                           .Include(p => p.Manufacturer)
                           .Include(p => p.Category)
                           .ToList();  // Chuyển đổi thành danh sách
        }

        public new Product GetById(int id)
        {
            return _context.Products
                           .Include(p => p.Manufacturer)
                           .Include(p => p.Category)
                           .FirstOrDefault(p => p.Id == id)!;
        }

        // Thêm phương thức GetImageUrlsByProductId ( lấy dữ liệu từ Ảnh )
        public List<string> GetImageUrlsByProductId(int productId)
        {
            var imageUrls = _context.Images.Where(i => i.ProductId == productId)
                                           .Select(i => i.Url)
                                           .ToList();
            return imageUrls;
        }

        // Thêm phương thức GetSpecificationsByProductId ( lấy dữ liệu từ Thông Số Kỹ Thuật )
        public List<Specification> GetSpecificationsByProductId(int productId)
        {
            var specifications = _context.Specifications
                                         .Where(s => s.ProductId == productId)
                                         .ToList();
            return specifications;
        }
    }
}
