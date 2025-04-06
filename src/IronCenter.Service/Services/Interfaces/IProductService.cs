using IronCenter.Service.Domain.Products;
using IronCenter.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCenter.Service.Services.Interfaces
{
    public interface IProductService : IService<Product>
    {
        // Agar Productga xos maxsus metodlar bo'lsa, bu yerga yoziladi
        Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId);
    }

}
