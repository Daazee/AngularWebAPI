using AngularWebAPI.Abstractions.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularWebAPI.Mock.EFRepository
{
    public class GenericRepository<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {

        public GenericRepository()
        {
            
        }
        public virtual Task<int> AddItemAsync(TEntity item)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {

        }

        public virtual Task<TEntity> GetItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IEnumerable<TEntity>> GetItemsAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task<TEntity> RemoveItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> UpdateItemAsync(TEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
