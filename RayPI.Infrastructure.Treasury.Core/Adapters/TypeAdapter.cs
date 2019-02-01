using AutoMapper;
using System;

namespace RayPI.Infrastructure.Treasury.Core.Adapters
{
    public sealed class TypeAdapter : ITypeAdapter
    {
        public TTarget Adapt<TSource, TTarget>(TSource source, TTarget targetInstance = null)
            where TSource : class
            where TTarget : class, new()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TTarget>());
            var mapper = config.CreateMapper();
            return mapper.Map<TTarget>(source);
            //Mapper.Initialize(c => c.CreateMap<TSource, TTarget>());
            //return Mapper.Map<TTarget>(source);
        }

        public TTarget Adapt<TSource, TTarget>(TSource source, TTarget targetInstance = null, params object[] moreSources)
            where TSource : class
            where TTarget : class, new()
        {
            throw new NotImplementedException();
        }
    }
}
