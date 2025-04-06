using IronCenter.Service.Domain.Storages;
using IronCenter.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCenter.Service.Services.Interfaces
{
    public interface IStorageService : IService<Storage>
    {
        Task<List<Storage>> GetStoragesByProductIdAsync(int productId);
    }

}
