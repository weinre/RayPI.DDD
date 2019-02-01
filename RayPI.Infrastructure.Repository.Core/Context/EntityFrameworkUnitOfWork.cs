using Microsoft.EntityFrameworkCore;
using RayPI.Domain.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using RayPI.Domain.Core.Specifications;
using RayPI.Infrastructure.Treasury.Core.Extensions;
using RayPI.Infrastructure.Treasury.Core.Helpers;
using RayPI.Infrastructure.Treasury.Core.Interfaces;
using System.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace RayPI.Infrastructure.Repository.Core.Context
{
    public class EntityFrameworkUnitOfWork : BizDbContext
    {
        /// <summary>The _operate content</summary>
        private readonly IOperateContent _operateContent;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public EntityFrameworkUnitOfWork()
        {
        }

        public EntityFrameworkUnitOfWork(DbContextOptions options)
            : base(options)
        {
        }

        public EntityFrameworkUnitOfWork(DbContextOptions<BizDbContext> options, IOperateContent operateContent)
            : this(options)
        {
            this._operateContent = operateContent;
        }

        /// <summary>Sets the base.</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="item">The item.</param>
        /// <param name="isAdd">if set to <c>true</c> [is add].</param>
        private void SetBase<TAggregateRoot>(TAggregateRoot item, bool isAdd = true) where TAggregateRoot : EntityBase
        {
            IEntityBaseAutoSetter entityBaseAutoSetter = item.AutoSetter ?? (IEntityBaseAutoSetter)new OperateSetter(this._operateContent);
            if (!isAdd)
            {
                item.UpdateId = entityBaseAutoSetter.UpdateId;
                item.UpdateTime = entityBaseAutoSetter.UpdateTime;
                item.UpdateName = entityBaseAutoSetter.UpdateName;
            }
            else
            {
                if (item.Id <= 0L)
                    item.Id = IdGenerateHelper.NewId;
                item.CreateId = entityBaseAutoSetter.CreateId;
                item.CreateTime = entityBaseAutoSetter.CreateTime;
                item.CreateName = entityBaseAutoSetter.CreateName;
                item.UpdateId = entityBaseAutoSetter.UpdateId;
                item.UpdateTime = entityBaseAutoSetter.UpdateTime;
                item.UpdateName = entityBaseAutoSetter.UpdateName;
            }
        }

        /// <summary>增加一个对象</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="item">The item.</param>
        /// <returns>System.Int64.</returns>
        public long Add<TAggregateRoot>(TAggregateRoot item) where TAggregateRoot : EntityBase
        {
            if ((object)item == null)
                return -1;
            this.SetBase<TAggregateRoot>(item, true);
            ((DbSet<TAggregateRoot>)this.CreateSet<TAggregateRoot>()).Add(item);
            return item.Id;
        }

        /// <summary>Adds the specified t aggregate roots.</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="tAggregateRoots">The t aggregate roots.</param>
        /// <returns>IEnumerable&lt;System.Int64&gt;.</returns>
        public IEnumerable<long> Add<TAggregateRoot>(IEnumerable<TAggregateRoot> tAggregateRoots) where TAggregateRoot : EntityBase
        {
            List<long> ids = new List<long>();
            if (tAggregateRoots.Any<TAggregateRoot>())
                LinqExtension.Each<TAggregateRoot>(tAggregateRoots, (Action<TAggregateRoot>)(x => ids.Add(this.Add<TAggregateRoot>(x))));
            return (IEnumerable<long>)ids;
        }

        /// <summary>物理删除一个对象</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="item">The item.</param>
        public void Remove<TAggregateRoot>(TAggregateRoot item) where TAggregateRoot : EntityBase
        {
            if ((object)item == null)
                return;
            this.SetDeleted<TAggregateRoot>(item);
            this.Set<TAggregateRoot>().Remove(item);
        }

        /// <summary>根据条件删除</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="filter">The filter.</param>
        public void Remove<TAggregateRoot>(ISpecification<TAggregateRoot> filter) where TAggregateRoot : EntityBase
        {
            LinqExtension.Each<TAggregateRoot>(this.CreateSet<TAggregateRoot>().Where<TAggregateRoot>(filter.SatisfiedBy()), new Action<TAggregateRoot>(this.Remove<TAggregateRoot>));
        }

        /// <summary>根据条件删除</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="tAggregateRoots">The t aggregate roots.</param>
        public void Remove<TAggregateRoot>(IQueryable<TAggregateRoot> tAggregateRoots) where TAggregateRoot : EntityBase
        {
            LinqExtension.Each<TAggregateRoot>(tAggregateRoots, new Action<TAggregateRoot>(this.Remove<TAggregateRoot>));
        }

        /// <summary>根据条件逻辑删除</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="tAggregateRoots">The t aggregate roots.</param>
        public void Delete<TAggregateRoot>(IQueryable<TAggregateRoot> tAggregateRoots) where TAggregateRoot : EntityBase, new()
        {
            LinqExtension.Each<TAggregateRoot>(tAggregateRoots, new Action<TAggregateRoot>(this.Delete<TAggregateRoot>));
        }

        /// <summary>根据条件逻辑删除</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="filter">The filter.</param>
        public void Delete<TAggregateRoot>(ISpecification<TAggregateRoot> filter) where TAggregateRoot : EntityBase, new()
        {
            //LinqExtension.Each<TAggregateRoot>(this.CreateSet<TAggregateRoot>().Where<TAggregateRoot>(filter.SatisfiedBy()).Set<TAggregateRoot, bool>((Expression<Func<TAggregateRoot, bool>>)(x => x.IsDeleted), 1 != 0), (Action<TAggregateRoot>)(x => this.Update<TAggregateRoot>(x, new Expression<Func<TAggregateRoot, object>>[0])));
        }

        /// <summary>根据条件逻辑删除</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="item">The item.</param>
        public void Delete<TAggregateRoot>(TAggregateRoot item) where TAggregateRoot : EntityBase, new()
        {
            item.IsDeleted = true;
            this.Update<TAggregateRoot>(item, new Expression<Func<TAggregateRoot, object>>[0]);
        }

        /// <summary>修改集合</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="tAggregateRoots">The t aggregate roots.</param>
        public void Update<TAggregateRoot>(IQueryable<TAggregateRoot> tAggregateRoots) where TAggregateRoot : EntityBase, new()
        {
            LinqExtension.Each<TAggregateRoot>(tAggregateRoots, (Action<TAggregateRoot>)(x => this.Update<TAggregateRoot>(x, new Expression<Func<TAggregateRoot, object>>[0])));
        }

        /// <summary>根据赋值的对象，进行修改</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="filter">The filter.</param>
        /// <param name="value">The value.</param>
        public void Update<TAggregateRoot>(ISpecification<TAggregateRoot> filter, Expression<Func<TAggregateRoot, TAggregateRoot>> value) where TAggregateRoot : EntityBase, new()
        {
            //LinqExtension.Each<TAggregateRoot>(this.CreateSet<TAggregateRoot>().Where<TAggregateRoot>(filter.SatisfiedBy()).Set<TAggregateRoot>(value), (Action<TAggregateRoot>)(x => this.Update<TAggregateRoot>(x, new Expression<Func<TAggregateRoot, object>>[0])));
        }

        /// <summary>更新实体</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="item">The item.</param>
        /// <param name="ignoreFileds">忽略部分字段更新</param>
        public void Update<TAggregateRoot>(TAggregateRoot item, params Expression<Func<TAggregateRoot, object>>[] ignoreFileds) where TAggregateRoot : EntityBase, new()
        {
            List<string> source = new List<string>()
      {
        "CreateID",
        "CreateName",
        "CreateTime"
      };
            foreach (Expression<Func<TAggregateRoot, object>> ignoreFiled in ignoreFileds)
            {
                MemberInfo member = ignoreFiled.GetMember<TAggregateRoot, object>();
                string propertyName = (object)member != null ? member.Name : (string)null;
                if (!string.IsNullOrEmpty(propertyName) && !source.Exists((Predicate<string>)(x => x == propertyName)))
                    source.Add(propertyName);
            }
            if ((object)item != null)
            {
                this.SetBase<TAggregateRoot>(item, false);
                this.SetModified<TAggregateRoot>(item);
            }
            List<string> list = source.Where<string>((Func<string, bool>)(m => !string.IsNullOrWhiteSpace(m))).Select<string, string>((Func<string, string>)(m => m.Trim())).ToList<string>();
            foreach (MemberInfo property in item.GetType().GetProperties())
            {
                string name = property.Name;
                if (list.Contains<string>(name, (IEqualityComparer<string>)StringComparer.OrdinalIgnoreCase))
                    this.Entry<TAggregateRoot>(item).Property(name).IsModified = false;
            }
        }

        /// <summary>Creates the set.</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <returns>IQueryable&lt;TAggregateRoot&gt;.</returns>
        public IQueryable<TAggregateRoot> CreateSet<TAggregateRoot>() where TAggregateRoot : class
        {
            return (IQueryable<TAggregateRoot>)this.Set<TAggregateRoot>();
        }

        /// <summary>Attaches the specified item.</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="item">The item.</param>
        public void Attach<TAggregateRoot>(TAggregateRoot item) where TAggregateRoot : class
        {
            this.Entry<TAggregateRoot>(item).State = EntityState.Unchanged;
        }

        /// <summary>Sets the modified.</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="item">The item.</param>
        public void SetModified<TAggregateRoot>(TAggregateRoot item) where TAggregateRoot : class
        {
            this.Entry<TAggregateRoot>(item).State = EntityState.Modified;
        }

        /// <summary>Sets the deleted.</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="item">The item.</param>
        public void SetDeleted<TAggregateRoot>(TAggregateRoot item) where TAggregateRoot : class
        {
            this.Entry<TAggregateRoot>(item).State = EntityState.Deleted;
        }

        /// <summary>Applies the current values.</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="original">The original.</param>
        /// <param name="current">The current.</param>
        public void ApplyCurrentValues<TAggregateRoot>(TAggregateRoot original, TAggregateRoot current) where TAggregateRoot : class
        {
            this.Entry<TAggregateRoot>(original).CurrentValues.SetValues((object)current);
        }

        /// <summary>保存</summary>
        /// <exception cref="T:System.Data.Entity.Validation.DbEntityValidationException">Entity Validation Failed - errors follow:\n +
        /// sb.ToString()</exception>
        public void SaveChanges()
        {
            try
            {
                base.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>Commits the and refresh changes.</summary>
        public void CommitAndRefreshChanges()
        {
            bool flag = false;
            do
            {
                try
                {
                    base.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    flag = true;
                    ex.Entries.ToList<EntityEntry>().ForEach((Action<EntityEntry>)(entry => entry.OriginalValues.SetValues(entry.GetDatabaseValues())));
                }
            }
            while (flag);
        }

        /// <summary>Rollbacks the changes.</summary>
        public void RollbackChanges()
        {
            this.ChangeTracker.Entries().ToList<EntityEntry>().ForEach((Action<EntityEntry>)(entry => entry.State = EntityState.Unchanged));
        }

        
        /// <summary>Executes the query.</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="sqlQuery">The SQL query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>IEnumerable&lt;TAggregateRoot&gt;.</returns>
        public IEnumerable<TAggregateRoot> ExecuteQuery<TAggregateRoot>(string sqlQuery, params object[] parameters)
        {
            throw new NotImplementedException();
            //return (IEnumerable<TAggregateRoot>)this.Database.SqlQuery<TAggregateRoot>(sqlQuery, parameters);
        }

        /// <summary>执行sql语句</summary>
        /// <param name="sqlCommand">The SQL command.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>System.Int32.</returns>
        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            throw new NotImplementedException();
            //return this.Database.ExecuteSqlCommand(sqlCommand, parameters);
        }

        /// <summary>执行有返回值的存储过程</summary>
        /// <typeparam name="TAggregateRoot">返回值对象类型</typeparam>
        /// <param name="storedProcedure">存储过程对象</param>
        /// <returns>返回值对象</returns>
        public IEnumerable<TAggregateRoot> ExecuteStoredProcedure<TAggregateRoot>(object storedProcedure)
        {
            throw new NotImplementedException();
            //return this.Database.ExecuteStoredProcedure<TAggregateRoot>(storedProcedure);
        }
        

        /// <summary>执行无返回值的存储过程</summary>
        /// <param name="storedProcedure">The stored procedure.</param>
        public void ExecuteStoredProcedure(object storedProcedure)
        {
            throw new NotImplementedException();
            //this.Database.ExecuteStoredProcedure(storedProcedure);
        }
        

        /// <summary>开始事务</summary>
        /// <param name="level">The level.</param>
        public void BeginTrans(IsolationLevel level)
        {
            if (this.Database.CurrentTransaction != null)
                return;
            this.Database.BeginTransaction();
        }

        
        /// <summary>Ups the sert.</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="item">The item.</param>
        public void UpSert<TAggregateRoot>(TAggregateRoot item) where TAggregateRoot : EntityBase, new()
        {
            throw new NotImplementedException();
            //this.Set<TAggregateRoot>().AddOrUpdate<TAggregateRoot>(item);
        }
        

        /// <summary>事务回滚</summary>
        public void TransRollBack()
        {
            this.Database.CurrentTransaction?.Rollback();
        }

        /// <summary>提交事务</summary>
        public void TransCommit()
        {
            this.Database.CurrentTransaction?.Commit();
        }
    }
}
