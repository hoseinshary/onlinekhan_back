using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Text;
using NasleGhalam.Common;
using NasleGhalam.DomainClasses.EntityConfigs;

namespace NasleGhalam.DataAccess.Context
{
    public class DBContext : DbContext, IUnitOfWork
    {
        static DBContext()
        {
            Database.SetInitializer<DBContext>(null);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // disable cascade delete
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            // default nvarchar(50)
            modelBuilder.Properties<string>().Configure(p => p.HasMaxLength(50));

            modelBuilder.Configurations.AddFromAssembly(typeof(ActionConfig).Assembly);
            
            base.OnModelCreating(modelBuilder);
        }


        #region ### Unit Of Work ###
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void MarkAsDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Deleted;
        }

        public void MarkAsDetached<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Detached;
        }

        public void MarkAsUnChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Unchanged; 
        }

        public void DetachAll()
        {
            foreach (DbEntityEntry dbEntityEntry in ChangeTracker.Entries())
            {
                if (dbEntityEntry.Entity != null)
                {
                    Entry(dbEntityEntry.Entity).State = EntityState.Detached;
                }
            }
        }

        public void UpdateFields<TEntity>(TEntity entity, params Expression<Func<TEntity, object>>[] fields) where TEntity : class
        {
            foreach (var property in fields)
            {
                Entry(entity).Property(property).IsModified = true;
            }
        }

        public void ExcludeFieldsFromUpdate<TEntity>(TEntity entity, params Expression<Func<TEntity, object>>[] fields) where TEntity : class
        {
            MarkAsChanged(entity);
            foreach (var property in fields)
            {
                Entry(entity).Property(property).IsModified = false;
            }
        }

        public void ValidateOnSaveEnabled(bool validateOnSaveEnabled)
        {
            Configuration.ValidateOnSaveEnabled = validateOnSaveEnabled;
        }

        public int SaveChanges(bool validateOnSaveEnabled)
        {
            ValidateOnSaveEnabled(validateOnSaveEnabled);
            return SaveChanges();
        }

        public ServerMessageResult CommitChanges(CrudType type = CrudType.None, string fieldName = "")
        {
            ServerMessageResult result = new ServerMessageResult();

            try
            {
                SaveChanges();

                string str;
                switch (type)
                {
                    case CrudType.Create:
                        str = " با موفقیت ثبت گردید.";
                        break;
                    case CrudType.Update:
                        str = " با موفقیت ویرایش گردید.";
                        break;
                    case CrudType.Delete:
                        str = " با موفقیت حذف گردید.";
                        break;
                    default:
                        str = "عملیات با موفقیت انجام گردید.";
                        break;
                }

                if (!string.IsNullOrEmpty(fieldName) && type != CrudType.None)
                {
                    str = fieldName + str;
                }

                result.FaMessage = str;
                result.MessageType = MessageType.Success;
            }
            catch (SqlException ex)
            {
                StringBuilder err = new StringBuilder();
                foreach (SqlError sqlErr in ex.Errors)
                {
                    err.Append(sqlErr.Message);
                    result.ErrorNumber = sqlErr.Number;
                }

                result.FaMessage = "خطا در اعمال اطلاعات! با مدیر تماس بگیرید.";
                result.MessageType = MessageType.Error;
                result.EnMessage = err.ToString();
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            catch (DbUpdateException ex)
            {
                SqlException innerException = null;
                Exception tmp = ex;
                while (innerException == null && tmp != null)
                {
                    if (tmp.InnerException != null)
                    {
                        innerException = tmp.InnerException as SqlException;
                    }
                    tmp = tmp.InnerException;
                }
                if (innerException != null && (innerException.Number == 2601 || innerException.Number == 2627))
                {
                    result.FaMessage = string.IsNullOrEmpty(fieldName) ? "خطای تکراری بودن داده! با مدیر تماس بگیرید." : $"این {fieldName} تکراری میباشد";
                    result.ErrorNumber = 2601;
                }
                else if (type == CrudType.Delete && innerException != null && innerException.Number == 547)
                {
                    result.FaMessage = "خطا در حذف اطلاعات ،";
                    result.FaMessage += "خطای رابطه ای! این موجودیت با دیگر جداول در ارتباط میباشد... ابتدا آنها را حذف نمایید";
                }
                else
                {
                    result.FaMessage = "خطا در اعمال اطلاعات! با مدیر تماس بگیرید.";
                    result.ErrorNumber = innerException?.Number ?? 0;
                }

                result.MessageType = MessageType.Error;
                result.EnMessage = innerException?.ToString() ?? ex.ToString();
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    //Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                    //    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        result.FaMessage += ve.PropertyName + ": " + ve.ErrorMessage + "،";
                    }
                }

                result.MessageType = MessageType.Error;
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }

            return result;
        }

        public DbContextTransaction BeginTransaction()
        {
            return Database.BeginTransaction();
        }

        public DbRawSqlQuery<T> SqlQuery<T>(string sqlQuery, params object[] sqlParam)
        {
            return Database.SqlQuery<T>(sqlQuery, sqlParam);
        }

        public int ExecuteSqlCommand(string sqlQuery, params object[] sqlParam)
        {
            return Database.ExecuteSqlCommand(sqlQuery, sqlParam);
        }
        #endregion
    }
}
