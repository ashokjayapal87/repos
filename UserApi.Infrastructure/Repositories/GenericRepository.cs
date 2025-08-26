using Microsoft.EntityFrameworkCore;
using UserApi.Core.Interfaces;
using UserApi.Models;

namespace UserApi.Infrastructure.Repositories;

public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
{
    #region Variables

    protected readonly DbContextClass _dbContext;

    #endregion

    #region Constructor
    protected GenericRepository(DbContextClass context)
    {
        _dbContext = context;
    }
    #endregion

    #region Public Methods
    public async Task<T> CreateAsync(T entity)
    {
        var result = await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<T?> GetIdAsync(int id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<T> UpdateAsync(T entity)
    {
        var updatedResult = _dbContext.Set<T>().Update(entity);
        await _dbContext.SaveChangesAsync();

        return updatedResult.Entity;
    }

    #endregion
}
