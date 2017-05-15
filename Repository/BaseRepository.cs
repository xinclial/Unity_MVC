using EF_DataModel;
using Microsoft.Practices.Unity.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Repository;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Tools.Extensions;

namespace Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected DataModelContext DbContext = new DataModelContext();
        protected SqlConnection sqlconnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SqlServerString"].ConnectionString);

        public DbSet<TEntity> DbSet { get; private set; }

        public DbEntityEntry<TEntity> CurrentEntry { get; private set; }

        public void GetEntry(TEntity entity)
        {
            this.CurrentEntry = this.DbContext.Entry<TEntity>(entity);
        }

        public BaseRepository()
        {
            this.DbSet = this.DbContext.Set<TEntity>();

        }
        public BaseRepository(DataModelContext context)
        {
            Guard.ArgumentNotNull(context, "context");
            this.DbContext = context;
            this.DbSet = this.DbContext.Set<TEntity>();
        }

        public TEntity GetEntity(Expression<Func<TEntity, bool>> filter)
        {
            return this.DbSet.Where(filter).FirstOrDefault();
        }

        public IQueryable<TEntity> GetIQueryable()
        {
            return this.DbSet.AsQueryable();
        }
        public IQueryable<TEntity> GetIQueryable(Expression<Func<TEntity, bool>> filter)
        {
            return this.DbSet.Where(filter).AsQueryable();
        }
        public IQueryable<TEntity> GetIQueryable<TKey>(Expression<Func<TEntity, bool>> filter, int pageIndex, int pageSize, Expression<Func<TEntity, TKey>> sortKeySelector, bool isAsc = true)
        {
            Guard.ArgumentNotNull(filter, "predicate");
            Guard.ArgumentNotNull(sortKeySelector, "sortKeySelector");
            if (isAsc)
            {
                return this.DbSet
                    .Where(filter)
                    .OrderBy(sortKeySelector)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize).AsQueryable();
            }
            else
            {
                return this.DbSet
                    .Where(filter)
                    .OrderByDescending(sortKeySelector)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize).AsQueryable();
            }
        }

        public int TotalCount()
        {
            return this.DbSet.Count();
        }
        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return this.DbSet.Where(predicate).Count();
        }

        public void Add(TEntity instance)
        {
            Guard.ArgumentNotNull(instance, "instance");
            this.DbSet.Attach(instance);
            this.DbContext.Entry(instance).State = EntityState.Added;
            this.DbContext.SaveChanges();
        }
        public void Update(TEntity instance)
        {
            Guard.ArgumentNotNull(instance, "instance");
            this.DbSet.Attach(instance);
            this.DbContext.Entry(instance).State = EntityState.Modified;
            this.DbContext.SaveChanges();
        }
        public void UpdateByNeed(TEntity instance, params string[] properties)
        {
            Guard.ArgumentNotNull(instance, "instance");
            this.DbSet.Attach(instance);
            if (properties == null)
            {
                this.DbContext.Entry(instance).State = EntityState.Modified;
            }
            else
            {
                GetEntry(instance);
                foreach (var item in properties)
                {
                    this.CurrentEntry.Property(item).IsModified = true;
                }
            }
            this.DbContext.SaveChanges();
        }

        public void Delete(TEntity instance)
        {
            Guard.ArgumentNotNull(instance, "instance");
            this.DbSet.Attach(instance);
            this.DbContext.Entry(instance).State = EntityState.Deleted;
            this.DbContext.SaveChanges();
        }

        public void Dispose()
        {
            this.DbContext.Dispose();
        }
        public void AddList(IEnumerable<TEntity> instancelist)
        {


            //foreach (var instance in instancelist)
            //{
            //    Guard.ArgumentNotNull(instance, "instance");
            //    this.DbContext.Entry(instance).State = EntityState.Added;
            //}

            this.DbSet.AddRange(instancelist);

            this.DbContext.SaveChanges();


        }

        public void DeleteList(IEnumerable<TEntity> instancelist)
        {
            //foreach (var instance in instancelist)
            //{
            //    Guard.ArgumentNotNull(instance, "instance");
            //    this.DbContext.Entry(instance).State = EntityState.Deleted;
            //}
            this.DbSet.RemoveRange(instancelist);
            this.DbContext.SaveChanges();

        }

        public void ExecuteSQLCommand(string sqlString, params object[] pampaters)
        {
            DbContext.Database.ExecuteSqlCommand(sqlString, pampaters);
        }
        public void BulkInsertList<T>(string tableName, IList<T> list) where T : class
        {
            using (var bulkCopy = new SqlBulkCopy(sqlconnection))
            {

                DataTable table = new DataTable();
                table = list.ToDataTable();

                bulkCopy.DestinationTableName = tableName;
                bulkCopy.BatchSize = table.Rows.Count;
                bulkCopy.WriteToServer(table);
            }
        }
        public void BulkInsertTable(string tableName, DataTable table)
        {
            using (var bulkCopy = new SqlBulkCopy(sqlconnection))
            {
                bulkCopy.DestinationTableName = tableName;
                bulkCopy.BatchSize = table.Rows.Count;
                bulkCopy.WriteToServer(table);
            }
        }
        public IQueryable<TEntity> ExecQuery(string sqlString, params object[] pampaters)
        {
            return DbContext.Database.SqlQuery<TEntity>(sqlString, pampaters) as IQueryable<TEntity>;
        }


        public void UpdateWithoutNullablePropertyWhenNull(TEntity instance)
        {
            Guard.ArgumentNotNull(instance, "instance");
            this.DbSet.Attach(instance);
            //获取这些可以为空的属性
            this.GetEntry(instance);
            var props = typeof(TEntity).GetProperties();
            foreach (var prop in props)
            {
                if (prop.PropertyType == typeof(string) || (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>))))
                {
                    //属性可以为空
                    //判断值是否为空
                    if (prop.GetValue(instance, null) != null)
                        this.CurrentEntry.Property(prop.Name).IsModified = true;
                }
                else
                    this.CurrentEntry.Property(prop.Name).IsModified = true;
            }

            this.DbContext.SaveChanges();
        }
    }
}
