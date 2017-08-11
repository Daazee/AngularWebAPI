using AngularWebAPI.Abstractions.Interface;
using AngularWebAPI.DataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularWebAPI.DataAccess.EFRepository
{
    public class GenericRepository<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        protected AngularWebAPIDataContext _db = new AngularWebAPIDataContext();

        public async Task<TEntity> GetItemAsync(int id)
        {
            var model = await _db.Set<TEntity>().FindAsync(id);
            return model;
        }        

        // gets list of items async
        public async Task<IEnumerable<TEntity>> GetItemsAsync()
        {
            var result = await _db.Set<TEntity>().ToListAsync();
            return result;
        }
        // add entity to a set
        public async Task<int> AddItemAsync(TEntity item)
        {
            _db.Set<TEntity>().Add(item);
            return await _db.SaveChangesAsync();
        }

        // updates an entity in a set
        public async Task<int> UpdateItemAsync(TEntity item)
        {
            _db.Entry<TEntity>(item).State = EntityState.Modified;
            return await _db.SaveChangesAsync();
        }

        // removes an entity in a set
        public async Task<TEntity> RemoveItemAsync(int id)
        {
            var query = await _db.Set<TEntity>().FindAsync(id);
            if (query != null)
            {
                _db.Set<TEntity>().Remove(query);
                await _db.SaveChangesAsync();
            }
            return query;
        }


        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
