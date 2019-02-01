using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.Domain.Core
{
    public interface IUnitOfWork : ISQL, IDisposable
    {
        void SaveChanges();

        void CommitAndRefreshChanges();

        void RollbackChanges();
    }
}
