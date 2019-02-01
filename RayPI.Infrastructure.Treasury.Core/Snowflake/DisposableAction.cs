using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.Infrastructure.Treasury.Core.Snowflake
{
    public class DisposableAction : IDisposable
    {
        private readonly Action _action;

        public DisposableAction(Action action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));
            this._action = action;
        }

        public void Dispose()
        {
            this._action();
        }
    }
}
