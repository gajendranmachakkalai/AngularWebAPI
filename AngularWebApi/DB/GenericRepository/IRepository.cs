using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AngularWebApi.DB.GenericRepository
{
    /// <summary>
    /// Generic Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Delete by entity
        /// </summary>
        /// <param name="entity"></param>
        Task DeleteAsync(T entity);

        /// <summary>
        /// Find the data with expression
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        IEnumerable<T> Find(Expression<Func<T, bool>> where);

        /// <summary>
        /// GetAll the data
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Get List of entites by appling filters, order, skip, take and properties
        /// </summary>
        /// <param name="filter">Filter expression</param>
        /// <param name="orderBy">Order by column</param>
        /// <param name="includeProperties">Include the properties in selection</param>
        /// <param name="first">Take</param>
        /// <param name="offset">Skip</param>
        /// <returns></returns>
        IQueryable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>,
            IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            int first = 0, int offset = 0);

        /// <summary>
        /// Get data by key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetAsync(int id);

        /// <summary>
        /// Insert record async
        /// </summary>
        /// <param name="entity">model</param>
        /// <returns></returns>
        //Task InsertAsync(T entity);
        Task<T> InsertAsync(T entity);

        /// <summary>
        /// Update record async
        /// </summary>
        /// <param name="entity">model</param>
        /// <returns></returns>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Insert the list of records
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task InsertRangeAsync(List<T> entities);

        /// <summary>
        /// Update the list of records
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        void UpdateRange(List<T> entities);

        /// <summary>
        /// Delete the entities
        /// </summary>
        /// <param name="entities"></param>
        void DeleteRange(List<T> entities);
    }
}