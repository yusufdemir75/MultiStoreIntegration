using MultiStoreIntegration.Domain.Entities.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Repositories.Store2
{
    public interface Store2IWriteRepository<T> : Store2IRepository<T> where T : BaseEntity
    {
        //add, delete , update process

        Task<bool> AddAsync(T model);

        Task<bool> AddRangeAsync(List<T> datas);

        bool Remove(T model);

        bool RemoveRange(List<T> datas);

        Task<bool> RemoveAsync(string id);

        bool Update(T model);

        void Attach(T entity);

        Task<int> SaveAsync();


    }
}
