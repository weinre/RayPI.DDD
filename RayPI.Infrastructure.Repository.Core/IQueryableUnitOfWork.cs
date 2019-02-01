using RayPI.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using RayPI.Domain.Core.Entity;
using RayPI.Domain.Core.Specifications;

namespace RayPI.Infrastructure.Repository.Core
{
    public interface IQueryableUnitOfWork : IUnitOfWork, ISQL, IDisposable
    {
        /// <summary>获取一个上下文类型的具体实例</summary>
        /// <typeparam name="TAggregateRoot">The type of the t entity.</typeparam>
        /// <returns>IQueryable&lt;TAggregateRoot&gt;.</returns>
        IQueryable<TAggregateRoot> CreateSet<TAggregateRoot>() where TAggregateRoot : class;

        /// <summary>附件对象</summary>
        /// <typeparam name="TAggregateRoot">The type of the t entity.</typeparam>
        /// <param name="item">The item.</param>
        void Attach<TAggregateRoot>(TAggregateRoot item) where TAggregateRoot : class;

        /// <summary>设置对象为修改状态</summary>
        /// <typeparam name="TAggregateRoot">The type of the t entity.</typeparam>
        /// <param name="item">The item.</param>
        void SetModified<TAggregateRoot>(TAggregateRoot item) where TAggregateRoot : class;

        /// <summary>更新对象值</summary>
        /// <typeparam name="TAggregateRoot">The type of the t entity.</typeparam>
        /// <param name="original">The original.</param>
        /// <param name="current">The current.</param>
        void ApplyCurrentValues<TAggregateRoot>(TAggregateRoot original, TAggregateRoot current) where TAggregateRoot : class;

        /// <summary>添加</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="item">The item.</param>
        long Add<TAggregateRoot>(TAggregateRoot item) where TAggregateRoot : EntityBase;

        /// <summary>批量添加</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="tAggregateRoots">The t aggregate roots.</param>
        IEnumerable<long> Add<TAggregateRoot>(IEnumerable<TAggregateRoot> tAggregateRoots) where TAggregateRoot : EntityBase;

        /// <summary>物理删除</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="item">The item.</param>
        void Remove<TAggregateRoot>(TAggregateRoot item) where TAggregateRoot : EntityBase;

        /// <summary>根据条件物理删除</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="filter">The filter.</param>
        void Remove<TAggregateRoot>(ISpecification<TAggregateRoot> filter) where TAggregateRoot : EntityBase;

        /// <summary>批量物理删除</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="tAggregateRoots">The t aggregate roots.</param>
        void Remove<TAggregateRoot>(IQueryable<TAggregateRoot> tAggregateRoots) where TAggregateRoot : EntityBase;

        /// <summary>批量逻辑删除</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="tAggregateRoots">The t aggregate roots.</param>
        void Delete<TAggregateRoot>(IQueryable<TAggregateRoot> tAggregateRoots) where TAggregateRoot : EntityBase, new();

        /// <summary>根据状态逻辑删除</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="filter">The filter.</param>
        void Delete<TAggregateRoot>(ISpecification<TAggregateRoot> filter) where TAggregateRoot : EntityBase, new();

        /// <summary>逻辑删除</summary>
        /// <param name="item">The item.</param>
        void Delete<TAggregateRoot>(TAggregateRoot item) where TAggregateRoot : EntityBase, new();

        /// <summary>批量修改</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="tAggregateRoots">The t aggregate roots.</param>
        void Update<TAggregateRoot>(IQueryable<TAggregateRoot> tAggregateRoots) where TAggregateRoot : EntityBase, new();

        /// <summary>根据条件修改</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="filter">The filter.</param>
        /// <param name="value">The value.</param>
        void Update<TAggregateRoot>(ISpecification<TAggregateRoot> filter, Expression<Func<TAggregateRoot, TAggregateRoot>> value) where TAggregateRoot : EntityBase, new();

        /// <summary>更新实体</summary>
        /// <param name="item">The item.</param>
        /// <param name="ignoreFileds">忽略部分字段更新</param>
        void Update<TAggregateRoot>(TAggregateRoot item, params Expression<Func<TAggregateRoot, object>>[] ignoreFileds) where TAggregateRoot : EntityBase, new();

        /// <summary>添加或更新对象</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="item">The item.</param>
        void UpSert<TAggregateRoot>(TAggregateRoot item) where TAggregateRoot : EntityBase, new();
    }
}
