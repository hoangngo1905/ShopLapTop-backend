using ShopLapTop.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLapTop.Repository
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(ShopLapTopContext context) : base(context)
        {
        }
    }
}
