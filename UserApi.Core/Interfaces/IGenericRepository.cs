namespace UserApi.Core.Interfaces;

public interface IGenericRepository<T> where T: class
{
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T?> GetIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
}
