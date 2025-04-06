using IronCenter.Service.Domain.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCenter.Service.Services.Interfaces
{
    public interface IStorageService 
    {
        Task<List<Storage>> GetStoragesByProductIdAsync(int productId);
    }

}
