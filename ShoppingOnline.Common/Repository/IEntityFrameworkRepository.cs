using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingOnline.Common.Repository
{
    public interface IEntityFrameworkRepository<TEntity>:IDisposable where TEntity: class
    {
        #region CRUD Operations

        TEntity Add(TEntity Entity);

        TEntity Update(TEntity Entity);

        void Delete(TEntity Entity);

        TEntity GetById(object KeyValue);

        IQueryable<TEntity> GetAll();

        int SaveChanges();

        #endregion

        #region async Operations
        Task<int> SaveChangesAsync();
        #endregion
    }
}
