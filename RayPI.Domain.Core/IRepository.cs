using RayPI.Domain.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using RayPI.Domain.Core.Entity;
using RayPI.Infrastructure.Treasury.Core.Model;
using RayPI.Infrastructure.Treasury.Core.Model.Enums;

namespace RayPI.Domain.Core
{
    public interface IRepository<TKey, TAggregateRoot> where TAggregateRoot : EntityBase, new()
    {
        #region 增
        /// <summary>
        /// 添加单个
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        long Add(TAggregateRoot item);

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="tAggregateRoots"></param>
        /// <returns></returns>
        IEnumerable<long> Add(IEnumerable<TAggregateRoot> tAggregateRoots);
        #endregion


        #region 删
        /// <summary>
        /// 根据Id移除
        /// </summary>
        /// <param name="id"></param>
        void Remove(TKey id);

        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="item"></param>
        void Remove(TAggregateRoot item);

        /// <summary>
        /// 批量移除
        /// </summary>
        /// <param name="filter"></param>
        void Remove(ISpecification<TAggregateRoot> filter);

        /// <summary>
        /// 批量移除
        /// </summary>
        /// <param name="tAggregateRoots"></param>
        void Remove(IQueryable<TAggregateRoot> tAggregateRoots);

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="filter"></param>
        void Delete(ISpecification<TAggregateRoot> filter);

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="tAggregateRoots">批量实体</param>
        void Delete(IQueryable<TAggregateRoot> tAggregateRoots);

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="item">The item.</param>
        void Delete(TAggregateRoot item);

        /// <summary>逻辑删除</summary>
        /// <param name="key">The key.</param>
        void Delete(TKey key);
        #endregion


        #region 改
        /// <summary>批量更新</summary>
        /// <param name="tAggregateRoots">批量实体</param>
        void Update(IQueryable<TAggregateRoot> tAggregateRoots);

        /// <summary>根据赋值的实体进行更新</summary>
        /// <param name="filter">更新条件</param>
        /// <param name="value">更新字段值</param>
        void Update(ISpecification<TAggregateRoot> filter, Expression<Func<TAggregateRoot, TAggregateRoot>> value);

        /// <summary>更新实体</summary>
        /// <param name="item">The item.</param>
        /// <param name="ignoreFileds">忽略部分字段更新</param>
        void Update(TAggregateRoot item, params Expression<Func<TAggregateRoot, object>>[] ignoreFileds);

        /// <summary>添加或更新</summary>
        /// <param name="item">实体</param>
        void UpSert(TAggregateRoot item);
        #endregion


        #region 查
        /// <summary>跟踪</summary>
        /// <param name="item">The item.</param>
        void TrackItem(TAggregateRoot item);

        /// <summary>根据聚合根的id查 或者 联查一些附属信息</summary>
        /// <param name="id">主键</param>
        /// <param name="isIgnoreDelete">是否忽略已逻辑删除的数据</param>
        /// <returns>TAggregateRoot.</returns>
        TAggregateRoot Find(TKey id, bool isIgnoreDelete = true);

        /// <summary>根据条件获取聚合根信息</summary>
        /// <param name="specification">查询条件</param>
        /// <param name="isIgnoreDelete">是否忽略已逻辑删除的数据</param>
        /// <returns>TAggregateRoot.</returns>
        TAggregateRoot Find(ISpecification<TAggregateRoot> specification, bool isIgnoreDelete = true);

        /// <summary>根据条件获取聚合根信息</summary>
        /// <param name="filter">查询条件</param>
        /// <param name="isIgnoreDelete">是否忽略已逻辑删除的数据</param>
        /// <returns>TAggregateRoot.</returns>
        TAggregateRoot Find(Expression<Func<TAggregateRoot, bool>> filter, bool isIgnoreDelete = true);

        /// <summary>查询实体信息</summary>
        /// <param name="isIgnoreDelete">是否忽略已逻辑删除的数据</param>
        /// <returns>IQueryable&lt;TAggregateRoot&gt;.</returns>
        IQueryable<TAggregateRoot> GetAll(bool isIgnoreDelete = true);

        /// <summary>不分页查询</summary>
        /// <param name="specification">查询条件</param>
        /// <param name="isIgnoreDelete">是否忽略已逻辑删除的数据</param>
        /// <returns>IQueryable&lt;TAggregateRoot&gt;.</returns>
        IQueryable<TAggregateRoot> GetAllMatching(ISpecification<TAggregateRoot> specification, bool isIgnoreDelete = true);

        /// <summary>分页查询</summary>
        /// <typeparam name="TK">The type of the tk.</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="filter">查询条件</param>
        /// <param name="orderByExpression">排序条件</param>
        /// <param name="sortOrder">排序（正序、倒序etc）</param>
        /// <param name="isIgnoreDelete">是否忽略已逻辑删除的数据</param>
        /// <returns>PageResult&lt;TAggregateRoot&gt;.</returns>
        PageResult<TAggregateRoot> GetListPaged<TK>(int pageIndex, int pageSize, ISpecification<TAggregateRoot> filter, Expression<Func<TAggregateRoot, TK>> orderByExpression = null, SortEnum sortOrder = SortEnum.Asc, bool isIgnoreDelete = true);

        /// <summary>分页查询</summary>
        /// <typeparam name="TK">The type of the tk.</typeparam>
        /// <param name="queryAggres">查询集合</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="filter">查询条件</param>
        /// <param name="orderByExpression">排序规则</param>
        /// <param name="sortOrder">排序枚举</param>
        /// <returns>PageResult&lt;TAggregateRoot&gt;.</returns>
        PageResult<TAggregateRoot> GetListPaged<TK>(IQueryable<TAggregateRoot> queryAggres, int pageIndex, int pageSize, ISpecification<TAggregateRoot> filter, Expression<Func<TAggregateRoot, TK>> orderByExpression = null, SortEnum sortOrder = SortEnum.Asc);

        /// <summary>判断是否存在</summary>
        /// <param name="filter">查询条件</param>
        /// <param name="isIgnoreDelete">是否忽略已逻辑删除的数据</param>
        /// <returns>true=存在，false=不存在</returns>
        bool Any(ISpecification<TAggregateRoot> filter, bool isIgnoreDelete = true);

        /// <summary>获取总数</summary>
        /// <param name="specification">查询条件</param>
        /// <param name="isIgnoreDelete">是否忽略已逻辑删除的数据</param>
        /// <returns>System.Int32.</returns>
        int GetCount(ISpecification<TAggregateRoot> specification, bool isIgnoreDelete = true);
        #endregion
    }
}
