using IronCenter.Service.Data;
using IronCenter.Service.DataAccess.Interfaces;
using IronCenter.Service.Domain.Categories;
using IronCenter.Service.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCenter.Service.DataAccess.Repositories
{
    // Repository.cs
    public class Repository<T> : IRepository<T> where T : Auditable
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(AppDbContext dbContext)
        {
            _context = dbContext;
            _dbSet = _context.Set<T>();
        }
        public async Task<T> AddAsync(T entity)
        {
            var entry = await _dbSet.AddAsync(entity);

            await _context.SaveChangesAsync();

            return entry.Entity;
        }
     

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Update(long Id,T entity)
        {
            entity.Id = Id;
            _dbSet.Update(entity);
        }

        public void Delete(T entity) => _context.Remove(entity);

        public Task UpdateAsync<T1>(T1 entity) where T1 : class 
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAllAsync() =>_dbSet;
    }
}
