using Dp.Lll.Infrastrucutre.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AngularWebApi.DB.GenericRepository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly UserDbContext _context;
        private DbSet<T> _entities;
        public Repository(UserDbContext context)
        {
            this._context = context;
            _entities = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _entities.AsNoTracking().AsQueryable();
        }

        public virtual IQueryable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            int first = 0, int offset = 0)
        {
            IQueryable<T> query = _entities.AsNoTracking().AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (offset > 0)
            {
                query = query.Skip(offset);
            }
            if (first > 0)
            {
                query = query.Take(first);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }
        public async Task<T> GetAsync(int id)
        {
            return await _entities.FindAsync(id);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> where)
        {
            return _entities.Where(where).AsNoTracking();
        }

        public async Task<T> InsertAsync(T entity)
        {
            try
            {

         
            
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
            return entity;
        }

        public async Task InsertRangeAsync(List<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entity");
            }
            await _entities.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                _entities.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateRange(List<T> entities)
        {
            try
            {
                if (entities == null)
                {
                    throw new ArgumentNullException("entities");
                }
                _entities.UpdateRange(entities);
                //_entities.AttachRange(entities);
                //_context.Entry(entities).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public void DeleteRange(List<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }
            _entities.RemoveRange(entities);
            _context.SaveChanges();
        }
    }
}
