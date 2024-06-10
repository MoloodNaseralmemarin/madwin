using Shop2City.DataLayer.Entities.Common;

namespace Shop2City.Core.Services
{
    public interface IGenericRepository<TEntity> : IDisposable where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetEntitiesQuery();

        Task<TEntity?> GetEntityById(int entityId);

        Task AddEntity(TEntity entity);
        void UpdateEntity(TEntity? entity);
        void RemoveEntity(TEntity? entity);

        Task RemoveEntity(int entityId);

        Task SaveChanges();
    }
}
