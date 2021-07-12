using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.Common.Repository
{
    public class EntityFrameworkRepository<TEntity> : IEntityFrameworkRepository<TEntity> where TEntity : class, new()
    {

        #region Members

        private DbContext _context;

        #endregion

        #region Constructor

        public EntityFrameworkRepository(DbContext context)
        {
            if (context == null) throw new ArgumentNullException("DB Context NULL");

            _context = context;
        }

        #endregion

        #region CRUD Operations
        public IQueryable<TEntity> GetAll()
        {
            return  _context.Set<TEntity>();
        }

        public TEntity Add(TEntity Entity)
        {
            if (Entity == null) throw new ArgumentNullException("Entity is null");
            try
            {
                _context.Add<TEntity>(Entity);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }


        public TEntity Update(TEntity Entity)
        {
            if (Entity == null) throw new ArgumentNullException("Entity is null");
            try
            {
                _context.Entry<TEntity>(Entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public void Delete(TEntity Entity)
        {
            if (Entity == null) throw new ArgumentNullException("Entity is null");
            try
            {
                _context.Entry<TEntity>(Entity).State = EntityState.Deleted;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public TEntity GetById(object KeyValue)
        {
            if (KeyValue == null) throw new ArgumentNullException("Entity is null");
            try
            {
                return _context.Set<TEntity>().Find(KeyValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Async Operations
        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
        #endregion
        #region Dispose Context

        public void Dispose()
        {
            _context.Dispose();
        }

        #endregion
    }
}
