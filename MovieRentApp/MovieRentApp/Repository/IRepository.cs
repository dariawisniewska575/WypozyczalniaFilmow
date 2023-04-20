using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRentApp.Repository;

public interface IRepository
{
    Task<TEntity> AddEntityAsync<TEntity>(TEntity entity) where TEntity : class;
    Task EditEntityAsync<TEntity>(TEntity entity) where TEntity : class;
    Task DeleteEntityAsync<TEntity>(TEntity entity) where TEntity : class;
    Task<IEnumerable<TEntity>> GetEntitiesAsync<TEntity>() where TEntity : class;
    IEnumerable<TEntity> GetEntities<TEntity>() where TEntity : class;
}