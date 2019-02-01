using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.Infrastructure.Treasury.Core.Adapters
{
    /// <summary>
    /// Base contract for map dto to aggregate or aggregate to dto.
    /// <remarks>
    /// This is a  contract for work with "auto" mappers ( automapper,emitmapper,valueinjecter...)
    /// or adhoc mappers
    /// </remarks>
    /// </summary>
    public interface ITypeAdapter
    {
        /// <summary>Adapt a source object to an instance of type</summary>
        /// <typeparam name="TSource">Type of source item</typeparam>
        /// <typeparam name="TTarget">Type of target item</typeparam>
        /// <param name="source">Instance to adapt</param>
        /// <param name="targetInstance"></param>
        /// <returns><paramref name="source" /> mapped to <typeparamref name="TTarget" /></returns>
        TTarget Adapt<TSource, TTarget>(TSource source, TTarget targetInstance = null) where TSource : class where TTarget : class, new();

        /// <summary>
        /// Adapt a source object to an instance of type
        /// nested object for map completion
        /// </summary>
        /// <typeparam name="TSource">Type of source item</typeparam>
        /// <typeparam name="TTarget">Type of target item</typeparam>
        /// <param name="source">Instance of source to map</param>
        /// <param name="targetInstance"></param>
        /// <param name="moreSources">More sources for mapping</param>
        /// <returns><paramref name="source" /> mapped to <typeparamref name="TTarget" /></returns>
        TTarget Adapt<TSource, TTarget>(TSource source, TTarget targetInstance = null, params object[] moreSources) where TSource : class where TTarget : class, new();
    }
}
