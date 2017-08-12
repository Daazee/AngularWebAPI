using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularWebAPI.Abstractions.Interface
{
    public interface IRepositoryBase<TEntity> where TEntity: class
    {
        Task<TEntity> GetItemAsync(int id);
        Task<IEnumerable<TEntity>> GetItemsAsync();
        Task<int> AddItemAsync(TEntity item);
        Task<int> UpdateItemAsync(TEntity item);
        Task<TEntity> RemoveItemAsync(int id);

    }
}
