using IronCenter.Service.Domain.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCenter.Service.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> AddAsync(Category entity);

        Task<List<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task UpdateAsync(Category entity);
        Task DeleteAsync(int id);
    }
}
