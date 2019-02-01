using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace RayPI.Infrastructure.Treasury.Core.Helpers
{
    public static class AutoMapperHelper
    {
        public static TTarget Map<TSource, TTarget>(TSource source, TTarget targetInstance = null)
            where TSource : class
            where TTarget : class, new()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TTarget>());
            var mapper = config.CreateMapper();
            return mapper.Map<TTarget>(source);
        }
    }
}
