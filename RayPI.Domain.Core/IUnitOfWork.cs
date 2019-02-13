using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.Domain.Core
{
    /// <summary>
    /// UnitOfWork接口
    /// </summary>
    public interface IUnitOfWork : ISQL, IDisposable
    {
        void SaveChanges();

        void CommitAndRefreshChanges();

        void RollbackChanges();
    }
}
