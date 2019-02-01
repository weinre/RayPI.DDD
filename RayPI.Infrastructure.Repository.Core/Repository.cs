using RayPI.Domain.Core;
using RayPI.Domain.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RayPI.Domain.Core.Specifications;
using RayPI.Infrastructure.Treasury.Core.Model;
using RayPI.Infrastructure.Treasury.Core.Model.Enums;

namespace RayPI.Infrastructure.Repository.Core
{
    public class Repository<TKey, TAggregateRoot> : IRepository<TKey, TAggregateRoot> where TAggregateRoot : EntityBase, new()
    {
        /// <summary>The _unit of work</summary>
        private readonly IQueryableUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <exception cref="T:System.ArgumentNullException"></exception>
        /// <exception cref="T:System.ArgumentNullException"></exception>
        public Repository(IQueryableUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException(nameof(unitOfWork));
            this._unitOfWork = unitOfWork;
        }

        /// <summary>Gets the member information.</summary>
        /// <param name="lambda">The lambda.</param>
        /// <returns>MemberExpression.</returns>
        /// <exception cref="T:System.ArgumentNullException"></exception>
        /// <exception cref="T:System.ArgumentException"></exception>
        /// <exception cref="T:System.ArgumentNullException"></exception>
        /// <exception cref="T:System.ArgumentException"></exception>
        private MemberExpression GetMemberInfo(LambdaExpression lambda)
        {
            if (lambda == null)
                throw new ArgumentNullException(nameof(lambda));
            MemberExpression memberExpression = (MemberExpression)null;
            switch (lambda.Body.NodeType)
            {
                case ExpressionType.Convert:
                    memberExpression = ((UnaryExpression)lambda.Body).Operand as MemberExpression;
                    break;
                case ExpressionType.MemberAccess:
                    memberExpression = lambda.Body as MemberExpression;
                    break;
            }
            if (memberExpression == null)
                throw new ArgumentException(nameof(lambda));
            return memberExpression;
        }

        /// <summary>Gets the related loading path.</summary>
        /// <param name="relatedLoadingProperty">The related loading property.</param>
        /// <returns>System.String.</returns>
        private string GetRelatedLoadingPath(Expression<Func<TAggregateRoot, object>> relatedLoadingProperty)
        {
            return this.GetMemberInfo((LambdaExpression)relatedLoadingProperty).ToString().Replace(relatedLoadingProperty.Parameters.First<ParameterExpression>().Name + ".", "");
        }

        #region 增

        /// <summary>批量新增</summary>
        /// <param name="tAggregateRoots">实体集合</param>
        /// <returns>IEnumerable&lt;System.Int64&gt;.</returns>
        public virtual IEnumerable<long> Add(IEnumerable<TAggregateRoot> tAggregateRoots)
        {
            IEnumerable<long> longs = this._unitOfWork.Add<TAggregateRoot>(tAggregateRoots);
            this._unitOfWork.SaveChanges();
            return longs;
        }

        /// <summary>新增</summary>
        /// <param name="item">实体</param>
        /// <returns>System.Int64.</returns>
        public virtual long Add(TAggregateRoot item)
        {
            long num = this._unitOfWork.Add<TAggregateRoot>(item);
            this._unitOfWork.SaveChanges();
            return num;
        }
        #endregion

        #region 查

        /// <summary>根据聚合根的id查 或者 联查一些附属信息</summary>
        /// <param name="id">主键</param>
        /// <param name="isIgnoreDelete">是否忽略已逻辑删除的数据</param>
        /// <returns>TAggregateRoot.</returns>
        public virtual TAggregateRoot Find(TKey id, bool isIgnoreDelete = true)
        {
            return this.Find(new DirectSpecification<TAggregateRoot>(x => x.Id.ToString() == id.ToString()), isIgnoreDelete);
        }

