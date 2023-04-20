using Microsoft.EntityFrameworkCore;
using MovieRentApp.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRentApp.Repository;

public class Repository : IRepository
{
    private readonly AppDbContext _dbContext;

    public Repository(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<TEntity> AddEntityAsync<TEntity>(TEntity entity) where TEntity : class
    {
        await _dbContext.Set<TEntity>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task EditEntityAsync<TEntity>(TEntity entity) where TEntity : class
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteEntityAsync<TEntity>(TEntity entity) where TEntity : class
    {
        _dbContext.Set<TEntity>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<TEntity>> GetEntitiesAsync<TEntity>() where TEntity : class
    {
        return await _dbContext.Set<TEntity>().ToListAsync();
    }

    public IEnumerable<TEntity> GetEntities<TEntity>() where TEntity : class
    {
        return _dbContext.Set<TEntity>().ToList();
    }
}
