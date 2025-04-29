namespace MultiStoreIntegration.Application.Repositories.Store3
{
    public interface Store3IWriteRepository<T> : Store3IRepository<T> where T : class
    {
        Task<bool> AddAsync(T model);
        Task<bool> AddRangeAsync(List<T> datas);
        Task<bool> RemoveAsync(string id);
        Task<bool> UpdateAsync(T model);
    }
}
