using MultiStoreIntegration.Domain.Entities.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MultiStoreIntegration.Application.Repositories.Store1
{
    public interface Store1IReadRepository<T> : Store1IRepository<T> where T : BaseEntity
    {
        //Select process
        IQueryable<T> GetAll(bool tracking = true);

        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);

        Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T> GetByIdAsync(Guid id, bool tracking = true);

        Task<IEnumerable<T>> GetAllAsync(bool tracking = true);

        Task<List<(string Category, int TotalQuantity)>> GetTotalStockPerCategoryAsync();

    }
}
