using Microsoft.EntityFrameworkCore;
using Shop2City.DataLayer.Context;
using Shop2City.DataLayer.Entities.Common;

namespace Shop2City.Core.Services
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        #region constructor

        private readonly Shop2CityContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(Shop2CityContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        #endregion

        public IQueryable<TEntity> GetEntitiesQuery()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<TEntity?> GetEntityById(int entityId)
        {
            return await _dbSet.SingleOrDefaultAsync(e => e.Id == entityId);
        }

        public async Task AddEntity(TEntity entity)
        {

            entity.CreateDate = DateTime.Now;
            entity.LastUpdateDate = entity.CreateDate;
            entity.Description = "مشخص نیست";
            entity.IsDelete = false;
            await _dbSet.AddAsync(entity);
        }

        public void UpdateEntity(TEntity? entity)
        {
            if (entity == null) return;
            entity.LastUpdateDate = DateTime.Now;
            _dbSet.Update(entity);
        }
        public void RemoveEntity(TEntity? entity)
        {
            if (entity == null) return;
            entity.IsDelete = true;
            UpdateEntity(entity);
        }

        public async Task RemoveEntity(int entityId)
        {
            var entity = await GetEntityById(entityId);
            RemoveEntity(entity);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
