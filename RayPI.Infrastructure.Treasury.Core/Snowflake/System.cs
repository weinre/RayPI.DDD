using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.Infrastructure.Treasury.Core.Snowflake
{
    public static class System
    {
        public static Func<long> currentTimeFunc = new Func<long>(InternalCurrentTimeMillis);
        private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long CurrentTimeMillis()
        {
            return currentTimeFunc();
        }

        public static IDisposable StubCurrentTime(Func<long> func)
        {
            currentTimeFunc = func;
            return (IDisposable)new DisposableAction((Action)(() => currentTimeFunc = new Func<long>(InternalCurrentTimeMillis)));
        }

        public static IDisposable StubCurrentTime(long millis)
        {
            currentTimeFunc = (Func<long>)(() => millis);
            return (IDisposable)new DisposableAction((Action)(() => currentTimeFunc = new Func<long>(InternalCurrentTimeMillis)));
        }

        private static long InternalCurrentTimeMillis()
        {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }
    }
}
