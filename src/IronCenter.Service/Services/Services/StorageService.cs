using IronCenter.Service.DataAccess.Interfaces;
using IronCenter.Service.Domain.Storages;
using IronCenter.Service.Services.Interfaces;

namespace IronCenter.Service.Services.Services
{
    public class StorageService : IStorageService
    {
        public Task<List<Storage>> GetStoragesByProductIdAsync(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
