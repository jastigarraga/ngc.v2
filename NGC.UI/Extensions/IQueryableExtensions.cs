using NGC.UI.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGC.UI.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Map<U,T>(this IQueryable<U> source)
        {
            var mapper = MapperFactory.GetMapper();
            return source.Select(u => mapper.Map<T>(u));
        }
        public static IEnumerable<T> Map<U,T>(this IEnumerable<U> source)
        {
            var mapper = MapperFactory.GetMapper();
            return source.Select(u => mapper.Map<T>(u));
        }
    }
}
