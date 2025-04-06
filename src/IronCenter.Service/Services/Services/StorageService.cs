using IronCenter.Service.DataAccess.Interfaces;
using IronCenter.Service.Domain.Storages;
using IronCenter.Service.Services.Interfaces;

namespace IronCenter.Service.Services.Services
{
    public class StorageService : Service<Storage>, IStorageService
    {
        private readonly IRepository<Storage> _repository;
        public StorageService(IRepository<Storage> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<List<Storage>> GetStoragesByProductIdAsync(int productId)
        {
            var allStorages = await _repository.GetAllAsync();
            return allStorages.Where(s => s.ProductId == productId).ToList();
        }
    }
}