        /// <summary>根据条件获取聚合根信息</summary>
        /// <param name="specification">查询条件</param>
        /// <param name="isIgnoreDelete">是否忽略已逻辑删除的数据</param>
        /// <returns>TAggregateRoot.</returns>
        public virtual TAggregateRoot Find(ISpecification<TAggregateRoot> specification, bool isIgnoreDelete = true)
        {
            return this.GetAllMatching(specification, isIgnoreDelete).FirstOrDefault<TAggregateRoot>();
        }

        /// <summary>根据条件获取聚合根信息</summary>
        /// <param name="filter">查询条件</param>
        /// <param name="isIgnoreDelete">是否忽略已逻辑删除的数据</param>
        /// <returns>TAggregateRoot.</returns>
        public virtual TAggregateRoot Find(Expression<Func<TAggregateRoot, bool>> filter, bool isIgnoreDelete = true)
        {
            IQueryable<TAggregateRoot> source = this.CreateSet<TAggregateRoot>().Where<TAggregateRoot>(filter);
            if (isIgnoreDelete)
                source = source.Where<TAggregateRoot>((Expression<Func<TAggregateRoot, bool>>)(x => x.IsDeleted == !isIgnoreDelete));
            return source.FirstOrDefault<TAggregateRoot>();
        }

        /// <summary>查询实体列表</summary>
        /// <param name="isIgnoreDelete">是否忽略已逻辑删除的数据</param>
        /// <returns>IQueryable&lt;TAggregateRoot&gt;.</returns>
        public virtual IQueryable<TAggregateRoot> GetAll(bool isIgnoreDelete = true)
        {
            return this.GetAllMatching(new TrueSpecification<TAggregateRoot>(), isIgnoreDelete);
        }

        /// <summary>不分页查询</summary>
        /// <param name="specification">查询条件</param>
        /// <param name="isIgnoreDelete">是否忽略已逻辑删除的数据</param>
        /// <returns>IQueryable&lt;TAggregateRoot&gt;.</returns>
        public virtual IQueryable<TAggregateRoot> GetAllMatching(ISpecification<TAggregateRoot> specification, bool isIgnoreDelete = true)
        {
            IQueryable<TAggregateRoot> source = this.CreateSet<TAggregateRoot>().Where(specification.SatisfiedBy());
            if (isIgnoreDelete)
                source = source.Where(x => x.IsDeleted == !isIgnoreDelete);
            return source;
        }

        /// <summary>分页查询</summary>
        /// <typeparam name="TK">The type of the tk.</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="filter">查询条件</param>
        /// <param name="orderByExpression">排序条件</param>
        /// <param name="sortOrder">排序（正序、倒序etc）</param>
        /// <param name="isIgnoreDelete">是否忽略已逻辑删除的数据</param>
        /// <returns>PageResult&lt;TAggregateRoot&gt;.</returns>
        public virtual PageResult<TAggregateRoot> GetListPaged<TK>(int pageIndex, int pageSize, ISpecification<TAggregateRoot> filter, Expression<Func<TAggregateRoot, TK>> orderByExpression = null, SortEnum sortOrder = SortEnum.Asc, bool isIgnoreDelete = true)
        {
            return this.GetListPaged<TK>(this.GetAll(isIgnoreDelete), pageIndex, pageSize, filter, orderByExpression, sortOrder);
        }

