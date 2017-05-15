using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public abstract class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        public IBaseRepository<TEntity> Repository = new BaseRepository<TEntity>();
        public IList<IDisposable> DisposableObjects { get; private set; }
        public BaseService()
        {
            this.DisposableObjects = new List<IDisposable>();
        }
        protected void AddDisposableObject(object obj)
        {
            IDisposable disposable = obj as IDisposable;
            if (null != disposable)
            {
                this.DisposableObjects.Add(disposable);
            }
        }
        public void Dispose()
        {
            foreach (IDisposable obj in this.DisposableObjects)
            {
                if (null != obj)
                {
                    obj.Dispose();
                }
            }
        }
        public TEntity GetEntity(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter)
        {
            return Repository.GetEntity(filter);
        }

        public IQueryable<TEntity> GetIQueryable()
        {
            return Repository.GetIQueryable();
        }

        public IQueryable<TEntity> GetIQueryable(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter)
        {
            return Repository.GetIQueryable(filter);
        }

        public IQueryable<TEntity> GetIQueryable<TOderKey>(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter, int pageIndex, int pageSize, System.Linq.Expressions.Expression<Func<TEntity, TOderKey>> sortKeySelector, bool isAsc = true)
        {
            return Repository.GetIQueryable(filter, pageIndex, pageSize, sortKeySelector, isAsc);
        }
        public int TotalCount()
        {
            return Repository.TotalCount();
        }
        public int Count(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return Repository.Count(predicate);
        }

        public void Update(TEntity instance)
        {
            Repository.Update(instance);
        }

        public void UpdateByNeed(TEntity instance, params string[] properties)
        {
            Repository.UpdateByNeed(instance, properties);
        }

        public void Add(TEntity instance)
        {
            Repository.Add(instance);
        }

        public void Delete(TEntity instance)
        {
            Repository.Delete(instance);
        }



        public void AddList(IEnumerable<TEntity> instancelist)
        {
            Repository.AddList(instancelist);
        }

        public void DeleteList(IEnumerable<TEntity> instancelist)
        {
            Repository.DeleteList(instancelist);
        }

        public void ExecuteSQLCommand(string sqlString, params object[] pampaters)
        {
            this.Repository.ExecuteSQLCommand(sqlString, pampaters);
        }

        public IQueryable<TEntity> ExecQuery(string sqlString, params object[] pampaters)
        {
            return this.Repository.ExecQuery(sqlString, pampaters);
        }

        public void BulkInsertList<T>(string tableName, IList<T> list) where T : class
        {
            this.BulkInsertList<T>(tableName, list);
        }

        public void BulkInsertTable(string tableName, System.Data.DataTable table)
        {
            this.Repository.BulkInsertTable(tableName, table);
        }


        public void UpdateWithoutNullablePropertyWhenNull(TEntity instance)
        {
            this.Repository.UpdateWithoutNullablePropertyWhenNull(instance);
        }
    }
}
