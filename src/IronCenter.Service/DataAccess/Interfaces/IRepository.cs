using IronCenter.Service.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCenter.Service.DataAccess.Interfaces
{
    public interface IRepository<T> where T : Auditable
    {
        Task<T> AddAsync(T entity);





        IQueryable<T> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        void Update(long Id, T entity);
        void Delete(T entity);
     
    }

}