        /// <summary>分页查询</summary>
        /// <typeparam name="TK">The type of the tk.</typeparam>
        /// <param name="queryAggres">查询集合</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="filter">查询条件</param>
        /// <param name="orderByExpression">排序规则</param>
        /// <param name="sortOrder">排序枚举</param>
        /// <returns>PageResult&lt;TAggregateRoot&gt;.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">null</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">null</exception>
        public virtual PageResult<TAggregateRoot> GetListPaged<TK>(IQueryable<TAggregateRoot> queryAggres, int pageIndex, int pageSize, ISpecification<TAggregateRoot> filter, Expression<Func<TAggregateRoot, TK>> orderByExpression = null, SortEnum sortOrder = SortEnum.Asc)
        {
            IQueryable<TAggregateRoot> source1 = queryAggres;
            int count1 = pageSize * (pageIndex - 1);
            int count2 = pageSize;
            int num = source1.Count<TAggregateRoot>(filter.SatisfiedBy());
            switch (sortOrder)
            {
                case SortEnum.Original:
                    return (PageResult<TAggregateRoot>)null;
                case SortEnum.Asc:
                    IQueryable<TAggregateRoot> source2 = source1.Where<TAggregateRoot>(filter.SatisfiedBy());
                    IOrderedQueryable<TAggregateRoot> source3;
                    if (orderByExpression == null)
                        source3 = source2.OrderBy<TAggregateRoot, long>((Expression<Func<TAggregateRoot, long>>)(x => x.Id));
                    else
                        source3 = source2.OrderBy<TAggregateRoot, TK>(orderByExpression);
                    IQueryable<TAggregateRoot> source4 = source3.Skip<TAggregateRoot>(count1).Take<TAggregateRoot>(count2);
                    return new PageResult<TAggregateRoot>()
                    {
                        List = source4.ToList<TAggregateRoot>(),
                        PageIndex = pageIndex,
                        PageSize = pageSize,
                        TotalCount = num,
                        TotalPages = num > 0 ? (int)Math.Ceiling((double)num / (double)pageSize) : 0
                    };
                case SortEnum.Desc:
                    IQueryable<TAggregateRoot> source5 = source1.Where<TAggregateRoot>(filter.SatisfiedBy());
                    IOrderedQueryable<TAggregateRoot> source6;
                    if (orderByExpression == null)
                        source6 = source5.OrderByDescending<TAggregateRoot, long>((Expression<Func<TAggregateRoot, long>>)(x => x.Id));
                    else
                        source6 = source5.OrderByDescending<TAggregateRoot, TK>(orderByExpression);
                    IQueryable<TAggregateRoot> source7 = source6.Skip<TAggregateRoot>(count1).Take<TAggregateRoot>(count2);
                    return new PageResult<TAggregateRoot>()
                    {
                        List = source7.ToList<TAggregateRoot>(),
                        PageIndex = pageIndex,
                        PageSize = pageSize,
                        TotalCount = num,
                        TotalPages = num > 0 ? (int)Math.Ceiling((double)num / (double)pageSize) : 0
                    };
                default:
                    throw new ArgumentOutOfRangeException(nameof(sortOrder), (object)sortOrder, (string)null);
            }
        }

        /// <summary>获取总数</summary>
        /// <param name="specification">查询条件</param>
        /// <param name="isIgnoreDelete">是否忽略已逻辑删除的数据</param>
        /// <returns>System.Int32.</returns>
        public virtual int GetCount(ISpecification<TAggregateRoot> specification, bool isIgnoreDelete = true)
        {
            return this.GetAll(isIgnoreDelete).Count<TAggregateRoot>(specification.SatisfiedBy());
        }
        #endregion

        #region 删
        /// <summary>批量移除</summary>
        /// <param name="filter">移除条件</param>
        public virtual void Remove(ISpecification<TAggregateRoot> filter)
        {
            this._unitOfWork.Remove<TAggregateRoot>(filter);
            this._unitOfWork.SaveChanges();
        }

        /// <summary>移除</summary>
        /// <param name="item">实体</param>
        public virtual void Remove(TAggregateRoot item)
        {
            this._unitOfWork.Remove<TAggregateRoot>(item);
            this._unitOfWork.SaveChanges();
        }

        /// <summary>移除</summary>
        /// <param name="id">主键</param>
        public virtual void Remove(TKey id)
        {
            this.Remove((ISpecification<TAggregateRoot>)new DirectSpecification<TAggregateRoot>((Expression<Func<TAggregateRoot, bool>>)(x => x.Id.ToString() == id.ToString())));
            this._unitOfWork.SaveChanges();
        }

        /// <summary>批量移除</summary>
        /// <param name="tAggregateRoots">批量实体</param>
        public void Remove(IQueryable<TAggregateRoot> tAggregateRoots)
        {
            this._unitOfWork.Remove<TAggregateRoot>(tAggregateRoots);
            this._unitOfWork.SaveChanges();
        }

