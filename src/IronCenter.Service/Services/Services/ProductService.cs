using IronCenter.Service.DataAccess.Interfaces;
using IronCenter.Service.Domain.Products;
using IronCenter.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCenter.Service.Services.Services
{
    public class ProductService : IProductService
    {
        public Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            throw new NotImplementedException();
        }
    }

}
