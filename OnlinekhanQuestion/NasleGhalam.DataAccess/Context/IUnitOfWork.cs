using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NasleGhalam.Common;

namespace NasleGhalam.DataAccess.Context
{
    public interface IUnitOfWork
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        void ValidateOnSaveEnabled(bool validateOnSaveEnabled);
        int SaveChanges();
        int SaveChanges(bool validateOnSaveEnabled);
        ServerMessageResult CommitChanges(CrudType type = CrudType.None, string fieldName = "");
        Task<int> SaveChangesAsync();
        void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class;
        void MarkAsDeleted<TEntity>(TEntity entity) where TEntity : class;
        void MarkAsDetached<TEntity>(TEntity entity) where TEntity : class;
        void MarkAsUnChanged<TEntity>(TEntity entity) where TEntity : class;
        void DetachAll();
        void UpdateFields<TEntity>(TEntity entity, params Expression<Func<TEntity, object>>[] fields) where TEntity : class;
        void ExcludeFieldsFromUpdate<TEntity>(TEntity entity, params Expression<Func<TEntity, object>>[] fields) where TEntity : class;
        void Dispose();
        DbContextTransaction BeginTransaction();
        int ExecuteSqlCommand(string sqlQuery, params object[] sqlParam);
        DbRawSqlQuery<T> SqlQuery<T>(string sqlQuery, params object[] sqlParam);
    }
}
