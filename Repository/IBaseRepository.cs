using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        TEntity GetEntity(Expression<Func<TEntity, bool>> filter);
        IQueryable<TEntity> GetIQueryable();
        IQueryable<TEntity> GetIQueryable(Expression<Func<TEntity, bool>> filter);
        IQueryable<TEntity> GetIQueryable<TOderKey>(Expression<Func<TEntity, bool>> filter, int pageIndex, int pageSize, Expression<Func<TEntity, TOderKey>> sortKeySelector, bool isAsc = true);
        int TotalCount();
        int Count(Expression<Func<TEntity, bool>> predicate);
        void Update(TEntity instance);
        void UpdateByNeed(TEntity instance, params string[] properties);
        void Add(TEntity instance);
        void AddList(IEnumerable<TEntity> instancelist);
        void Delete(TEntity instance);
        void DeleteList(IEnumerable<TEntity> instance);
        void ExecuteSQLCommand(string sqlString, params object[] pampaters);
        IQueryable<TEntity> ExecQuery(string sqlString, params object[] pampaters);
        void BulkInsertList<T>(string tableName, IList<T> list) where T : class;
        void BulkInsertTable(string tableName, DataTable table);

        //szy
        /// <summary>
        /// 在可以为空值的属性 的value是空的时候，不跟新这个字段
        /// </summary>
        /// <param name="instance"></param>
        void UpdateWithoutNullablePropertyWhenNull(TEntity instance);
    }
}
