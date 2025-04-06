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
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IRepository<Product> _repository;

        public ProductService(IRepository<Product> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            var allProducts = await _repository.GetAllAsync();
            return allProducts.Where(p => p.CategoryId == categoryId).ToList();
        }
    }

}