        /// <summary>逻辑删除</summary>
        /// <param name="tAggregateRoots">批量实体</param>
        public void Delete(IQueryable<TAggregateRoot> tAggregateRoots)
        {
            this._unitOfWork.Delete<TAggregateRoot>(tAggregateRoots);
            this._unitOfWork.SaveChanges();
        }

        /// <summary>逻辑删除</summary>
        /// <param name="filter">移除条件</param>
        public virtual void Delete(ISpecification<TAggregateRoot> filter)
        {
            this._unitOfWork.Delete<TAggregateRoot>(filter);
            this._unitOfWork.SaveChanges();
        }

        /// <summary>逻辑删除</summary>
        /// <param name="item">The item.</param>
        public virtual void Delete(TAggregateRoot item)
        {
            this._unitOfWork.Delete<TAggregateRoot>(item);
            this._unitOfWork.SaveChanges();
        }

        /// <summary>逻辑删除</summary>
        /// <param name="id">The item.</param>
        public virtual void Delete(TKey id)
        {
            this.Delete((ISpecification<TAggregateRoot>)new DirectSpecification<TAggregateRoot>((Expression<Func<TAggregateRoot, bool>>)(x => x.Id.ToString() == id.ToString())));
            this._unitOfWork.SaveChanges();
        }
        #endregion 

        /// <summary>跟踪</summary>
        /// <param name="item">The item.</param>
        public virtual void TrackItem(TAggregateRoot item)
        {
            if ((object)item == null)
                return;
            this._unitOfWork.Attach<TAggregateRoot>(item);
        }

        #region 改

        /// <summary>批量修改</summary>
        /// <param name="tAggregateRoots">批量实体</param>
        public virtual void Update(IQueryable<TAggregateRoot> tAggregateRoots)
        {
            this._unitOfWork.Update<TAggregateRoot>(tAggregateRoots);
            this._unitOfWork.SaveChanges();
        }

        /// <summary>根据赋值的实体进行修改</summary>
        /// <param name="filter">更新条件</param>
        /// <param name="value">更新字段值</param>
        public virtual void Update(ISpecification<TAggregateRoot> filter, Expression<Func<TAggregateRoot, TAggregateRoot>> value)
        {
            this._unitOfWork.Update<TAggregateRoot>(filter, value);
            this._unitOfWork.SaveChanges();
        }

        /// <summary>更新实体</summary>
        /// <param name="item">The item.</param>
        /// <param name="ignoreFileds">忽略部分字段更新</param>
        /// <exception cref="T:System.NotImplementedException"></exception>
        public virtual void Update(TAggregateRoot item, params Expression<Func<TAggregateRoot, object>>[] ignoreFileds)
        {
            this._unitOfWork.Update<TAggregateRoot>(item, ignoreFileds);
            this._unitOfWork.SaveChanges();
        }

        /// <summary>添加或更新</summary>
        /// <param name="item">实体</param>
        public virtual void UpSert(TAggregateRoot item)
        {
            this._unitOfWork.UpSert<TAggregateRoot>(item);
            this._unitOfWork.SaveChanges();
        }
        #endregion

        /// <summary>CREATE DB SET</summary>
        /// <typeparam name="TAggregateRoot">The type of the t entity.</typeparam>
        /// <returns>IQueryable&lt;TAggregateRoot&gt;.</returns>
        public virtual IQueryable<TAggregateRoot> CreateSet<TAggregateRoot>() where TAggregateRoot : class
        {
            return this._unitOfWork.CreateSet<TAggregateRoot>().AsNoTracking<TAggregateRoot>();
        }

        /// <summary>判断是否存在</summary>
        /// <param name="filter">查询条件</param>
        /// <param name="isIgnoreDelete">是否忽略已逻辑删除的数据</param>
        /// <returns>true=存在，false=不存在</returns>
        public bool Any(ISpecification<TAggregateRoot> filter, bool isIgnoreDelete = true)
        {
            return this.GetAllMatching(filter, isIgnoreDelete).Any<TAggregateRoot>();
        }
    }
}
