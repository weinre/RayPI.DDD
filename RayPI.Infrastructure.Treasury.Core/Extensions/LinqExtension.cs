using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace RayPI.Infrastructure.Treasury.Core.Extensions
{
    /// <summary>Linq帮助类</summary>
    public static class LinqExtension
    {
        /// <summary>each循环</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iEnumerable"></param>
        /// <param name="action"></param>
        [DebuggerStepThrough]
        public static void Each<T>(this IEnumerable<T> iEnumerable, Action<T> action)
        {
            //if (!iEnumerable.IsNotNull())
            if (iEnumerable==null)
                return;
            foreach (T i in iEnumerable)
                action(i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iEnumerable"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        [DebuggerStepThrough]
        public static void Each<T>(this IEnumerable iEnumerable, Action<T> action)
        {
            //if (!iEnumerable.IsNotNull())
            if (iEnumerable==null)
                return;
            foreach (object i in iEnumerable)
                action((T)i);
        }

        /// <summary>扩展的Distinct</summary>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TV"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> Distinct<T, TV>(this IEnumerable<T> source, Func<T, TV> keySelector)
        {
            return source.Distinct<T>((IEqualityComparer<T>)new LinqExtension.CommonEqualityComparer<T, TV>(keySelector));
        }

        private class CommonEqualityComparer<T, TV> : IEqualityComparer<T>
        {
            private readonly Func<T, TV> _keySelector;

            public CommonEqualityComparer(Func<T, TV> keySelector)
            {
                this._keySelector = keySelector;
            }

            public bool Equals(T x, T y)
            {
                return EqualityComparer<TV>.Default.Equals(this._keySelector(x), this._keySelector(y));
            }

            public int GetHashCode(T obj)
            {
                return EqualityComparer<TV>.Default.GetHashCode(this._keySelector(obj));
            }
        }
    }
}
